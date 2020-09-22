using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutoriel : MonoBehaviour
{
    public GameObject handGrace;
    public GameObject grace;
    public GameObject cloud1;
    public TextMeshProUGUI txtGrace;
    public string phrase1;
    public string phrase2;


    public void StartTutoriel()
    {
        StartCoroutine(GoTutoriel());
    }

    IEnumerator GoTutoriel()
    {
        LeanTween.alpha(cloud1.GetComponent<RectTransform>(), 1f, 2f);
        LeanTween.alpha(grace, 1f, 2f);
        LeanTween.alpha(handGrace, 1f, 2f);

        yield return new WaitForSeconds(0.5f);

        txtGrace.text = phrase1;

        yield return new WaitForSeconds(5f);

        txtGrace.text = phrase2;

        yield return new WaitForSeconds(5.5f);

        txtGrace.text = "";
        LeanTween.alpha(cloud1.GetComponent<RectTransform>(), 0f, 2f);
        LeanTween.alpha(grace, 0f, 2f);
        LeanTween.alpha(handGrace, 0f, 2f);
    }
}
