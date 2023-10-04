using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class NicoAgent : Agent
{
    [Tooltip("Joint #1 - Arm")]
    public GameObject joint_1;
    [Tooltip("Joint #2 - Humerus")]
    public GameObject joint_2;
    [Tooltip("Joint #3 - Ulma")]
    public GameObject joint_3;
    [Tooltip("Joint #4 - NoIdea...")]
    public GameObject joint_4;
    [Tooltip("Joint #5 - Palm")]
    public GameObject joint_5;

    [Tooltip("Finger #1.1 - Index")]
    public GameObject index_finger_1;
    [Tooltip("Finger #1.2 - Index")]
    public GameObject index_finger_2;
    [Tooltip("Finger #1.3 - Index")]
    public GameObject index_finger_3;

    [Tooltip("Finger #2.1 - Middle")]
    public GameObject middle_finger_1;
    [Tooltip("Finger #2.2 - Middle")]
    public GameObject middle_finger_2;
    [Tooltip("Finger #2.3 - Middle")]
    public GameObject middle_finger_3;

    [Tooltip("Finger #3.1 - Pinky")]
    public GameObject pinky_finger_1;
    [Tooltip("Finger #3.2 - Pinky")]
    public GameObject pinky_finger_2;
    [Tooltip("Finger #3.3 - Pinky")]
    public GameObject pinky_finger_3;

    private Vector3 j1_rot;
    private Vector3 j2_rot;
    private Vector3 j3_rot;
    private Vector3 j4_rot;
    private Vector3 j5_rot;

    private Quaternion i1_rot;
    private Quaternion i2_rot;
    private Quaternion i3_rot;

    private Quaternion m1_rot;
    private Quaternion m2_rot;
    private Quaternion m3_rot;

    private Quaternion p1_rot;
    private Quaternion p2_rot;
    private Quaternion p3_rot;

    [Tooltip("The target object")]
    public GameObject target;

    [Tooltip("End effector")]
    public GameObject effector;

    private float last_dist;

    public override void Initialize()
    {
        base.Initialize();  
        j1_rot = joint_1.transform.localEulerAngles;
        j2_rot = joint_2.transform.localEulerAngles;
        j3_rot = joint_3.transform.localEulerAngles;
        j4_rot = joint_4.transform.localEulerAngles;
        j5_rot = joint_5.transform.localEulerAngles;
        last_dist = (target.transform.position - effector.transform.position).magnitude;
    }

    public override void OnEpisodeBegin()
    {
        //target.transform.position = new Vector3(Random.Range(0.2f, 0.4f) + 0.789f, 8.39f, Random.Range(-0.333f, 0.5f) - 7.294f);
        //target.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        joint_1.transform.localEulerAngles = j1_rot;
        joint_2.transform.localEulerAngles = j2_rot;
        joint_3.transform.localEulerAngles = j3_rot;
        joint_4.transform.localEulerAngles = j4_rot;
        joint_5.transform.localEulerAngles = j5_rot;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(joint_1.transform.localEulerAngles.y);
        sensor.AddObservation(joint_2.transform.localEulerAngles.z);
        sensor.AddObservation(joint_3.transform.localEulerAngles.x);
        sensor.AddObservation(joint_4.transform.localEulerAngles.y);
        sensor.AddObservation(joint_5.transform.localEulerAngles.x);

        sensor.AddObservation(target.transform.position - effector.transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float max_range = 1f;
        
        var action_0 = max_range * Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
        var action_1 = max_range * Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
        var action_2 = max_range * Mathf.Clamp(actions.ContinuousActions[2], -1f, 1f);
        var action_3 = max_range * Mathf.Clamp(actions.ContinuousActions[3], -1f, 1f);
        var action_4 = max_range * Mathf.Clamp(actions.ContinuousActions[4], -1f, 1f);

        Vector3 rot1 = joint_1.transform.localEulerAngles + new Vector3(0f, action_0, 0f);
        Vector3 rot2 = joint_2.transform.localEulerAngles + new Vector3(0f, 0f, action_1);
        Vector3 rot3 = joint_3.transform.localEulerAngles + new Vector3(action_2, 0f, 0f);
        Vector3 rot4 = joint_4.transform.localEulerAngles + new Vector3(0f, action_3, 0f);
        Vector3 rot5 = joint_5.transform.localEulerAngles + new Vector3(action_4, 0f, 0f);

        // transform rotations into proper range

        rot1.y = (rot1.y % 360);
        rot2.z = (rot2.z % 360);
        rot3.x = (rot3.x % 360);
        rot4.y = (rot4.y % 360);
        rot5.x = (rot5.x % 360);

        rot1.y = rot1.y - 360 * ((int)rot1.y / 180);
        rot2.z = rot2.z - 360 * ((int)rot2.z / 180);
        rot3.x = rot3.x - 360 * ((int)rot3.x / 180);
        rot4.y = rot4.y - 360 * ((int)rot4.y / 180);
        rot5.x = rot5.x - 360 * ((int)rot5.x / 180);

        joint_1.transform.localRotation = Quaternion.Euler(rot1.x, Mathf.Clamp(rot1.y, -90f, 90f), rot1.z);
        joint_2.transform.localRotation = Quaternion.Euler(rot2.x, rot2.y, Mathf.Clamp(rot2.z, -100f, 100f));
        joint_3.transform.localRotation = Quaternion.Euler(Mathf.Clamp(rot3.x, 0, 150), rot3.y, rot3.z);
        joint_4.transform.localRotation = Quaternion.Euler(rot4.x, Mathf.Clamp(rot4.y, -90f, 150f), rot4.z);
        joint_5.transform.localRotation = Quaternion.Euler(Mathf.Clamp(rot5.x, -60, 60), rot5.y, rot5.z);

        // joint_1.transform.Rotate(rot1, Space.Self);
        // joint_2.transform.Rotate(rot2, Space.Self);
        // joint_3.transform.Rotate(rot3, Space.Self);
        // joint_4.transform.Rotate(rot4, Space.Self);
        // joint_5.transform.Rotate(rot5, Space.Self);

        // float rew = 2 * Mathf.Exp(-(target.transform.position - effector.transform.position).magnitude) - 1;
        float new_dist = (target.transform.position - effector.transform.position).magnitude;
        float rew = last_dist - new_dist;
        last_dist = new_dist;
        AddReward(rew * 100);
    }

}
