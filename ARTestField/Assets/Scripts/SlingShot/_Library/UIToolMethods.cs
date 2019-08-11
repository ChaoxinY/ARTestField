using UnityEngine;

public class UIToolMethods : MonoBehaviour
{
	#region Variables
	#endregion

	#region Initialization
	#endregion

	#region Functionality
	public void LoadScene(string sceneName)
	{
		StartCoroutine(StaticReferences.CoroutineToolMethods.AsyncLoadScene(sceneName));
	}

	public void ExitGame()
	{
		StaticReferences.UIToolMethods.ExitGame();
	}
	#endregion
}

