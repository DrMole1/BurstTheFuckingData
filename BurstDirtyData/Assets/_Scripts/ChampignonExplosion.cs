using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampignonExplosion : MonoBehaviour
{
    public GameObject ptcChampignonPrefab;
    public float radius = 2.5f;
    public float growParameter = 1.35f;


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

        GameObject ptcChampignonInstance;
        ptcChampignonInstance = Instantiate(ptcChampignonPrefab, transform.parent.position, Quaternion.identity);
        Destroy(ptcChampignonInstance, 8f);

        Destroy(transform.parent.gameObject);
    }


    IEnumerator MakeBig(Rigidbody rb)
    {
        Transform tf;
        Transform tfChild;

        tf = rb.gameObject.transform;
        tfChild = rb.gameObject.transform.GetChild(0).transform;

        if(tf.localScale.x <= 1.5f)
        {
            tf.localScale = new Vector3(tf.localScale.x * growParameter, tf.localScale.y * growParameter, tf.localScale.z * growParameter);
            tfChild.localScale = new Vector3(tfChild.localScale.x * growParameter, tfChild.localScale.y * growParameter, tfChild.localScale.z * growParameter);

            yield return new WaitForSeconds(2f);

            tf = rb.gameObject.transform;
            tfChild = rb.gameObject.transform.GetChild(0).transform;

            tf.localScale = new Vector3(tf.localScale.x * growParameter, tf.localScale.y * growParameter, tf.localScale.z * growParameter);
            tfChild.localScale = new Vector3(tfChild.localScale.x * growParameter, tfChild.localScale.y * growParameter, tfChild.localScale.z * growParameter);

            yield return new WaitForSeconds(2f);

            tf = rb.gameObject.transform;
            tfChild = rb.gameObject.transform.GetChild(0).transform;

            tf.localScale = new Vector3(tf.localScale.x * growParameter, tf.localScale.y * growParameter, tf.localScale.z * growParameter);
            tfChild.localScale = new Vector3(tfChild.localScale.x * growParameter, tfChild.localScale.y * growParameter, tfChild.localScale.z * growParameter);
        }

       
        yield return new WaitForSeconds(2f);
    }
}
