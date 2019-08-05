using UnityEngine.UI;
using System;

[Serializable]
public struct MinMaxValue<T>
{
	public T minValue;
	public T maxValue;
}

public static class UtilityLibrary
{
	public static float GetFloatValueFromInputField(InputField inputField)
	{
		if(float.TryParse(inputField.text, out float value))
		{
			value = float.Parse(inputField.text);
		}
		return value;
	}

	public static int GetIntValueFromInputField(InputField inputField)
	{
		if(int.TryParse(inputField.text, out int value))
		{
			value = int.Parse(inputField.text);
		}
		return value;
	}
}


