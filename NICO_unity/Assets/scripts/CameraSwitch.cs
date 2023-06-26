using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject main;
    public GameObject LHand;
    public GameObject Rhand;
    // Start is called before the first frame update

    public void ToggleMain()
    {
        main.SetActive(true);
        LHand.SetActive(false);
        Rhand.SetActive(false);
    }
    public void ToggleLeft()
    {
        main.SetActive(false);
        LHand.SetActive(true);
        Rhand.SetActive(false);
    }
    public void ToggleRight()
    {
        main.SetActive(false);
        LHand.SetActive(false);
        Rhand.SetActive(true);
    }
}
