using System.Collections.Generic;
using UnityEngine;

public class DynamicRig : MonoBehaviour
{
	public AvatarController avatarController;
	public float thiccness = .1f;
	public List<GameObject> pool;
	public float zOffset = 1;
	public Material mat;

	// Update is called once per frame
	private void Update()
	{
		BodyTrackingData featureData = avatarController.client.GetData();
		if (featureData == null) return;

		CreateOrScaleCube(0, featureData.LeftShoulderPos, featureData.LeftElbowPos);
		CreateOrScaleCube(1, featureData.RightShoulderPos, featureData.RightElbowPos);
		CreateOrScaleCube(2, featureData.RightElbowPos, featureData.RightHandPos);
		CreateOrScaleCube(3, featureData.LeftKneePos, featureData.LeftFootPos);
		CreateOrScaleCube(4, featureData.RightKneePos, featureData.RightFootPos);
		CreateOrScaleCube(5, featureData.LeftHipPos, featureData.LeftKneePos);
		CreateOrScaleCube(6, featureData.RightHipPos, featureData.RightKneePos);
		CreateOrScaleCube(7, featureData.LeftHandPos, featureData.LeftIndexFingerPos);
		CreateOrScaleCube(8, featureData.RightHandPos, featureData.RightIndexFingerPos);
		CreateOrScaleCube(9, featureData.LeftHipPos, featureData.RightHipPos);
		CreateOrScaleCube(10, featureData.RightHipPos, featureData.RightShoulderPos);
		CreateOrScaleCube(11, featureData.LeftShoulderPos, featureData.RightShoulderPos);
		CreateOrScaleCube(12, featureData.LeftShoulderPos, featureData.LeftHipPos);
		CreateOrScaleCube(13, featureData.LeftElbowPos, featureData.LeftHandPos);
	}

	private void CreateOrScaleCube(int index, Vector3 start, Vector3 end)
	{
		if (pool.Count - 1 < index)
		{
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			obj.transform.SetParent(transform);
			obj.transform.localScale = Vector3.one * thiccness;
			Destroy(obj.GetComponent<Collider>());
			obj.GetComponent<MeshRenderer>().material = mat;

			pool.Add(obj);
		}

		start = avatarController.targets.head.transform.parent.TransformPoint(start);
		end = avatarController.targets.head.transform.parent.TransformPoint(end);

		start.z += zOffset;
		end.z += zOffset;

		pool[index].transform.position = (start + end) / 2f;
		pool[index].transform.LookAt(end);
		pool[index].transform.localScale = new Vector3(thiccness, thiccness, Vector3.Distance(start, end));
	}
}