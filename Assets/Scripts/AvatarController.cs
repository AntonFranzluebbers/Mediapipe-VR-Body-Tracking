using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unityutilities;
using RootMotion.FinalIK;
using System.Reflection;

[System.Serializable]
public class IKTargets
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    public Transform pelvis;
    public Transform chest;
    public Transform leftFoot;
    public Transform rightFoot;
    public Transform leftKnee;
    public Transform rightKnee;
    public Transform leftElbow;
    public Transform rightElbow;
    public Transform leftShoulder;
    public Transform rightShoulder;
}

public class AvatarController : MonoBehaviour
{
    public Rig rig;
    public VRIK avatar;
    public IKTargets targets;
    /// <summary>
    /// The parent object for all the IK targets
    /// </summary>
    private GameObject ikTargetsObject;
    
    public ShowMesh meshVisualizer;
    public DataServer client;


    // Start is called before the first frame update
    private void Start()
    {
        ikTargetsObject = new GameObject("IK Targets");
        ikTargetsObject.transform.SetParent(transform);
        ikTargetsObject.transform.localPosition = Vector3.zero;
        ikTargetsObject.transform.localScale = new Vector3(1, 1, 1);
        
        // reflection. very gross
        // adds a cube for every field in IKTargets
        FieldInfo[] fields = typeof(IKTargets).GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.name = field.Name;
            obj.transform.SetParent(ikTargetsObject.transform);
            Destroy(obj.GetComponent<Collider>());
            obj.transform.localScale = Vector3.one * 0.1f;
            obj.GetComponent<MeshRenderer>().material.color = Color.red;
            targets.GetType().GetField(field.Name).SetValue(targets, obj.transform);
        }


        // set the IK solver targets to the appropriate objects
        avatar.solver.spine.headTarget = targets.head;
        avatar.solver.spine.pelvisTarget = targets.pelvis;

        avatar.solver.leftArm.target = targets.leftHand;
        avatar.solver.leftArm.bendGoal = targets.leftElbow;
        avatar.solver.leftLeg.target = targets.leftFoot;
        avatar.solver.leftLeg.bendGoal = targets.leftKnee;

        avatar.solver.rightArm.target = targets.rightHand;
        avatar.solver.rightArm.bendGoal = targets.rightElbow;
        avatar.solver.rightLeg.target = targets.rightFoot;
        avatar.solver.rightLeg.bendGoal = targets.rightKnee;
    }

    // Update is called once per frame
    private void Update()
    {
        BodyTrackingData data = client.GetData();
        if (data == null) return;

        // ikTargetsObject.transform.rotation = rig.head.rotation * Quaternion.Inverse(Quaternion.LookRotation(data.HeadForward));
        
        // meshVisualizer.transform.position = rig.head.position - data.HeadPos;
        transform.Translate(rig.head.position - transform.TransformPoint(data.HeadPos), Space.World);

        // targets.head.position = rig.head.position;
        // targets.leftHand.position = rig.leftHand.position;
        // targets.rightHand.position = rig.rightHand.position;


        targets.head.localPosition = data.HeadPos;
        targets.head.localRotation = Quaternion.LookRotation(data.HeadForward, Vector3.up);
        targets.pelvis.localPosition = data.HipPos;

        targets.leftHand.localPosition = data.LeftHandPos;
        targets.leftElbow.localPosition = data.LeftElbowPos;
        targets.leftKnee.localPosition = data.LeftKneePos;
        targets.leftFoot.localPosition = data.LeftFootPos;
        
        targets.rightHand.localPosition = data.RightHandPos;
        targets.rightElbow.localPosition = data.RightElbowPos;
        targets.rightKnee.localPosition = data.RightKneePos;
        targets.rightFoot.localPosition = data.RightFootPos;

    }
}
