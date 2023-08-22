using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

public class RotateShoulder : MonoBehaviour
{
    [Range(-50f, 50f)]
    public float y;
    public GameObject shoulder;
    private Vector3 rotation;

    public float g_x;
    public float g_y;
    public float g_z;
    public GameObject empty_object;

    // Start is called before the first frame update
    void Start()
    {
        rotation = shoulder.transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rotation);
        Vector3 change = new Vector3(0.0f, y * Time.deltaTime, 0.0f);
        rotation += change;
        shoulder.transform.rotation = Quaternion.Euler(rotation);
        // shoulder.transform.Rotate(0, y * Time.deltaTime, 0, Space.Self);
        // empty_object.transform.localRotation = Quaternion.Euler(g_x, g_y, g_z);


    }
}
