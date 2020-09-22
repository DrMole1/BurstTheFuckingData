using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampignonVertExplosion : MonoBehaviour
{
    public GameObject ptcChampiVertPrefab;
    public GameObject ptcChampiVertPrefab2;
    public float radius = 2.5f;


    public void LevelUp()
    {
        GameObject.Find("GameManager").GetComponent<GameManagement>().time -= 5;

        GameObject ptcChampiVertInstance;
        ptcChampiVertInstance = Instantiate(ptcChampiVertPrefab, transform.parent.position, Quaternion.identity);
        Destroy(ptcChampiVertInstance, 8f);

        GameObject ptcChampiVertInstance2;
        ptcChampiVertInstance2 = Instantiate(ptcChampiVertPrefab2, new Vector3(4.9f, 0.7f, -6.75f), Quaternion.identity);
        Destroy(ptcChampiVertInstance2, 8f);

        Destroy(transform.parent.gameObject);
    }
}
