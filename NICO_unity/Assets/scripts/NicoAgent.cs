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

    private float rotationLimit = 7f;

    // Allows for smoother changes
    private float smoothJoint1Change = 0f;
    private float smoothJoint2Change = 0f;
    private float smoothJoint3Change = 0f;
    private float smoothJoint4Change = 0f;
    private float smoothJoint5Change = 0f;

    private float rotationSpeed = 1f;

    [Tooltip("The target object")]
    public GameObject target;

    [Tooltip("End effector")]
    public GameObject effector;

    private float last_dist;

    public override void Initialize()
    {
        base.Initialize();  
        j1_rot = joint_1.transform.localRotation;
        j2_rot = joint_2.transform.localRotation;
        j3_rot = joint_3.transform.localRotation;
        j4_rot = joint_4.transform.localRotation;
        j5_rot = joint_5.transform.localRotation;


        last_dist = (target.transform.position - effector.transform.position).magnitude;
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


        smoothJoint1Change = 0f;
        smoothJoint2Change = 0f;
        smoothJoint3Change = 0f;
        smoothJoint4Change = 0f;
        smoothJoint5Change = 0f;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(joint_1.transform.localEulerAngles.x);
        sensor.AddObservation(joint_2.transform.localEulerAngles.y);
        sensor.AddObservation(joint_3.transform.localEulerAngles.z);
        sensor.AddObservation(joint_4.transform.localEulerAngles.x);
        sensor.AddObservation(joint_5.transform.localEulerAngles.y);

        sensor.AddObservation(target.transform.position - effector.transform.position);
        // Szép et al. suggest adding the position of the target and the end effector separatelly
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //smoothJoint1Change = Mathf.MoveTowards(smoothJoint1Change, actions.ContinuousActions[0], 2f * Time.fixedDeltaTime);
        //smoothJoint2Change = Mathf.MoveTowards(smoothJoint2Change, actions.ContinuousActions[1], 2f * Time.fixedDeltaTime);
        //smoothJoint3Change = Mathf.MoveTowards(smoothJoint3Change, actions.ContinuousActions[2], 2f * Time.fixedDeltaTime);
        //smoothJoint4Change = Mathf.MoveTowards(smoothJoint4Change, actions.ContinuousActions[3], 2f * Time.fixedDeltaTime);
        //smoothJoint5Change = Mathf.MoveTowards(smoothJoint5Change, actions.ContinuousActions[4], 2f * Time.fixedDeltaTime);

        smoothJoint1Change = smoothJoint1Change + Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
        smoothJoint2Change = smoothJoint2Change + Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
        smoothJoint3Change = smoothJoint3Change + Mathf.Clamp(actions.ContinuousActions[2], -1f, 1f);
        smoothJoint4Change = smoothJoint4Change + Mathf.Clamp(actions.ContinuousActions[3], -1f, 1f);
        smoothJoint5Change = smoothJoint5Change + Mathf.Clamp(actions.ContinuousActions[4], -1f, 1f);


        smoothJoint1Change = Mathf.Clamp(smoothJoint1Change, -rotationLimit, rotationLimit);
        smoothJoint2Change = Mathf.Clamp(smoothJoint2Change, -rotationLimit, rotationLimit);
        smoothJoint3Change = Mathf.Clamp(smoothJoint3Change, -rotationLimit, rotationLimit);
        smoothJoint4Change = Mathf.Clamp(smoothJoint4Change, -rotationLimit, rotationLimit);
        smoothJoint5Change = Mathf.Clamp(smoothJoint5Change, -rotationLimit, rotationLimit);

        joint_1.transform.Rotate(0f, smoothJoint1Change * Time.deltaTime * rotationSpeed, 0f, Space.Self);
        joint_2.transform.Rotate(smoothJoint2Change * Time.deltaTime * rotationSpeed, 0f, 0f, Space.Self);
        joint_3.transform.Rotate(0f, 0f, smoothJoint3Change * Time.deltaTime * rotationSpeed, Space.Self);
        joint_4.transform.Rotate(0f, smoothJoint4Change * Time.deltaTime * rotationSpeed, 0f, Space.Self);
        joint_5.transform.Rotate(smoothJoint5Change * Time.deltaTime * rotationSpeed, 0f, 0f, Space.Self);

        // float rew = 2 * Mathf.Exp(-(target.transform.position - effector.transform.position).magnitude) - 1;

        float new_dist = (target.transform.position - effector.transform.position).magnitude;

        //Debug.Log(last_dist - new_dist);
        //Debug.Log(new_dist);

        float rew = last_dist - new_dist;
        last_dist = new_dist;
        AddReward(rew * 100 + 0.1f/new_dist);

        // threshold cca 0.0005
        // min dist 0.1, max dist 2.5
    }

}
