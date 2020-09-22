using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBDD : MonoBehaviour
{
    public GameObject PanelMCD;
    public GameObject PanelMLD;
    public GameObject PanelDDL;
    public GameObject PanelSucces;


    public void ActivateMCD()
    {
        PanelMCD.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void ActivateMLD()
    {
        PanelMLD.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void ActivateDDL()
    {
        PanelDDL.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void ActivateSucces()
    {
        PanelSucces.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void DesactivateMCD()
    {
        PanelMCD.SetActive(false);
    }

    public void DesactivateMLD()
    {
        PanelMLD.SetActive(false);
    }

    public void DesactivateDDL()
    {
        PanelDDL.SetActive(false);
    }

    public void DesactivateSucces()
    {
        PanelSucces.SetActive(false);
        GetComponent<AudioSource>().Play();
    }
}
