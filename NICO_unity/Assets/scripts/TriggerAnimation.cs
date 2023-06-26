using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{

    public Animator nicoAnims;
    public Animator cubeAnims;
    public GameObject cube;
    public GameObject tmpcube;

    /*public GameObject idle;
    public GameObject happy;
    public GameObject sad;
    public GameObject shocked;*/

    public void TriggerCubeGrab()
    {
        /*idle.SetActive(true);
        happy.SetActive(false);
        shocked.SetActive(false);
        sad.SetActive(false);*/
        cube.SetActive(true);
        tmpcube.SetActive(false);
        cubeAnims.enabled = true;
        cubeAnims.Play("Base Layer.Cube|cube", -1, 0.0f);
        nicoAnims.Play("Base Layer.Armature|cube_grab", -1, 0.0f);
    }

    public void TriggerPinch()
    {
        /*idle.SetActive(false);
        happy.SetActive(true);
        shocked.SetActive(false);
        sad.SetActive(false);*/
        cube.SetActive(false);
        tmpcube.SetActive(true);
        nicoAnims.Play("Base Layer.Armature|pinch", -1, 0.0f);

    }

    public void TriggerSad()
    {
        /*idle.SetActive(false);
        happy.SetActive(false);
        shocked.SetActive(false);
        sad.SetActive(true);*/
        cube.SetActive(false);
        tmpcube.SetActive(true);
        nicoAnims.Play("Base Layer.Armature|sad", -1, 0.0f);
    }
    public void TriggerHappy()
    {
        /*idle.SetActive(false);
        happy.SetActive(true);
        shocked.SetActive(false);
        sad.SetActive(false);*/
        cube.SetActive(false);
        tmpcube.SetActive(true);
        nicoAnims.Play("Base Layer.Armature|happy", -1, 0.0f);
    }
    public void TriggerShocked()
    {
        /*idle.SetActive(false);
        happy.SetActive(false);
        shocked.SetActive(true);
        sad.SetActive(false);*/
        cube.SetActive(false);
        tmpcube.SetActive(true);
        nicoAnims.Play("Base Layer.Armature|shocked", -1, 0.0f);
    }
    public void TriggerWave()
    {
        //idle.SetActive(true);
        /*happy.SetActive(false);
        shocked.SetActive(false);
        sad.SetActive(false);*/
        cube.SetActive(false);
        tmpcube.SetActive(true);
        nicoAnims.Play("Base Layer.Armature|wave", -1, 0.0f);
    }

    private void Start()
    {
        TriggerWave();
    }

    private void Update()
    {
        if (nicoAnims.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !nicoAnims.IsInTransition(0))
        {
            /*idle.SetActive(true);
            happy.SetActive(false);
            sad.SetActive(false);
            shocked.SetActive(false);*/
        };
    }
}
