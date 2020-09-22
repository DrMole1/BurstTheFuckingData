using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectBehaviour : MonoBehaviour
{
    public GameObject handGrace;
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;
    public GameObject textGrace;
    public ParticleSystem ptcConfettis1;
    public ParticleSystem ptcConfettis2;
    public ParticleSystem ptcConfettis3;
    public ParticleSystem ptcConfettis4;
    public GameObject panel;
    public AudioSource clic;


    private bool once = true;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveHand());
        StartCoroutine(SeeCloud());
        StartCoroutine(MoveCloud());
        ptcConfettis1.gameObject.GetComponent<AudioSource>().Play();
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
        yield return new WaitForSeconds(4f);

        LeanTween.alpha(cloud1, 1f, 2f);
        LeanTween.alpha(cloud2, 1f, 2f);
        LeanTween.alpha(cloud3, 1f, 2f);
        LeanTween.alpha(textGrace, 1f, 2f);
    }

    IEnumerator MoveCloud()
    {
        yield return new WaitForSeconds(2f);

        LeanTween.moveY(cloud1, cloud1.transform.position.y - 0.6f, 2f);
        LeanTween.moveY(cloud2, cloud2.transform.position.y + 0.25f, 2f);
        LeanTween.moveY(cloud3, cloud3.transform.position.y - 0.25f, 2f);

        yield return new WaitForSeconds(2f);

        LeanTween.moveY(cloud1, cloud1.transform.position.y + 0.6f, 2f);
        LeanTween.moveY(cloud2, cloud2.transform.position.y - 0.25f, 2f);
        LeanTween.moveY(cloud3, cloud3.transform.position.y + 0.25f, 2f);

        StartCoroutine(MoveCloud());
    }

    public void SelectLevel(string nameScene)
    {
        if(once)
        {
            StartCoroutine(StartLevel(nameScene));
            once = false;
        }
    }

    IEnumerator StartLevel(string nameScene)
    {

        ptcConfettis3.Play();
        ptcConfettis4.Play();
        ptcConfettis1.gameObject.GetComponent<AudioSource>().Play();
        clic.Play();

        yield return new WaitForSeconds(3f);

        panel.SetActive(true);
        LeanTween.alpha(panel.GetComponent<RectTransform>(), 1, 1f);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(nameScene, LoadSceneMode.Single);
    }
}
