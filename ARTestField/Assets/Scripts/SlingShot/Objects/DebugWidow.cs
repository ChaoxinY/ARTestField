using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugWidow : MonoBehaviour
{
	#region Variables
	[SerializeField]
	private GameObject slingShotOriginPoint;
	[SerializeField]
	private InputField bulletMassInputField;
	[SerializeField]
	private InputField slingShotLaunchForceInputField;
	[SerializeField]
	private InputField slingShotLaunchAngleInputField;
	[SerializeField]
	private InputField slingShotAngleCounterWeightInputField;
	[SerializeField]
	private InputField slingShotOriginXInputField;
	[SerializeField]
	private InputField slingShotOriginYInputField;
	[SerializeField]
	private InputField slingShotOriginZInputField;
	[SerializeField]
	private InputField trajectoryInterval;
	#endregion

	#region Functionality
	public void ChangeBulletMass()
	{
		StaticReferences.bulletMass = UtilityLibrary.GetFloatValueFromInputField(bulletMassInputField);
	}

	public void ChangeSlingShotLaunchForce()
	{
		StaticReferences.slingShotMaximumLaunchForce = UtilityLibrary.GetFloatValueFromInputField(slingShotLaunchForceInputField);
	}

	public void ChangeSlingShotAngleCounterWeight()
	{
		StaticReferences.slingShotAngleCounterWeight = UtilityLibrary.GetFloatValueFromInputField(slingShotAngleCounterWeightInputField);
	}

	public void ChangeSlingShotAngle()
	{
		StaticReferences.slingShotLaunchAngle = -Mathf.Abs(UtilityLibrary.GetFloatValueFromInputField(slingShotLaunchAngleInputField));
	}

	public void ChangeSlingShotOriginXValue()
	{
		StaticReferences.slingShotOriginPoint.x = UtilityLibrary.GetFloatValueFromInputField(slingShotOriginXInputField);
		UpdateslingShotOriginPoint();
	}

	public void ChangeSlingShotOriginYValue()
	{
		StaticReferences.slingShotOriginPoint.y = UtilityLibrary.GetFloatValueFromInputField(slingShotOriginYInputField);
		UpdateslingShotOriginPoint();
	}

	public void ChangeSlingShotOriginZValue()
	{
		StaticReferences.slingShotOriginPoint.z  = UtilityLibrary.GetFloatValueFromInputField(slingShotOriginZInputField);
		UpdateslingShotOriginPoint();
	}

	public void ChangeTrajectoryIntervals()
	{
		StaticReferences.predictionIntervals = UtilityLibrary.GetFloatValueFromInputField(trajectoryInterval);
	}

	private void UpdateslingShotOriginPoint()
	{
		slingShotOriginPoint.transform.localPosition = StaticReferences.slingShotOriginPoint;
	}
	#endregion
}