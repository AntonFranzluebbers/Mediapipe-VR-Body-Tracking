using Newtonsoft.Json;
using UnityEngine;

public class AvatarCombiner : DataServer
{
	public AvatarController[] avatars;
	private BodyTrackingData combinedData;

	// Update is called once per frame
	private void Update()
	{
		combinedData = new BodyTrackingData
		{
			body = new Vector3[32]
		};
		for (int index = 0; index < avatars.Length; index++)
		{
			AvatarController a = avatars[index];
			BodyTrackingData data = a.client.GetData();
			if (data == null) continue;

			for (int i = 0; i < data.body.Length; i++)
			{
				Vector3 v = data.body[i];
				combinedData.body[i] += a.transform.TransformPoint(v);
			}
		}

		for (int i = 0; i < combinedData.body.Length; i++)
		{
			combinedData.body[i] /= avatars.Length;
		}

		LastPacketConverted = combinedData;
	}
}