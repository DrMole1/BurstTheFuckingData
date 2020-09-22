using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public GameObject title;
    public GameObject handGrace;
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;
    public GameObject textGrace;
    public GameObject handGrace2;
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
        StartCoroutine(GrowTitle());
        StartCoroutine(MoveHand());
        StartCoroutine(SeeCloud());
        StartCoroutine(MoveCloud());
        StartCoroutine(MoveHand2());
        ptcConfettis1.gameObject.GetComponent<AudioSource>().Play();
    }


    IEnumerator GrowTitle()
    {
        LeanTween.value(gameObject, 0.5f, 2f, 1f).setOnUpdate((float val) => {
            title.transform.localScale = new Vector2(val, val);
        });

        yield return new WaitForSeconds(1f);

        LeanTween.value(gameObject, 2f, 1.5f, 1f).setOnUpdate((float val) => {
            title.transform.localScale = new Vector2(val, val);
        });
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

    IEnumerator MoveHand2()
    {
        yield return new WaitForSeconds(7f);

        LeanTween.moveX(handGrace2, -4.8f, 2f);
    }


    public void PushBtnStart()
    {
        if(once)
        {
            StartCoroutine(ButtonStart());

            once = false;
        }
    }

    IEnumerator ButtonStart()
    {
        ptcConfettis3.Play();
        ptcConfettis4.Play();
        ptcConfettis1.gameObject.GetComponent<AudioSource>().Play();
        clic.Play();

        yield return new WaitForSeconds(1.5f);

        panel.SetActive(true);
        LeanTween.alpha(panel.GetComponent<RectTransform>(), 1, 1f);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("SelectLevel", LoadSceneMode.Single);
    }
}
