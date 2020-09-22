using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobombExplosion : MonoBehaviour
{
    public float radius = 2.5f;
    public float force = 10f;
    public GameObject ptcBobombPrefab;


    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        GameObject ptcBobombInstance;
        ptcBobombInstance = Instantiate(ptcBobombPrefab, transform.parent.position, Quaternion.identity);
        Destroy(ptcBobombInstance, 8f);

        Destroy(transform.parent.gameObject);
    }
}
