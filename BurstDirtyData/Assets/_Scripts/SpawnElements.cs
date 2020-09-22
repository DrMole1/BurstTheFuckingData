using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElements : MonoBehaviour
{
    [Header("Elements")]
    public GameObject[] elements;
    [Header("Point of spawn")]
    public Transform[] pointOfSpawn;
    [Header("Paramètres de spawn")]
    public float delay = 0.3f;
    public int nElementsPerSpawn = 1;
    [Range(1, 3)]
    public int nPointOfSpawn = 2;
    public GameObject[] bonus;
    public int rateSpawnBonus = 0;

    private int niveau = 0;
    private int currentRateBonus = 0;

    //Time when insert query starts
    private float timeSpawnCallStart; //spawn with duration parameter
    //Initializing class ElmentLibrary, to allow access to all element prefab.
    private static ElementLibrary e;



    private void Start()
    {
        e = new ElementLibrary(elements);
        niveau = GetComponent<GameManagement>().nNiveau;
    }

    public void ActiveSpawn()
    {
        StartCoroutine(Spawn());
    }

    
    IEnumerator Spawn()
    {
        for(int i = 0; i < nPointOfSpawn; i++)
        {
            int numberSpawn = Random.Range(0, 3);

            for(int j = 0; j < nElementsPerSpawn; j++)
            {
                int numberElement = Random.Range(0, elements.Length);

                GameObject elementInstance;
                elementInstance = Instantiate(elements[numberElement], pointOfSpawn[numberSpawn].position, Quaternion.identity, transform);

            }
        }

        if(rateSpawnBonus != 0)
        {
            currentRateBonus++;
            
            if(currentRateBonus == rateSpawnBonus)
            {
                currentRateBonus = 0;

                int randomBonus = Random.Range(0, bonus.Length);
                Instantiate(bonus[randomBonus], pointOfSpawn[1].position, Quaternion.identity, transform);
            }
        }

        yield return new WaitForSeconds(delay);

        if(gameObject.GetComponent<GameManagement>().isPlaying)
        {
            StartCoroutine(Spawn());
        }
    }

    public void SpawnElement(GameObject element, float duration)
    {
        timeSpawnCallStart = gameObject.GetComponent<GameManagement>().time;
        StartCoroutine(Spawn(element, duration));
    }

    IEnumerator Spawn(GameObject element, float duration)
    {
        for (int i = 0; i < nPointOfSpawn; i++)
        {
            int numberSpawn = Random.Range(0, 3);

            for (int j = 0; j < (nElementsPerSpawn*2); j++)
            {
                GameObject elementInstance;
                elementInstance = Instantiate(element, pointOfSpawn[numberSpawn].position, Quaternion.identity, transform);

            }
        }

        yield return new WaitForSeconds(delay*0.5f);

        if (gameObject.GetComponent<GameManagement>().time < timeSpawnCallStart + duration)
        {
            StartCoroutine(Spawn(element, duration));
        }
    }




}
