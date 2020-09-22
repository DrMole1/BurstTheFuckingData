using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampignonMegaExplosion : MonoBehaviour
{
    public GameObject ptcChampignonMegaPrefab;
    public float radius = 3.5f;
    public float growParameter = 2.2f;


    public void GrowUp()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                StartCoroutine(MakeBig(rb));
            }
        }

        GameObject ptcChampignonMegaInstance;
        ptcChampignonMegaInstance = Instantiate(ptcChampignonMegaPrefab, transform.parent.position, Quaternion.identity);
        Destroy(ptcChampignonMegaInstance, 8f);

        Destroy(transform.parent.gameObject);
    }


    IEnumerator MakeBig(Rigidbody rb)
    {
        Transform tf;

        tf = rb.gameObject.transform;

        if (tf.localScale.x <= 1.5f)
        {
            tf.localScale = new Vector3(tf.localScale.x * growParameter, tf.localScale.y * growParameter, tf.localScale.z * growParameter);

            yield return new WaitForSeconds(2f);

            tf = rb.gameObject.transform;

            tf.localScale = new Vector3(tf.localScale.x * growParameter, tf.localScale.y * growParameter, tf.localScale.z * growParameter);

            yield return new WaitForSeconds(2f);

            tf = rb.gameObject.transform;

            tf.localScale = new Vector3(tf.localScale.x * growParameter, tf.localScale.y * growParameter, tf.localScale.z * growParameter);
        }


        yield return new WaitForSeconds(2f);
    }
}
