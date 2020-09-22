using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UpdateQuery : MonoBehaviour
{
    //Const
    public static string[] _Shapes = ElementLibrary._Shapes;
    public static readonly string[] _Places = ElementLibrary._Places;
    public static readonly string[] _Colors = ElementLibrary._Colors;
    public static readonly int[] _FillAmount = ElementLibrary._FillAmount;

    public static readonly string[] _PColors = ElementLibrary._PColors;
    public static readonly string[] _EColors = ElementLibrary._EColors;

    //Members

    private GameObject[] _elements;
    private Condition _GoodElementCondition;
    private GameObject _element;

    [Header("Query")]
    public TextMeshProUGUI QueryTextUI;
    public string _Shape;
    public string _Place;
    public string _Color;
    public int _FillingPercentage;
    public Condition _Condition;
    public string _Query;
    

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Niveau1")
        {
            _Shapes = new string[] { _Shapes[0] };
        }
        ChangeQuery();
    }

    public void ChangeQuery()
    {
        _elements = ElementLibrary._elements;
        int[] r = new int[] { Random.Range(0, _Shapes.Length), Random.Range(0,_Places.Length), Random.Range(0, _Colors.Length), Random.Range(0, _FillAmount.Length) };
        this._Shape = _Shapes[r[0]];
        this._Place = _Places[r[1]];
        this._Color = _Colors[r[2]];
        this._FillingPercentage = _FillAmount[r[3]];
        this._GoodElementCondition = new Condition(this._Shape, "", this._Color, this._FillingPercentage, "=");
        
        
        this._Query = "UPDATE " + _PColors[r[1]] + this._Place.ToUpper() + "</mark>" 
            + " SET SHAPE='"+ this._Shape + "', COLOR='"+_EColors[r[2]]+this._Color+"</color>"+"', FILLAMOUNT="+this._FillingPercentage;

        int random;
        switch (Random.Range(0, 3)){
            case 0:
                random = Random.Range(0, _Shapes.Length);
                this._Condition = new Condition(_Shapes[random], this._Place);
                this._Query += " WHERE SHAPE='"+ _Shapes[random] + "';";
                break;
            case 1:
                random = Random.Range(0, _Colors.Length);
                this._Condition = new Condition("", this._Place, _Colors[random]);
                this._Query += " WHERE COLOR='" +_PColors[random]+ _Colors[random] + "</color>';";
                break;
            case 2:
                random = Random.Range(0, _FillAmount.Length);
                this._Condition = new Condition("", this._Place, "",_FillAmount[random],"=");
                this._Query += " WHERE FILLAMOUNT=" + _FillAmount[random] + ";";
                break;
        }
        QueryTextUI.text = _Query;
    }

    public void ApplyQuery()
    {
        _element = ElementLibrary.SelectGoodElement(_GoodElementCondition);
        GameObject[] gObjects =  GameObject.FindGameObjectsWithTag("Element");
        Vector3 position;
        Quaternion rotation;
        foreach(GameObject g in gObjects)
        {
            if (_Condition.IsVerifyingConditions(g.GetComponent<ElementsProperties>()))
            {
                position = g.transform.position;
                rotation = g.transform.rotation;
                Destroy(g);
                Instantiate(_element, position, rotation);
            }
        }
    }
}
