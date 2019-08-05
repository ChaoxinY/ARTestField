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
		StaticRefrences.bulletMass = UtilityLibrary.GetFloatValueFromInputField(bulletMassInputField);
	}

	public void ChangeSlingShotLaunchForce()
	{
		StaticRefrences.slingShotMaximumLaunchForce = UtilityLibrary.GetFloatValueFromInputField(slingShotLaunchForceInputField);
	}

	public void ChangeSlingShotAngleCounterWeight()
	{
		StaticRefrences.slingShotAngleCounterWeight = UtilityLibrary.GetFloatValueFromInputField(slingShotAngleCounterWeightInputField);
	}

	public void ChangeSlingShotAngle()
	{
		StaticRefrences.slingShotLaunchAngle = -Mathf.Abs(UtilityLibrary.GetFloatValueFromInputField(slingShotLaunchAngleInputField));
	}

	public void ChangeSlingShotOriginXValue()
	{
		StaticRefrences.slingShotOriginPoint.x = UtilityLibrary.GetFloatValueFromInputField(slingShotOriginXInputField);
		UpdateslingShotOriginPoint();
	}

	public void ChangeSlingShotOriginYValue()
	{
		StaticRefrences.slingShotOriginPoint.y = UtilityLibrary.GetFloatValueFromInputField(slingShotOriginYInputField);
		UpdateslingShotOriginPoint();
	}

	public void ChangeSlingShotOriginZValue()
	{
		StaticRefrences.slingShotOriginPoint.z  = UtilityLibrary.GetFloatValueFromInputField(slingShotOriginZInputField);
		UpdateslingShotOriginPoint();
	}

	private void UpdateslingShotOriginPoint()
	{
		slingShotOriginPoint.transform.localPosition = StaticRefrences.slingShotOriginPoint;
	}
	#endregion
}