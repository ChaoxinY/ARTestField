using UnityEngine;

public class Minion : MonoBehaviour
{
	#region Variables
	public MinionPreset minionPreset;
	private MinionModule minionModule;
	#endregion

	#region Initialization
	private void Start()
	{
		minionModule = MinionFactory.CreateMinionModule(gameObject, minionPreset);
	}
	#endregion

	#region Functionality
	private void OnCollisionEnter(Collision collision)
	{
		minionModule.ReactToCollision(gameObject, collision);
	}
	#endregion
}

