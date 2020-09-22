using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarExplosion : MonoBehaviour
{
    public GameObject ptcStarPrefab;
    public float radius = 150f;
    ElementsProperties elem;


    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                if (rb.gameObject.transform.GetComponent<ElementsProperties>() != null)
                {
                    elem = rb.gameObject.transform.GetComponent<ElementsProperties>();
                    GameObject.Find("GameManager").GetComponent<FireConditions>().OnElementDetected(elem);
                }
            }
        }

        GameObject ptcStarInstance;
        ptcStarInstance = Instantiate(ptcStarPrefab, transform.parent.position, Quaternion.identity);
        Destroy(ptcStarInstance, 8f);

        Destroy(transform.parent.gameObject);
    }
}
