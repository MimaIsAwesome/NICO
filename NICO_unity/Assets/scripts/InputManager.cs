using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    OrbitCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<OrbitCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cam.MoveHorizontal(true);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            cam.MoveHorizontal(false);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            cam.MoveVertical(true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            cam.MoveVertical(false);
        }
    }
}
