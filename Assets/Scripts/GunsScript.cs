using UnityEngine;
using TMPro;

public class GunsScript : MonoBehaviour
{
    //Refs
    [SerializeField] GameObject bullet;
    [SerializeField] Camera cam;
    [SerializeField] Transform shootPoint;

    //Stats
    [SerializeField] bool allowButtonHold;
    [SerializeField] int damage, magazineSize, bulletsPerTap;
    [SerializeField] float timeBetweenShots, timeBetweenShooting, spread, reloadTime, range;
    [SerializeField] float shootForce, upwardForce;
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading; //enum??

    //Bug fixes
    [SerializeField] bool allowInvoke = true;

    //Keys
    [SerializeField] KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] KeyCode reloadKey = KeyCode.R;

    //Misc
    Vector3 center = new Vector3(0.5f, 0.5f, 0);

    //UI
    //[SerializeField] TextMeshProUGUI ammoUI;

    private void Awake()
    {
        //Full mag on start
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        MyInput();

        //ammoUI.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }

    private void MyInput()
    {
        if (allowButtonHold)
            shooting = Input.GetKey(shootKey);
        else
            shooting = Input.GetKeyDown(shootKey);

        if (Input.GetKeyDown(reloadKey) && bulletsLeft < magazineSize && !reloading)
            Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
            Reload();

        if(shooting && readyToShoot && bulletsLeft > 0 && !reloading)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = cam.ViewportPointToRay(center);
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100);

        Vector3 directionWithoutSpread = targetPoint - shootPoint.position;
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        if(allowInvoke)
        {
            Invoke("ResetShots", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShots()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadEnd", reloadTime);
    }

    private void ReloadEnd()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }


}
