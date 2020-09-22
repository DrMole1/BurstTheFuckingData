using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    public List<string> Shapes;
    public List<string> Places;
    public List<string> Colors;
    public List<int> FillingPercentages;
    public string FillingVerificationType;

    public Condition(string shape, string place = null, string color = null, int fillingPercentage = 0, string fillingVerificationType = "")
    {
        Shapes = new List<string>();
        Places = new List<string>();
        Colors = new List<string>();
        FillingPercentages = new List<int>();
        FillingVerificationType = "";

        if (shape != null && shape != "" && shape!="all")
        {
            Shapes.Add(shape);
        }

        if (place != null && place != "" && place!="all")
        {
            Places.Add(place);
        }

        if (color != null && color != "" && color !="all")
        {
            Colors.Add(color);
        }

        if (fillingVerificationType!="")
        {
            SetFillingPercentages(fillingVerificationType, fillingPercentage);
        }
    }

    public Condition(int[] fillingPercentages, string fillingVerificationType, string shape = null, string place = null, string color = null)
    {
        Shapes = new List<string>();
        Places = new List<string>();
        Colors = new List<string>();
        FillingPercentages = new List<int>();
        FillingVerificationType = "";

        if (shape != null && shape != "" && shape!="all")
        {
            Shapes.Add(shape);
        }

        if (place != null && place != "" && place!="all")
        {
            Places.Add(place);
        }

        if (color != null && color != "" && color!="all")
        {
            Colors.Add(color);
        }

        switch (fillingPercentages.Length)
        {
            case 0:
                break;
            case 1:
                SetFillingPercentages(fillingVerificationType, fillingPercentages[0]);
                break;
            case 2:
                SetFillingPercentages(fillingPercentages[0], fillingPercentages[1], fillingVerificationType);
                break;
            default:
                Debug.LogWarning("Two many percentages value in this condition, please verify");
                break;
        }
    }
    public Condition(string[] shapes = null, string[] places = null, string[] colors = null, int[] fillingPercentages = null, string fillingVerificationType = "")
    {
        Shapes = new List<string>();
        Places = new List<string>();
        Colors = new List<string>();
        FillingPercentages = new List<int>();
        FillingVerificationType = "";
        if(shapes!= null)
        {
            foreach (string s in shapes)
            {
                if (s != null && s != "" && s != "all")
                {
                    Shapes.Add(s);
                }
            }
        }
        
        if(places != null)
        {
            foreach (string s in places)
            {
                if (s != null && s != "" && s != "all")
                {
                    Places.Add(s);
                }
            }
        }
        
        if(colors != null)
        {
            foreach (string s in colors)
            {
                if (s != null && s != "" && s != "all")
                    Colors.Add(s);
            }
        }
        
        if(fillingPercentages != null)
        {
            switch (fillingPercentages.Length)
            {
                case 0:
                    break;
                case 1:
                    SetFillingPercentages(fillingVerificationType, fillingPercentages[0]);
                    break;
                case 2:
                    SetFillingPercentages(fillingPercentages[0], fillingPercentages[1], fillingVerificationType);
                    break;
                default:
                    Debug.LogWarning("Two many percentages value in this condition, please verify");
                    break;
            }
        }
        
    }


    private bool IsFillingVerificationTypeCorrect(string type)
    {
        
        switch (type)
        {
            case ">": return true;
            case "<": return true;
            case "==": return true;
            case "=": return true;
            case ">=": return true;
            case "<=": return true;
            case "!=": return true;
            case "!": return true;
            default: return false;
        }
    }

    public void SetFillingPercentages(string verificationType, int value)
    {
        FillingVerificationType = verificationType.Replace(" ",""); //delete spaces
        if (IsFillingVerificationTypeCorrect(FillingVerificationType))
        {
            if (value >= 0 && value <= 100)
            {
                FillingPercentages.Add(value);
                this.FillingVerificationType = verificationType;
            }
            else
            {
                Debug.LogWarning("Filling Percentage condition is invalid, please verify value.");
            }
        }
        else
        {
            Debug.LogWarning("Filling Percentage condition is invalid, please verify type.");
        }
        
    }

    public void SetFillingPercentages(int value1, int value2, string verificationType="==")
    {
        
        FillingVerificationType = verificationType.Replace(" ", ""); //delete spaces
        if (IsFillingVerificationTypeCorrect(FillingVerificationType))
        {
            if (value1 >=0 && value1 <=100 && value2 >= 0 && value2 <= 100)
            {
                FillingPercentages.Add(value1);
                FillingPercentages.Add(value2);
                this.FillingVerificationType = verificationType;
            }
            else
            {
                Debug.LogWarning("Filling Percentage condition is invalid, please verify values.");
            }
            
           
        }
        else
        {
            Debug.LogWarning("Filling Percentage condition is invalid, please verify type.");
        }
    }

    public void ClearConditions()
    {
        Shapes.Clear();
        Places.Clear();
        Colors.Clear();
        FillingPercentages.Clear();
    }

    public bool IsVerifyingConditions(ElementsProperties elem)
    {
        return IsShapeCorrect(elem) && IsColorCorrect(elem) && IsPlaceCorrect(elem) && IsFillingPercentageCorrect(elem);

    }

    public bool IsShapeCorrect(ElementsProperties elem)
    {
        return Shapes.Contains(elem.Shape) || Shapes.Count == 0; //return true if the shapes contains the good string or if shapes is empty
    }

    public bool IsPlaceCorrect(ElementsProperties elem)
    {
        return Places.Contains(elem.Place) || Places.Count == 0;
    }

    public bool IsColorCorrect(ElementsProperties elem)
    {
        return Colors.Contains(elem.Color) || Colors.Count == 0;
    }

    public bool IsFillingPercentageCorrect(ElementsProperties elem)
    {
        bool result;

        switch (FillingPercentages.Count)
        {
            case 0: 
                result = true; 
                break;
            case 1:
                switch (FillingVerificationType)
                {
                    case ">": result = elem.FillingPercentage > FillingPercentages[0]; break;
                    case "<": result = elem.FillingPercentage < FillingPercentages[0]; break;
                    case "==": result = elem.FillingPercentage == FillingPercentages[0]; break;
                    case "=": result = elem.FillingPercentage == FillingPercentages[0]; break;
                    case ">=": result = elem.FillingPercentage >= FillingPercentages[0]; break;
                    case "<=": result = elem.FillingPercentage <= FillingPercentages[0]; break;
                    case "!=": result = elem.FillingPercentage != FillingPercentages[0]; break;
                    default:
                        result=false;
                        Debug.LogWarning("Error, Filling condition type is invalid.");
                        break;
                }
                ;break;
            case 2:
                switch (FillingVerificationType)
                {
                    case "==": result = elem.FillingPercentage >= FillingPercentages[0] && elem.FillingPercentage <= FillingPercentages[1]; break;
                    case "=": result = elem.FillingPercentage >= FillingPercentages[0] && elem.FillingPercentage <= FillingPercentages[1]; break;
                    case "!=": result = !(elem.FillingPercentage >= FillingPercentages[0] && elem.FillingPercentage <= FillingPercentages[1]); break;
                    case "!": result = !(elem.FillingPercentage >= FillingPercentages[0] && elem.FillingPercentage <= FillingPercentages[1]); break;
                    default:
                        result = false;
                        Debug.LogWarning("Error, Filling condition type is invalid.");
                        break;
                }
                ;break;
            default: 
                result = false;
                Debug.LogWarning("Error, too many Percentages in condition");
                break;
        }
        
        return result;
    }
}

