using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private bool targetHit;
    public int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Destroy stray bullets after 3 seconds so it won't pollute the scene
        Invoke("Delete", 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (targetHit)
            return;
        else
            targetHit = true;

        rb.isKinematic = true; // makes it stick

        transform.SetParent(collision.transform);

        switch (collision.gameObject.tag) 
        {
            case "Enemy":
                Debug.Log("A");
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                Delete();
                break;
            case "Surface":
                Debug.Log("B");
                Delete();
                break;
        }
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
