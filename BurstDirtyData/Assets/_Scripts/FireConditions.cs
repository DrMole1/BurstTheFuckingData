using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManagement))]
public class FireConditions : MonoBehaviour
{

    // Déclaration des variables
    // ==================================================
    public List<Condition> conditions;

    public FireConditions()
    {
        conditions = new List<Condition>();
    }

    [Header("Prefab de feedbacks")]
    public GameObject ptcRedLowPrefab;
    public GameObject ptcRedHighPrefab;
    public GameObject ptcBlueLowPrefab;
    public GameObject ptcBlueHighPrefab;
    public GameObject ptcGreenLowPrefab;
    public GameObject ptcGreenHighPrefab;

    public GameObject ptcExplosionRedPrefab;
    public GameObject ptcExplosionBluePrefab;
    public GameObject ptcExplosionGreenPrefab;

    public GameObject ptcViscosityRedPrefab;
    public GameObject ptcViscosityBluePrefab;
    public GameObject ptcViscosityGreenPrefab;

    public GameObject ptcEvaporationPrefab;

    public GameObject ptcScore100Prefab;
    public GameObject ptcScore400Prefab;

    public AudioSource smash;
    public AudioSource good1;
    public AudioSource good2;
    public AudioSource good3;
    public AudioSource good4;




    private GameManagement gameManagement;
    private int combo = 0;

    // ==================================================


    private void Awake()
    {
        conditions = new List<Condition>();
        string[] shapes = { "rond", "carré" };
        int[] perc = {50};
        //conditions.Add(new Condition(shapes, default, default, perc, "=" ));
        conditions.Add(new Condition("all", "", "green"));

        gameManagement = gameObject.GetComponent<GameManagement>();
    }

    public bool IsMatchingConditions(ElementsProperties elem)
    {
        bool result = false;
        if (conditions.Count > 0)
        {
            foreach (Condition c in conditions)
            {
                result |= c.IsVerifyingConditions(elem);
            }
        }
        else
        {
            result = true;
        }
        
        return result;
    }

    private void Update()
    {
        // =================================================
        ElementsProperties elem;
        BobombExplosion bobomb = null;
        ChampignonExplosion champi = null;
        FleurExplosion fleur = null;
        ChampignonVertExplosion champiVert = null;
        POWExplosion POW = null;
        ChampignonMegaExplosion champiMega = null;
        StarExplosion star = null;
        CoinBlocExplosion coinBloc = null;
        // =================================================

        RaycastHit hit;
        Ray ray;

        if (Input.GetMouseButtonDown(0) && gameManagement.isPlaying)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9))
            {
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                elem = hit.transform.GetComponent<ElementsProperties>();
                if(hit.transform.gameObject.name == "BobOmb(Clone)")
                {
                    bobomb = hit.transform.GetChild(0).GetComponent<BobombExplosion>();
                }
                if (hit.transform.gameObject.name == "Champignon(Clone)")
                {
                    champi = hit.transform.GetChild(0).GetComponent<ChampignonExplosion>();
                }
                if (hit.transform.gameObject.name == "Fleur(Clone)")
                {
                    fleur = hit.transform.GetChild(0).GetComponent<FleurExplosion>();
                }
                if (hit.transform.gameObject.name == "ChampignonVert(Clone)")
                {
                    champiVert = hit.transform.GetChild(0).GetComponent<ChampignonVertExplosion>();
                }
                if (hit.transform.gameObject.name == "BlocPOW(Clone)")
                {
                    POW = hit.transform.GetChild(0).GetComponent<POWExplosion>();
                }
                if (hit.transform.gameObject.name == "ChampignonMega(Clone)")
                {
                    champiMega = hit.transform.GetChild(0).GetComponent<ChampignonMegaExplosion>();
                }
                if (hit.transform.gameObject.name == "Star(Clone)")
                {
                    star = hit.transform.GetChild(0).GetComponent<StarExplosion>();
                }
                if (hit.transform.gameObject.name == "CoinBloc(Clone)")
                {
                    coinBloc = hit.transform.GetChild(0).GetComponent<CoinBlocExplosion>();
                }

                if (elem)
                {
                    Debug.Log("The element is having an ElementsProperties component.");
                    OnElementDetected(elem);
                } 
                else if (bobomb)
                {
                    Debug.Log("The element is having an BobombExplosion component.");
                    bobomb.Explode();
                    gameObject.GetComponent<CameraShake>().StartBobombExplosion();
                }
                else if (champi)
                {
                    Debug.Log("The element is having an ChampignonExplosion component.");
                    champi.GrowUp();
                    gameObject.GetComponent<CameraShake>().StartBobombExplosion();
                }
                else if (fleur)
                {
                    Debug.Log("The element is having an FleurExplosion component.");
                    fleur.Fire();
                    gameObject.GetComponent<CameraShake>().StartBobombExplosion();
                }
                else if (champiVert)
                {
                    Debug.Log("The element is having an ChampignonVertExplosion component.");
                    champiVert.LevelUp();
                    gameObject.GetComponent<CameraShake>().StartBobombExplosion();
                }
                else if (POW)
                {
                    Debug.Log("The element is having an POWExplosion component.");
                    POW.Explode();
                    gameObject.GetComponent<CameraShake>().StartBobombExplosion();
                }
                else if (champiMega)
                {
                    Debug.Log("The element is having an ChampignonMegaExplosion component.");
                    champiMega.GrowUp();
                    gameObject.GetComponent<CameraShake>().StartBobombExplosion();
                }
                else if (star)
                {
                    Debug.Log("The element is having an StarExplosion component.");
                    star.Explode();
                    gameObject.GetComponent<CameraShake>().StartBobombExplosion();
                }
                else if (coinBloc)
                {
                    Debug.Log("The element is having an CoinBlocExplosion component.");
                    coinBloc.Explode();
                    gameObject.GetComponent<CameraShake>().StartBobombExplosion();
                }
            }
        }
    }

    public void OnElementDetected(ElementsProperties elem)
    {
        if (IsMatchingConditions(elem))
        {
            Debug.Log("Conditions match.");
            OnElementMatchConditions(elem);
        }
        else
        {
            GameObject ptcEvaporationInstance;
            ptcEvaporationInstance = Instantiate(ptcEvaporationPrefab, elem.gameObject.transform.position, Quaternion.Euler(elem.gameObject.transform.up));
            Destroy(ptcEvaporationInstance, 10f);

            Destroy(elem.gameObject);

            combo = 0;
        }
    }

    private void OnElementMatchConditions(ElementsProperties elem)
    {
        SpawnFluid(elem);
        gameObject.GetComponent<CameraShake>().StartShakeExplosion();
        smash.Play();

        gameManagement.AddScore();
        combo++;

        GameObject ptcScore100Instance;
        ptcScore100Instance = Instantiate(ptcScore100Prefab, elem.gameObject.transform.position, Quaternion.Euler(elem.gameObject.transform.up));
        Destroy(ptcScore100Instance, 5f);

        if(elem.gameObject.transform.localScale.x > 1.1f)
        {
            print("BONUS");
            gameManagement.AddScore();
            combo++;

            GameObject ptcScore100Instance2;
            ptcScore100Instance2 = Instantiate(ptcScore100Prefab, elem.gameObject.transform.position, Quaternion.Euler(elem.gameObject.transform.up));
            Destroy(ptcScore100Instance2, 5f);
        }

        if (combo >= 4)
        {
            gameManagement.StartHighFeedbackScore();
            gameManagement.Pactole();
            combo = 0;
            good4.Play();

            GameObject ptcScore400Instance;
            ptcScore400Instance = Instantiate(ptcScore400Prefab, elem.gameObject.transform.position, Quaternion.Euler(elem.gameObject.transform.up));
            Destroy(ptcScore400Instance, 5f);
        }
        else
        {
            gameManagement.StartLowFeedbackScore();
            if(combo == 3)
            {
                good3.Play();
            }
            else if (combo == 2)
            {
                good2.Play();
            }
            else if (combo == 1)
            {
                good1.Play();
            }
        }

        Destroy(elem.gameObject);
        gameManagement.OnElementDestroyed();
    }



    /// <summary>
    /// Permet de faire spawn un fluide coloré à l'explosion de l'élément
    /// </summary>
    /// <param name="elem">Le script de l'élément</param>
    private void SpawnFluid(ElementsProperties elem)
    {
        if(elem.Color == "red")
        {
            GameObject ptcExplosionRedInstance;
            ptcExplosionRedInstance = Instantiate(ptcExplosionRedPrefab, elem.gameObject.transform.position, Quaternion.Euler(elem.gameObject.transform.up));
            Destroy(ptcExplosionRedInstance, 10f);

            GameObject ptcViscosityRedInstance;
            ptcViscosityRedInstance = Instantiate(ptcViscosityRedPrefab, elem.gameObject.transform.position, Quaternion.Euler(elem.gameObject.transform.up));
            Destroy(ptcViscosityRedInstance, 10f);

            if (elem.FillingPercentage <= 50)
            {
                GameObject ptcRedLowInstance;
                ptcRedLowInstance = Instantiate(ptcRedLowPrefab, elem.gameObject.transform.position, Quaternion.identity);
                Destroy(ptcRedLowInstance, 10f);
            }
            else
            {
                GameObject ptcRedHighInstance;
                ptcRedHighInstance = Instantiate(ptcRedHighPrefab, elem.gameObject.transform.position, Quaternion.identity);
                Destroy(ptcRedHighInstance, 10f);
            }
        } 
        else if (elem.Color == "blue")
        {
            GameObject ptcExplosionBlueInstance;
            ptcExplosionBlueInstance = Instantiate(ptcExplosionBluePrefab, elem.gameObject.transform.position, Quaternion.Euler(elem.gameObject.transform.up));
            Destroy(ptcExplosionBlueInstance, 10f);

            GameObject ptcViscosityBlueInstance;
            ptcViscosityBlueInstance = Instantiate(ptcViscosityBluePrefab, elem.gameObject.transform.position, Quaternion.Euler(elem.gameObject.transform.up));
            Destroy(ptcViscosityBlueInstance, 10f);

            if (elem.FillingPercentage <= 50)
            {
                GameObject ptcBlueLowInstance;
                ptcBlueLowInstance = Instantiate(ptcBlueLowPrefab, elem.gameObject.transform.position, Quaternion.identity);
                Destroy(ptcBlueLowInstance, 10f);
            }
            else
            {
                GameObject ptcBlueHighInstance;
                ptcBlueHighInstance = Instantiate(ptcBlueHighPrefab, elem.gameObject.transform.position, Quaternion.identity);
                Destroy(ptcBlueHighInstance, 10f);
            }
        }
        else if (elem.Color == "green")
        {
            GameObject ptcExplosionGreenInstance;
            ptcExplosionGreenInstance = Instantiate(ptcExplosionGreenPrefab, elem.gameObject.transform.position, Quaternion.Euler(elem.gameObject.transform.up));
            Destroy(ptcExplosionGreenInstance, 10f);

            GameObject ptcViscosityGreenInstance;
            ptcViscosityGreenInstance = Instantiate(ptcViscosityGreenPrefab, elem.gameObject.transform.position, Quaternion.Euler(elem.gameObject.transform.up));
            Destroy(ptcViscosityGreenInstance, 10f);

            if (elem.FillingPercentage <= 50)
            {
                GameObject ptcGreenLowInstance;
                ptcGreenLowInstance = Instantiate(ptcGreenLowPrefab, elem.gameObject.transform.position, Quaternion.identity);
                Destroy(ptcGreenLowInstance, 10f);
            }
            else
            {
                GameObject ptcGreenHighInstance;
                ptcGreenHighInstance = Instantiate(ptcGreenHighPrefab, elem.gameObject.transform.position, Quaternion.identity);
                Destroy(ptcGreenHighInstance, 10f);
            }
        }
    }
}
