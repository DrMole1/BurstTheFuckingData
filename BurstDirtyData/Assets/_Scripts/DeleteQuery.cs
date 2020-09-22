using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DeleteQuery : MonoBehaviour
{
    //Const
    public static string[] _Shapes = ElementLibrary._Shapes;
    public static readonly string[] _Places = ElementLibrary._Places;
    public static readonly string[] _Colors = ElementLibrary._Colors;
    public static readonly int[] _FillAmount = ElementLibrary._FillAmount;

    public static readonly string[] _PColors = ElementLibrary._PColors;
    public static readonly string[] _EColors = ElementLibrary._EColors;

    //Members

    [Header("Query")]
    public TextMeshProUGUI QueryTextUI;
    public string _Place;
    public Condition _Condition;
    public string _Query;
    private FireConditions fireConditions;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Niveau1")
        {
            _Shapes = new string[] { _Shapes[0] };
        }
        fireConditions = gameObject.GetComponent<FireConditions>();
        ChangeQuery();
    }

    public void ChangeQuery()
    {
        int[] r = new int[] { Random.Range(0, _Shapes.Length), Random.Range(0, _Places.Length), Random.Range(0, _Colors.Length), Random.Range(0, _FillAmount.Length) };
        this._Place = _Places[r[1]];

        this._Query = "DELETE FROM " + _PColors[r[1]] + this._Place.ToUpper() + "</mark>";

        int random;
        switch (Random.Range(0, 3))
        {
            case 0:
                random = Random.Range(0, _Shapes.Length);
                _Condition = new Condition(_Shapes[random],this._Place);
                this._Query += " WHERE SHAPE='" + _Shapes[random] + "';";
                break;
            case 1:
                random = Random.Range(0, _Colors.Length);
                _Condition = new Condition("", this._Place, _Colors[random]);
                this._Query += " WHERE COLOR='" + _PColors[random] + _Colors[random] + "</color>';";
                break;
            case 2:
                random = Random.Range(0, _FillAmount.Length);
                this._Condition = new Condition("", this._Place, "", _FillAmount[random], "=");
                this._Query += " WHERE FILLAMOUNT=" + _FillAmount[random] + ";";
                break;
        }

        QueryTextUI.text = _Query;
    }

    public void ApplyQuery()
    {
        ElementsProperties elem;
        GameObject[] gObjects = GameObject.FindGameObjectsWithTag("Element");
        foreach (GameObject g in gObjects)
        {
            elem = g.GetComponent<ElementsProperties>();
            if (elem != null && _Condition.IsVerifyingConditions(elem))
            {
                fireConditions.OnElementDetected(elem);
            }
        }
    }
}
