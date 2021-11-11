using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

[Serializable]
public class BodyTrackingData
{
	public Vector3[] face;
	public Vector3[] left_hand;
	public Vector3[] right_hand;
	public Vector3[] body;

	public Vector3 HeadPos => body[0];
	public Vector3 HeadForward => body[0] - (body[7] + body[8]) / 2;
	public Vector3 LeftHandPos => body[15];
	public Vector3 RightHandPos => body[16];
	public Vector3 LeftElbowPos => body[13];
	public Vector3 RightElbowPos => body[14];
	public Vector3 HipPos => (body[23] + body[24]) / 2;
	public Vector3 LeftFootPos => body[27];
	public Vector3 RightFootPos => body[28];
	public Vector3 LeftKneePos => body[25];
	public Vector3 RightKneePos => body[26];
	public Vector3 LeftShoulderPos => body[11];
	public Vector3 RightShoulderPos => body[12];
	public Vector3 LeftHipPos => body[23];
	public Vector3 RightHipPos => body[24];
	public Vector3 LeftIndexFingerPos => body[19];
	public Vector3 RightIndexFingerPos => body[20];
}

public class DataServer : MonoBehaviour
{
	private string lastPacket = "";
	private int lastPacketConverted = 0;
	private BodyTrackingData lastConvertedPacket;

	// TODO cache the json convert
	public string LastPacket = "";
	public BodyTrackingData LastPacketConverted = null;

	public BodyTrackingData GetData()
	{
		// if (LastPacketConverted != null)
		// {
		// 	return LastPacketConverted;
		// }
		if (LastPacket != "")
		{
			return JsonUtility.FromJson<BodyTrackingData>(LastPacket);
		}
		else
		{
			return null;
		}
	}
}