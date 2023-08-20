using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class NicoAgent : Agent
{
    [Tooltip("Joint #1")]
    public GameObject joint_1;
    [Tooltip("Joint #2")]
    public GameObject joint_2;
    [Tooltip("Joint #3")]
    public GameObject joint_3;
    [Tooltip("Joint #4")]
    public GameObject joint_4;
    [Tooltip("Joint #5")]
    public GameObject joint_5;

    private Quaternion j1_rot;
    private Quaternion j2_rot;
    private Quaternion j3_rot;
    private Quaternion j4_rot;
    private Quaternion j5_rot;

    [Tooltip("The target object")]
    public GameObject target;

    [Tooltip("End effector")]
    public GameObject effector;


    public override void Initialize()
    {
        base.Initialize();  
        j1_rot = joint_1.transform.localRotation;
        j2_rot = joint_2.transform.localRotation;
        j3_rot = joint_3.transform.localRotation;
        j4_rot = joint_4.transform.localRotation;
        j5_rot = joint_5.transform.localRotation;
    }

    public override void OnEpisodeBegin()
    {
        target.transform.position = new Vector3(Random.Range(0.2f, 0.4f) + 0.789f, 8.39f, Random.Range(-0.333f, 0.5f) - 7.294f);
        target.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        joint_1.transform.localRotation = j1_rot;
        joint_2.transform.localRotation = j2_rot;
        joint_3.transform.localRotation = j3_rot;
        joint_4.transform.localRotation = j4_rot;
        joint_5.transform.localRotation = j5_rot;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(joint_1.transform.eulerAngles.normalized);
        sensor.AddObservation(joint_2.transform.eulerAngles.normalized);
        sensor.AddObservation(joint_3.transform.eulerAngles.normalized);
        sensor.AddObservation(joint_4.transform.eulerAngles.normalized);
        sensor.AddObservation(joint_5.transform.eulerAngles.normalized);

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
        var action_5 = max_range * Mathf.Clamp(actions.ContinuousActions[5], -1f, 1f);
        var action_6 = max_range * Mathf.Clamp(actions.ContinuousActions[6], -1f, 1f);
        var action_7 = max_range * Mathf.Clamp(actions.ContinuousActions[7], -1f, 1f);
        var action_8 = max_range * Mathf.Clamp(actions.ContinuousActions[8], -1f, 1f);
        var action_9 = max_range * Mathf.Clamp(actions.ContinuousActions[9], -1f, 1f);
        var action_10 = max_range * Mathf.Clamp(actions.ContinuousActions[10], -1f, 1f);
        var action_11 = max_range * Mathf.Clamp(actions.ContinuousActions[11], -1f, 1f);
        var action_12 = max_range * Mathf.Clamp(actions.ContinuousActions[12], -1f, 1f);
        var action_13 = max_range * Mathf.Clamp(actions.ContinuousActions[13], -1f, 1f);
        var action_14 = max_range * Mathf.Clamp(actions.ContinuousActions[14], -1f, 1f);



        joint_1.transform.Rotate(action_0, action_1, action_2, Space.World);
        joint_2.transform.Rotate(action_3, action_4, action_5, Space.World);
        joint_3.transform.Rotate(action_6, action_7, action_8, Space.World);
        joint_4.transform.Rotate(action_9, action_10, action_11, Space.World);
        joint_5.transform.Rotate(action_12, action_13, action_14, Space.World);


        float reward = 1f/(target.transform.position - effector.transform.position).magnitude;
        AddReward(reward/1000f);
    }

}
