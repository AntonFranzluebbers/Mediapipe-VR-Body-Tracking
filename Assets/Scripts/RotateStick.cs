using UnityEngine;
using unityutilities;

public class RotateStick : MonoBehaviour
{
	private Side side;


	private void Update()
	{
		if (!InputMan.ThumbstickIdle(side))
		{
			transform.Rotate(0, Time.deltaTime * InputMan.ThumbstickX(side), 0, Space.World);
			transform.Rotate(Time.deltaTime * InputMan.ThumbstickY(side), 0, 0, Space.Self);
		}
	}
}