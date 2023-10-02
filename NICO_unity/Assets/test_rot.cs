using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_rot : MonoBehaviour
{
    [Range(-180f, 180f)]
    public float rotx, roty, rotz = 0f;
 
    public GameObject joint;

    // Start is called before the first frame update
    void Start()
    { 
        Vector3 rot = new Vector3(rotx, roty, rotz);
        Debug.Log(rot);
        Debug.Log(joint.transform.localEulerAngles);
        float x = joint.transform.localEulerAngles.x % 360;
        float y = joint.transform.localEulerAngles.y % 360;
        float z = joint.transform.localEulerAngles.z % 360;
        rotx = x - 360 * ((int)x / 180);
        roty = y - 360 * ((int)y / 180);
        rotz = z - 360 * ((int)z / 180);
        Vector3 rot2 = new Vector3(rotx, roty, rotz);
        Debug.Log(rot2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = new Vector3(rotx, roty, rotz);
        Debug.Log(rot);
        joint.transform.localRotation = Quaternion.Euler(rot);
    }
}
