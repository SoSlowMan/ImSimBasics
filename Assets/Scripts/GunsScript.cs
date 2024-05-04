using UnityEngine;

public class GunsScript : MonoBehaviour
{
    //Stats
    [SerializeField] int damage;
    [SerializeField] bool allowButtonHold;
    [SerializeField] int magazineSize, bulletsPerTap;
    [SerializeField] float timeBetweenShots, timeBetweenShoting, spread, reloadTime, range;
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading; //enum??
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
