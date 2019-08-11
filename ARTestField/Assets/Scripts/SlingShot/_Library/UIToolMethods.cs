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
		StartCoroutine(StaticRefrences.CoroutineToolMethods.AsyncLoadScene(sceneName));
	}

	public void ExitGame()
	{
		StaticRefrences.UIToolMethods.ExitGame();
	}
	#endregion
}

