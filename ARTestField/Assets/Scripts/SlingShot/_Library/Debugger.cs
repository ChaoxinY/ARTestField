using UnityEngine;

public static class Debugger
{
	public static void DebugObject(object caller, string debugContent)
	{
		Debug.Log($"Caller: {caller} Debug Content: {debugContent}");
	}
}


