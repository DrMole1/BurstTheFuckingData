using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera cam;

    private bool isShaking = false;


    public void StartShakeExplosion()
    {
        StartCoroutine(ShakeExplosion());
    }


    IEnumerator ShakeExplosion()
    {
        if (isShaking == false)
        {
            isShaking = true;

            LeanTween.value(gameObject, 60f, 53f, 0.15f).setOnUpdate((float val) => {
                cam.fieldOfView = val;
            });

            yield return new WaitForSeconds(0.15f);

            LeanTween.value(gameObject, 53f, 60f, 0.3f).setOnUpdate((float val) => {
                cam.fieldOfView = val;
            });

            yield return new WaitForSeconds(0.3f);

            isShaking = false;
        }
    }


    public void StartBobombExplosion()
    {
        StartCoroutine(BobombExplosion());
    }


    IEnumerator BobombExplosion()
    {
        if (isShaking == false)
        {
            isShaking = true;

            LeanTween.value(gameObject, 60f, 63.5f, 0.15f).setOnUpdate((float val) => {
                cam.fieldOfView = val;
            });

            yield return new WaitForSeconds(0.15f);

            LeanTween.value(gameObject, 63.5f, 60f, 0.3f).setOnUpdate((float val) => {
                cam.fieldOfView = val;
            });

            yield return new WaitForSeconds(0.3f);

            isShaking = false;
        }
    }
}
