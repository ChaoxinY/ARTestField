using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugWidow : MonoBehaviour
{
	#region Variables
	[SerializeField]
	private readonly GameObject slingShotOriginPoint;
	[SerializeField]
	private readonly InputField bulletMassInputField;
	[SerializeField]
	private readonly InputField slingShotLaunchForceInputField;
	[SerializeField]
	private readonly InputField slingShotLaunchAngleInputField;
	[SerializeField]
	private readonly InputField slingShotAngleCounterWeightInputField;
	[SerializeField]
	private readonly InputField slingShotOriginXInputField;
	[SerializeField]
	private readonly InputField slingShotOriginYInputField;
	[SerializeField]
	private readonly InputField slingShotOriginZInputField;
	[SerializeField]
	private readonly InputField trajectoryInterval;
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