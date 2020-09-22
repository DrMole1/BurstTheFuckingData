using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{

    public float speed = 50f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.right * Time.deltaTime * speed);

        if (transform.position.x < -30)
        {
            Destroy(gameObject);
        }
    }
}
