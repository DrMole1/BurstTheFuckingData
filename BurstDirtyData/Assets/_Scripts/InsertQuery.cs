using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class InsertQuery : MonoBehaviour
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

    public TextMeshProUGUI QueryTextUI;
    public float SpawnDuration = 2f;
    private string _Shape;
    private string _Color;
    private int _FillingPercentage;
    public string _Query;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Niveau1")
        {
            _Shapes = new string[] {_Shapes[0] };
        }
        ChangeQuery();
    }
        
    public void ChangeQuery()
    {
        _elements = ElementLibrary._elements;
        int[] r = new int[] { Random.Range(0, _Shapes.Length), 0, Random.Range(0, _Colors.Length), Random.Range(0, _FillAmount.Length) };
        this._Shape = _Shapes[r[0]];
        this._Color = _Colors[r[2]];
        this._FillingPercentage = _FillAmount[r[3]];
        this._GoodElementCondition = new Condition(this._Shape, "", this._Color, this._FillingPercentage, "=");
        this._Query = "INSERT INTO " + _PColors[r[1]] + "GREEN" + "</mark>" + " (SHAPE, COLOR, FILLAMOUNT) VALUES ('" + this._Shape + "', '" + _EColors[r[2]] + this._Color + "</color>" + "', " + this._FillingPercentage + ");";

        QueryTextUI.text = _Query;
    }


    public void ApplyQuery()
    {
        _element = ElementLibrary.SelectGoodElement(_GoodElementCondition);
        gameObject.GetComponent<SpawnElements>().SpawnElement(_element, SpawnDuration);
    }

}
