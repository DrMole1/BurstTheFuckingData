using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSave : MonoBehaviour
{
    public int nMaxNiveau = 1;
    public GameObject[] iconeLevel;
    public GameObject[] trophee;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nMaxNiveau; i++)
        {
            string nameSave = "Score" + (i + 1).ToString();

            if (PlayerPrefs.GetInt(nameSave) >= 1)
            {
                iconeLevel[i].transform.GetChild(0).gameObject.SetActive(true);

                if(i < nMaxNiveau - 1)
                {
                    iconeLevel[i + 1].SetActive(true);
                }
              
            }
            if (PlayerPrefs.GetInt(nameSave) >= 2)
            {
                iconeLevel[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt(nameSave) == 3)
            {
                iconeLevel[i].transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        if (PlayerPrefs.GetInt("Succes1", 0) >= 1)
        {
            trophee[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Succes2", 0) >= 1)
        {
            trophee[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Succes3", 0) >= 1)
        {
            trophee[2].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Succes4", 0) >= 1)
        {
            trophee[3].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Succes5", 0) >= 1)
        {
            trophee[4].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Succes6", 0) >= 1)
        {
            trophee[5].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Succes7", 0) >= 1)
        {
            trophee[6].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Succes8", 0) >= 1)
        {
            trophee[7].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Succes9", 0) >= 1)
        {
            trophee[8].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Succes10", 0) >= 1)
        {
            trophee[9].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Succes11", 0) >= 1)
        {
            trophee[10].SetActive(true);
        }
    }
}

