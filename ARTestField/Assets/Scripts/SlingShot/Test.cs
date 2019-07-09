using System;
using UnityEngine;

public class Test : MonoBehaviour
{
	#region Variables
	public Vector2 offset;
	private Vector2 origin = new Vector2(1,1);
	public Quaternion originRotation;
	#endregion

	#region Initialization
	private void Start()
	{
		originRotation = transform.rotation;
	}
	#endregion

	#region Functionality
	public void CalculateAngle()
	{
		float angle = Vector2.Angle( origin, origin+offset );
		Debugger.DebugObject(this, $"Offset{origin+offset} Angle: {angle.ToString()}");
	}

	public void Rotate()
	{
		transform.Rotate(Vector3.up, 45);
	}

	public void ResetRotation()
	{
		transform.rotation = originRotation;
	}
	#endregion
}

