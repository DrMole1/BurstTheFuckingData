using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsProperties : MonoBehaviour
{
    public string Shape;
    public string Place;
    public string Color;
    public int FillingPercentage;

    private GameObject Green;
    private GameObject Purple;
    private GameObject Rose;
    private GameObject Yellow;

    private Collider[] colliders;


    // Start is called before the first frame update
    void Start()
    {
        Green = GameObject.Find("Green");
        Purple = GameObject.Find("Purple");
        Rose = GameObject.Find("Rose");
        Yellow = GameObject.Find("Yellow");

    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics.OverlapSphere(this.transform.position, 0.2f);
        if(colliders.Length > 0)
        {
            foreach (Collider c in colliders)
            {
                //Debug.Log("Overlap avec"+ c.gameObject.name);
                if(c.gameObject == Rose)
                {
                    Place = "pink";
                }else if (c.gameObject == Yellow)
                {
                    Place = "yellow";
                }else if(c.gameObject == Purple)
                {
                    Place = "purple";
                }else if (c.gameObject == Green)
                {
                    Place = "green";
                }
            }
        }
    }
}
