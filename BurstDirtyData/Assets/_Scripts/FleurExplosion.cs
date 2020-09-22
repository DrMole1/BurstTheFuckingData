using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleurExplosion : MonoBehaviour
{
    public GameObject ptcFleurPrefab;
    public float radius = 2.5f;
    ElementsProperties elem;


    public void Fire()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                if(rb.gameObject.transform.GetComponent<ElementsProperties>() != null)
                {
                    elem = rb.gameObject.transform.GetComponent<ElementsProperties>();
                    GameObject.Find("GameManager").GetComponent<FireConditions>().OnElementDetected(elem);
                }
            }
        }

        GameObject ptcFleurInstance;
        ptcFleurInstance = Instantiate(ptcFleurPrefab, transform.parent.position, Quaternion.identity);
        Destroy(ptcFleurInstance, 8f);

        Destroy(transform.parent.gameObject);
    }
}
