using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


[RequireComponent (typeof(Queries))]
[RequireComponent (typeof(FireConditions))]
public class GameManagement : MonoBehaviour
{

    // Déclaration des variables
    // ====================================================
    public TextMeshProUGUI QueryTextUI;
    public TextMeshProUGUI TimeTextUI;
    public TextMeshProUGUI ScoreTextUI;
    public TextMeshProUGUI txtGrace;
    public GameObject canvasBonus;

    public int nNiveau = 1;
    [HideInInspector]public float time = 0f;
    public int nSeconds = 60;
    [HideInInspector] public bool isPlaying = false;
    public int maxScoreToWin = 7000;
    private float width = 1f;
    private float variabilite = 0f;
    private bool once1 = true;
    private bool once2 = true;
    private bool once3 = true;
    private int dialogue = 0;
    private int voice = 1;




    public GameObject Demarrage3;
    public GameObject Demarrage2;
    public GameObject Demarrage1;
    public GameObject DemarrageAreWeGood;
    public GameObject CadreQuery;
    public GameObject CadreScore;
    public AudioSource tic;
    public AudioSource coucou;
    public GameObject panel;
    public GameObject CadreFin;
    public RectTransform jauge;
    public GameObject wamp1;
    public GameObject wamp2;
    public GameObject wamp3;
    public ParticleSystem[] ptcConfettis;
    public AudioSource[] good;
    public GameObject btnBack;
    public AudioSource clic;
    public GameObject handGrace;
    public GameObject grace;
    public GameObject cloud1;
    public AudioSource bonus;
    public AudioSource voice1;
    public AudioSource voice2;
    public AudioSource voice3;
    public AudioSource voice4;
    public AudioSource voice5;


    private Queries cQueries;
    private FireConditions cFireConditions;
    private int QueryIndex;
    private int limitIndex;
    private int CoroutineIndexLimit;
    private List<Query> listQueries;
    // ====================================================

    public void disableCanvasBonus()
    {
        canvasBonus.SetActive(false);
        bonus.Play();
    }

    public void showCanvasBonus()
    {
        canvasBonus.SetActive(true);
    }

    private void Awake()
    {
        cQueries = gameObject.GetComponent<Queries>();
        cFireConditions = gameObject.GetComponent<FireConditions>();
    }

    //invoked when an element match conditions.
    public void OnElementDestroyed()
    {
        limitIndex++; //update the limit index
    }

    private void Start()
    {
        QueryIndex = 0;
        limitIndex = 0;
        CoroutineIndexLimit = 0;

        StartCoroutine(StartTimer());
        StartCoroutine(AlphaPanel());
        StartCoroutine(MoveHand());
    }

    private void SetOrderedQueries(List<Query> queries)
    {
        listQueries = queries;
        StartCoroutine(ChangeQuerie());
    }

    public void StartChangeQuerie()
    {
        print("Start to change the current querie.");
        StartCoroutine(ChangeQuerie());
    }

    public IEnumerator ChangeQuerie()
    {
        if(time > nSeconds)
        {
            yield break;
        }
        if (QueryIndex < listQueries.Count)
        {
            Debug.Log("Chargement de la Query n°"+(QueryIndex+1));
            if (listQueries[QueryIndex].Limit == 0)
            {
                StartCoroutine(FeedBackChangeQuery());

                Debug.Log("Query Temporelle " + listQueries[QueryIndex].Duration+"s");
                cFireConditions.conditions = listQueries[QueryIndex].conditions;
                if (QueryTextUI != null)
                {
                    print("Change text of Querie");
                    QueryTextUI.text = listQueries[QueryIndex].QueryText;
                }
                yield return new WaitForSeconds(listQueries[QueryIndex].Duration);
                QueryIndex++;
                limitIndex = 0;
                StartCoroutine(ChangeQuerie());
            }
            else
            {
                if (limitIndex < listQueries[QueryIndex].Limit) //gère la limit
                {
                    if (CoroutineIndexLimit == 0)
                    {
                        StartCoroutine(FeedBackChangeQuery());
                        if (QueryTextUI != null)
                        {
                            QueryTextUI.text = listQueries[QueryIndex].QueryText;
                        }
                        Debug.Log("Query Limit : " + listQueries[QueryIndex].Limit);
                        cFireConditions.conditions = listQueries[QueryIndex].conditions;
                    }
                    CoroutineIndexLimit++;
                    
                    yield return new WaitForSeconds(0.2f);
                    if (listQueries[QueryIndex].Duration < CoroutineIndexLimit * 0.2f)
                    {
                        CoroutineIndexLimit = 0;
                        QueryIndex++;
                        limitIndex = 0;
                    }
                        
                    StartCoroutine(ChangeQuerie());
                }
                else
                {
                    CoroutineIndexLimit = 0;
                    QueryIndex++;
                    limitIndex = 0;
                    StartCoroutine(ChangeQuerie());
                }
            }
            
            
        }
    }


    /// <summary>
    /// Fonction servant à implémenter le 3, 2, 1, GOOO !
    /// </summary>
    /// <returns></returns>
    IEnumerator StartTimer()
    {
        if(GameObject.Find("Tutoriel") != null)
        {
            GameObject.Find("Tutoriel").GetComponent<Tutoriel>().StartTutoriel();

            yield return new WaitForSeconds(11f);
        }

        yield return new WaitForSeconds(2f);

        // Démarrage
        // =================================================================================
        Demarrage3.transform.position = new Vector3(0f, 4.25f, -6f);
        LeanTween.moveY(Demarrage3, 2.5f, 0.3f);
        LeanTween.scale(Demarrage3, new Vector3(4f, 4f, 4f), 0.3f);
        Demarrage3.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.3f);

        LeanTween.moveY(Demarrage3, 3f, 0.5f);
        LeanTween.scale(Demarrage3, new Vector3(3f, 3f, 3f), 0.5f);
        LeanTween.alpha(Demarrage3, 0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        Demarrage2.transform.position = new Vector3(0f, 4.25f, -6f);
        LeanTween.moveY(Demarrage2, 2.5f, 0.3f);
        LeanTween.scale(Demarrage2, new Vector3(4f, 4f, 4f), 0.3f);
        Demarrage2.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.3f);

        LeanTween.moveY(Demarrage2, 3f, 0.5f);
        LeanTween.scale(Demarrage2, new Vector3(3f, 3f, 3f), 0.5f);
        LeanTween.alpha(Demarrage2, 0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        Demarrage1.transform.position = new Vector3(0f, 4.25f, -6f);
        LeanTween.moveY(Demarrage1, 2.5f, 0.3f);
        LeanTween.scale(Demarrage1, new Vector3(4f, 4f, 4f), 0.3f);
        Demarrage1.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.3f);

        LeanTween.moveY(Demarrage1, 3f, 0.5f);
        LeanTween.scale(Demarrage1, new Vector3(3f, 3f, 3f), 0.5f);
        LeanTween.alpha(Demarrage1, 0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        DemarrageAreWeGood.transform.position = new Vector3(0f, 4.25f, -6f);
        LeanTween.moveY(DemarrageAreWeGood, 2.5f, 0.3f);
        LeanTween.scale(DemarrageAreWeGood, new Vector3(1.5f, 1.5f, 1.5f), 0.3f);
        DemarrageAreWeGood.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.3f);

        LeanTween.moveY(DemarrageAreWeGood, 3f, 0.5f);
        LeanTween.scale(DemarrageAreWeGood, new Vector3(1f, 1f, 1f), 0.5f);
        LeanTween.alpha(DemarrageAreWeGood, 0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        Destroy(Demarrage1);
        Destroy(Demarrage2);
        Destroy(Demarrage3);
        LeanTween.moveY(DemarrageAreWeGood, 30f, 0.5f);
        // =================================================================================

        // Mettre en ordre les requête après
        Debug.Log("Début du "+ SceneManager.GetActiveScene().name);
        SetOrderedQueries(cQueries.DicoQueries[SceneManager.GetActiveScene().name]);

        StartCoroutine(Timer());
    }


    IEnumerator Timer()
    {

        isPlaying = true;
        ScoreTextUI.text = 0.ToString();
        gameObject.GetComponent<SpawnElements>().ActiveSpawn();

        while(time <= nSeconds)
        {
            yield return new WaitForSeconds(1f);
            time++;
            TimeTextUI.text = (nSeconds - time).ToString();

            if(TimeTextUI.text == "-1")
            {
                TimeTextUI.text = "FINITO !";
            }
        }

        // Fin du jeu !
        isPlaying = false;
        DemarrageAreWeGood.GetComponent<AudioSource>().Play();
        CheckBonus(int.Parse(ScoreTextUI.text));
        yield return new WaitForSeconds(3f);
        disableCanvasBonus();
        CadreFin.SetActive(true);
        LeanTween.scale(CadreFin.GetComponent<RectTransform>(), CadreFin.GetComponent<RectTransform>().localScale * 1.2f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        LeanTween.scale(CadreFin.GetComponent<RectTransform>(), CadreFin.GetComponent<RectTransform>().localScale / 1.2f, 0.5f);
        yield return new WaitForSeconds(0.3f);
        variabilite = maxScoreToWin / 650;
        good[0].Play();

        for (int cptScore = 0; cptScore < int.Parse(ScoreTextUI.text); cptScore+= 20)
        {
            width += (1/variabilite) * 20;
            jauge.sizeDelta = new Vector2(width, 107f);
            yield return new WaitForSeconds(0.0001f);

            if(width >= 200 && once1)
            {
                once1 = false;
                StartCoroutine(ActiveWamp1());
                dialogue = 1;
            }
            else if (width >= 350 && once2)
            {
                once2 = false;
                StartCoroutine(ActiveWamp2());
                dialogue = 2;
            }
            if (width >= 650 && once3)
            {
                once3 = false;
                StartCoroutine(ActiveWamp3());
                dialogue = 3;

                yield return new WaitForSeconds(1f);

                btnBack.SetActive(true);
                StartCoroutine(SeeCloud());
            }
        }

        if(once3)
        {
            btnBack.SetActive(true);
            StartCoroutine(SeeCloud());
        }
    }



    IEnumerator FeedBackChangeQuery()
    {
        LeanTween.scale(CadreQuery.GetComponent<RectTransform>(), CadreQuery.GetComponent<RectTransform>().localScale * 1.5f, 0.3f);
        coucou.Play();

        yield return new WaitForSeconds(0.3f);

        LeanTween.scale(CadreQuery.GetComponent<RectTransform>(), CadreQuery.GetComponent<RectTransform>().localScale / 1.5f, 0.5f);

        if(listQueries[QueryIndex].Duration != 0)
        {
            yield return new WaitForSeconds(listQueries[QueryIndex].Duration - 3f);

            LeanTween.rotate(CadreQuery.GetComponent<RectTransform>(), -7.5f, 0.3f);
            tic.Play();

            yield return new WaitForSeconds(0.3f);

            LeanTween.rotate(CadreQuery.GetComponent<RectTransform>(), 7.5f, 0.5f);

            yield return new WaitForSeconds(0.7f);

            LeanTween.rotate(CadreQuery.GetComponent<RectTransform>(), -7.5f, 0.3f);
            tic.Play();

            yield return new WaitForSeconds(0.3f);

            LeanTween.rotate(CadreQuery.GetComponent<RectTransform>(), 7.5f, 0.5f);

            yield return new WaitForSeconds(0.7f);

            LeanTween.rotate(CadreQuery.GetComponent<RectTransform>(), -7.5f, 0.3f);
            tic.Play();

            yield return new WaitForSeconds(0.3f);

            LeanTween.rotate(CadreQuery.GetComponent<RectTransform>(), 7.5f, 0.5f);
        }
    }



    public void AddScore()
    {
        ScoreTextUI.text = (int.Parse(ScoreTextUI.text) + 100).ToString();
    }

    public void Pactole()
    {
        ScoreTextUI.text = (int.Parse(ScoreTextUI.text) + 400).ToString();

        // Mise en place du choix des pouvoirs ici !
        showCanvasBonus();

        if(voice == 6)
        {
            voice = 1;
        }

        if (voice == 1)
        {
            voice1.Play();
        }
        else if (voice == 2)
        {
            voice2.Play();
        }
        else if (voice == 3)
        {
            voice3.Play();
        }
        else if (voice == 4)
        {
            voice4.Play();
        }
        else if (voice == 5)
        {
            voice5.Play();
        }

        voice++;
    }

    public void StartLowFeedbackScore()
    {
        StartCoroutine(LowFeedbackScore());
    }

    public void StartHighFeedbackScore()
    {
        StartCoroutine(HighFeedbackScore());
    }

    IEnumerator LowFeedbackScore()
    {
        LeanTween.scale(CadreScore.GetComponent<RectTransform>(), new Vector3(0.22f, 0.22f, 1f), 0.3f);

        yield return new WaitForSeconds(0.3f);

        LeanTween.scale(CadreScore.GetComponent<RectTransform>(), new Vector3(0.15f, 0.15f, 1f), 0.5f);
    }

    IEnumerator HighFeedbackScore()
    {
        LeanTween.scale(CadreScore.GetComponent<RectTransform>(), new Vector3(0.3f, 0.3f, 1f), 0.3f);

        yield return new WaitForSeconds(0.3f);

        LeanTween.scale(CadreScore.GetComponent<RectTransform>(), new Vector3(0.15f, 0.15f, 1f), 0.5f);
    }


    IEnumerator AlphaPanel()
    {
        LeanTween.alpha(panel.GetComponent<RectTransform>(), 1f, 0f);
        LeanTween.alpha(panel.GetComponent<RectTransform>(), 0f, 1f);

        yield return new WaitForSeconds(1f);

        panel.SetActive(false);
    }

    IEnumerator ActiveWamp1()
    {
        wamp1.SetActive(true);

        gameObject.GetComponent<CameraShake>().StartShakeExplosion();
        ptcConfettis[0].Play();
        ptcConfettis[1].Play();
        ptcConfettis[1].gameObject.GetComponent<AudioSource>().Play();
        good[1].Play();

        LeanTween.scale(wamp1.GetComponent<RectTransform>(), new Vector3(9f, 6f, 1f), 0.3f);
        LeanTween.alpha(wamp1.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.rotate(wamp1.GetComponent<RectTransform>(), 7.5f, 0.5f);

        yield return new WaitForSeconds(0.3f);

        LeanTween.scale(wamp1.GetComponent<RectTransform>(), new Vector3(4.5f, 3f, 1f), 0.5f);

        SaveScore(1);
    }

    IEnumerator ActiveWamp2()
    {
        wamp2.SetActive(true);

        gameObject.GetComponent<CameraShake>().StartShakeExplosion();
        ptcConfettis[2].Play();
        ptcConfettis[3].Play();
        ptcConfettis[3].gameObject.GetComponent<AudioSource>().Play();
        good[2].Play();

        LeanTween.scale(wamp2.GetComponent<RectTransform>(), new Vector3(10.5f, 6.5f, 1f), 0.3f);
        LeanTween.alpha(wamp2.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.rotate(wamp2.GetComponent<RectTransform>(), 7.5f, 0.5f);

        yield return new WaitForSeconds(0.3f);

        LeanTween.scale(wamp2.GetComponent<RectTransform>(), new Vector3(4.5f, 3f, 1f), 0.5f);

        SaveScore(2);
    }

    IEnumerator ActiveWamp3()
    {
        wamp3.SetActive(true);

        gameObject.GetComponent<CameraShake>().StartShakeExplosion();
        ptcConfettis[4].Play();
        ptcConfettis[5].Play();
        ptcConfettis[5].gameObject.GetComponent<AudioSource>().Play();
        good[3].Play();

        LeanTween.scale(wamp3.GetComponent<RectTransform>(), new Vector3(13.5f, 8.5f, 1f), 0.3f);
        LeanTween.alpha(wamp3.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.rotate(wamp3.GetComponent<RectTransform>(), 7.5f, 0.5f);

        yield return new WaitForSeconds(0.3f);

        LeanTween.scale(wamp3.GetComponent<RectTransform>(), new Vector3(4.5f, 3f, 1f), 0.5f);

        SaveScore(3);
    }


    public void PushBackButton()
    {
        StartCoroutine(Back());
    }

    IEnumerator Back()
    {
        ptcConfettis[0].Play();
        ptcConfettis[1].Play();
        ptcConfettis[0].gameObject.GetComponent<AudioSource>().Play();
        clic.Play();

        yield return new WaitForSeconds(3f);

        panel.SetActive(true);
        LeanTween.alpha(panel.GetComponent<RectTransform>(), 1, 1f);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("SelectLevel", LoadSceneMode.Single);
    }

    IEnumerator MoveHand()
    {
        LeanTween.rotateZ(handGrace, 10f, 1f);

        yield return new WaitForSeconds(1f);

        LeanTween.rotateZ(handGrace, -25f, 1f);

        yield return new WaitForSeconds(1f);

        StartCoroutine(MoveHand());
    }

    IEnumerator SeeCloud()
    {
        yield return new WaitForSeconds(1.5f);
       
        LeanTween.alpha(cloud1.GetComponent<RectTransform>(), 1f, 2f);
        LeanTween.alpha(grace, 1f, 2f);
        LeanTween.alpha(handGrace, 1f, 2f);

        yield return new WaitForSeconds(1f);

        if(dialogue == 0)
        {
            txtGrace.text = "Alors là j'vais essayer de pas faire mon Grâce Madembo ... ";
        } else if (dialogue == 1)
        {
            txtGrace.text = "C'est pas une leçon de morale, mais il fallait lire votre cours.";
        }
        else if (dialogue == 2)
        {
            txtGrace.text = "Je vois que vos révisions ont été sportives !";
        }
        else if (dialogue == 3)
        {
            txtGrace.text = "Que vous soyez en paix.";
        }
    }




    public void SaveScore(int score)
    {
        string nameSave = "Score" + nNiveau.ToString();

        if(score == 1 && PlayerPrefs.GetInt(nameSave) == 0)
        {
            PlayerPrefs.SetInt(nameSave, 1);
        }
        else if (score == 2 && PlayerPrefs.GetInt(nameSave) <= 1)
        {
            PlayerPrefs.SetInt(nameSave, 2);
        }
        else if (score == 3 && PlayerPrefs.GetInt(nameSave) <= 2)
        {
            PlayerPrefs.SetInt(nameSave, 3);
        }
    }


    public void CheckBonus(int score)
    {
        if(nNiveau == 10 && score >= 2500)
        {
            PlayerPrefs.SetInt("Succes1", 2);
        }

        if (nNiveau == 1 && score >= 15000)
        {
            PlayerPrefs.SetInt("Succes2", 2);
        }
        else if (nNiveau == 2 && score >= 7200)
        {
            PlayerPrefs.SetInt("Succes3", 2);
        }
        else if (nNiveau == 3 && score >= 24000)
        {
            PlayerPrefs.SetInt("Succes4", 2);
        }
        else if (nNiveau == 4 && score >= 5700)
        {
            PlayerPrefs.SetInt("Succes5", 2);
        }
        else if (nNiveau == 5 && score >= 10700)
        {
            PlayerPrefs.SetInt("Succes6", 2);
        }
        else if (nNiveau == 6 && score >= 9300)
        {
            PlayerPrefs.SetInt("Succes7", 2);
        }
        else if (nNiveau == 7 && score >= 12000)
        {
            PlayerPrefs.SetInt("Succes8", 2);
        }
        else if (nNiveau == 8 && score >= 15400)
        {
            PlayerPrefs.SetInt("Succes9", 2);
        }
        else if (nNiveau == 9 && score >= 7800)
        {
            PlayerPrefs.SetInt("Succes10", 2);
        }
        else if (nNiveau == 10 && score >= 13100)
        {
            PlayerPrefs.SetInt("Succes11", 2);
        }
    }
}
