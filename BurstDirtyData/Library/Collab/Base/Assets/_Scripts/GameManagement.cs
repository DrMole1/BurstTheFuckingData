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


    [HideInInspector]public float time = 0f;
    public int nSeconds = 60;
    [HideInInspector] public bool isPlaying = false;
    public int maxScoreToWin = 7000;
    private float width = 1f;
    private float variabilite = 0f;



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



    private Queries cQueries;
    private FireConditions cFireConditions;
    private int QueryIndex;
    private int limitIndex;
    private List<Query> listQueries;
    // ====================================================



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

        StartCoroutine(StartTimer());
        StartCoroutine(AlphaPanel());
    }

    private void SetOrderedQueries(List<Query> queries)
    {
        listQueries = queries;
        StartCoroutine(ChangeQuerie());
    }

    public IEnumerator ChangeQuerie()
    {
        if (QueryIndex < listQueries.Count)
        {
            StartCoroutine(FeedBackChangeQuery());

            Debug.Log("Chargement de la Query n°"+(QueryIndex+1));
            if (listQueries[QueryIndex].Limit == 0)
            {
                Debug.Log("Query Temporelle " + listQueries[QueryIndex].Duration+"s");
                cFireConditions.conditions = listQueries[QueryIndex].conditions;
                if (QueryTextUI != null)
                {
                    QueryTextUI.text = listQueries[QueryIndex].QueryText;
                }
                yield return new WaitForSeconds(listQueries[QueryIndex].Duration);
                QueryIndex++;
                limitIndex = 0;
                StartCoroutine(ChangeQuerie());
            }
            else if(limitIndex < listQueries[QueryIndex].Limit) //gère la limit
            {   
                if(limitIndex == 0)
                {
                    Debug.Log("Query Limit : " + listQueries[QueryIndex].Limit);
                    cFireConditions.conditions = listQueries[QueryIndex].conditions;
                    StartCoroutine(ChangeQuerie());
                }
            }
            else
            {
                if (QueryTextUI != null)
                {
                    QueryTextUI.text = listQueries[QueryIndex].QueryText;
                }
                yield return new WaitForSeconds(listQueries[QueryIndex].Duration);
                QueryIndex++;
                limitIndex = 0;
                StartCoroutine(ChangeQuerie());
            }
            
        }
    }


    /// <summary>
    /// Fonction servant à implémenter le 3, 2, 1, GOOO !
    /// </summary>
    /// <returns></returns>
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(2f);

        // Possible tutoriel

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
        if (SceneManager.GetActiveScene().name == "Niveau1")
        {
            Debug.Log("Début du niveau 1");
            SetOrderedQueries(cQueries.DicoQueries["Niveau1"]);
        }

        StartCoroutine(Timer());
    }


    IEnumerator Timer()
    {

        isPlaying = true;
        ScoreTextUI.text = 0.ToString();
        gameObject.GetComponent<SpawnElements>().ActiveSpawn();

        for(int i = 0; i <= nSeconds; i++)
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
        yield return new WaitForSeconds(3f);

        CadreFin.SetActive(true);
        LeanTween.scale(CadreFin.GetComponent<RectTransform>(), CadreFin.GetComponent<RectTransform>().localScale * 1.2f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        LeanTween.scale(CadreFin.GetComponent<RectTransform>(), CadreFin.GetComponent<RectTransform>().localScale / 1.2f, 0.5f);
        yield return new WaitForSeconds(0.3f);
        variabilite = maxScoreToWin / 650;

        for(int cptScore = 0; cptScore < int.Parse(ScoreTextUI.text); cptScore+= 15)
        {
            width += (1/variabilite) * 15;
            jauge.sizeDelta = new Vector2(width, 107f);
            yield return new WaitForSeconds(0.0001f);
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
}
