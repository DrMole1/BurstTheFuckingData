using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackground : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public float delay = 7f;


    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Instantiate(backgroundPrefab, transform.position, Quaternion.identity);

        //print(Time.time);
        yield return new WaitForSeconds(delay);
        //print(Time.time);
        StartCoroutine(Spawn());
    }
}
