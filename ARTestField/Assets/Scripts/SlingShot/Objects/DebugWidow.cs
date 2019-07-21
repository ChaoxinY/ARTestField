using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugWidow : MonoBehaviour
{
	#region Variables
	public GameObject slingShotOriginPoint;
	public InputField bulletMassInputField;
	public InputField slingShotLaunchForceInputField;
	public InputField slingShotLaunchAngleInputField;
	public InputField slingShotAngleCounterWeightInputField;
	public InputField slingShotOriginXInputField;
	public InputField slingShotOriginYInputField;
	public InputField slingShotOriginZInputField;
	#endregion

	#region Functionality
	public void ChangeBulletMass()
	{
		StaticRefrences.bulletMass = GetFloadValueFromInputField(bulletMassInputField);
	}

	public void ChangeSlingShotLaunchForce()
	{
		StaticRefrences.slingShotMaximumLaunchForce = GetFloadValueFromInputField(slingShotLaunchForceInputField);
	}

	public void ChangeSlingShotAngleCounterWeight()
	{
		StaticRefrences.slingShotAngleCounterWeight = GetFloadValueFromInputField(slingShotAngleCounterWeightInputField);
	}

	public void ChangeSlingShotAngle()
	{
		StaticRefrences.slingShotLaunchAngle = GetFloadValueFromInputField(slingShotLaunchAngleInputField);
	}

	public void ChangeSlingShotOriginXValue()
	{
		StaticRefrences.slingShotOriginPoint.x = GetFloadValueFromInputField(slingShotOriginXInputField);
		UpdateslingShotOriginPoint();
	}

	public void ChangeSlingShotOriginYValue()
	{
		StaticRefrences.slingShotOriginPoint.y = GetFloadValueFromInputField(slingShotOriginYInputField);
		UpdateslingShotOriginPoint();
	}

	public void ChangeSlingShotOriginZValue()
	{
		StaticRefrences.slingShotOriginPoint.z  = GetFloadValueFromInputField(slingShotOriginZInputField);
		UpdateslingShotOriginPoint();
	}

	private float GetFloadValueFromInputField(InputField inputField)
	{
		float value = 0;
		float.Parse(inputField.text);
		return value;
	}

	private void UpdateslingShotOriginPoint()
	{
		slingShotOriginPoint.transform.localPosition = StaticRefrences.slingShotOriginPoint;
	}
	#endregion
}