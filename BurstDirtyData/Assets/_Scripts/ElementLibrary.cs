using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementLibrary
{
    //Const
    public static readonly string[] _Shapes = new string[] { "sphere", "capsule" };
    public static readonly string[] _Places = new string[] { "green", "pink", "purple", "yellow", };
    public static readonly string[] _Colors = new string[] { "red", "blue", "green" };
    public static readonly int[] _FillAmount = new int[] { 25, 50, 75, 100 };

    public static readonly string[] _PColors = new string[] { "<mark=#5AFF008F>", "<mark=#FF96FF8F>", "<mark=#FF49FF8F>", "<mark=#FFFF288F>" };
    public static readonly string[] _EColors = new string[] { "<color=#C73F3F>", "<color=#3F89C7>", "<color=#3FC740>" };

    //Members
    [Header("Elements")]
    public static GameObject[] _elements = null;
    public ElementLibrary(GameObject[] gameObjects)
    {
        //Debug.Log(gameObjects.Length);

        _elements = gameObjects;
        //Debug.Log(_elements.Length);
    }

    public static GameObject SelectGoodElement(Condition c)
    {
        GameObject element = null;
        Debug.Log(_elements.Length);
        string log = c.Shapes[0];
        if (c.Places.Count > 0)
        {
            log += c.Places[0];
        }
        log += c.Colors[0];
        if (c.Places.Count == 1)
        {
            log += c.FillingPercentages[0];
        }
        if (c.Places.Count == 2)
        {
            log += c.FillingPercentages[1];
        }
        log += c.FillingVerificationType;
        Debug.Log(log);
        foreach (GameObject e in _elements)
        {
            if (c.IsVerifyingConditions(e.GetComponent<ElementsProperties>()))
            {
                element = e;
                break;
            }
        }
        return element;
    }
}
