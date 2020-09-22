using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlocExplosion : MonoBehaviour
{
    public GameObject ptcCoinBlocPrefab;
    public GameObject[] bonus;


    public void Explode()
    {

        GameObject ptcCoinBlocInstance;
        ptcCoinBlocInstance = Instantiate(ptcCoinBlocPrefab, transform.parent.position, Quaternion.identity);
        Destroy(ptcCoinBlocInstance, 8f);

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

        int randomBonus = Random.Range(0, bonus.Length);
        Instantiate(bonus[randomBonus], transform.parent.position, Quaternion.identity);

        Destroy(transform.parent.gameObject);
    }
}
