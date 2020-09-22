using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POWExplosion : MonoBehaviour
{
    public float radius = 20f;
    public float force = 5000f;
    public GameObject ptcPOWPrefab;


    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        GameObject.Find("GameManager").GetComponent<GameManagement>().StartChangeQuerie();

        GameObject ptcPOWInstance;
        ptcPOWInstance = Instantiate(ptcPOWPrefab, transform.parent.position, Quaternion.identity);
        Destroy(ptcPOWInstance, 8f);

        StartCoroutine(MakeBig());
    }

    IEnumerator MakeBig()
    {
        Transform tf = transform.parent.transform;
        GameObject bloc = transform.parent.gameObject;

        LeanTween.scale(bloc, new Vector3(tf.localScale.x * 2, tf.localScale.y * 2, tf.localScale.z * 2), 0.1f);

        yield return new WaitForSeconds(0.1f);

        LeanTween.scale(bloc, new Vector3(tf.localScale.x / 1.5f, tf.localScale.y / 1.5f, tf.localScale.z / 1.5f), 0.02f);

        yield return new WaitForSeconds(0.02f);

        Destroy(transform.parent.gameObject);
    }
}
