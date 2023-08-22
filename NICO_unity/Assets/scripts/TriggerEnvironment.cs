using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnvironment : MonoBehaviour
{
    public GameObject lirkisLab;
    public GameObject toyOffice;

    public void toggleLirkisLab()
    {
        lirkisLab.SetActive(true);
        toyOffice.SetActive(false);
    }
    public void toggleToyOffice()
    {
        lirkisLab.SetActive(false);
        toyOffice.SetActive(true);
    }
}
