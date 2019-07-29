using UnityEngine;

public class Mininon : MonoBehaviour
{
	#region Variables
	public MinionPreset minionPreset;
	public PathingInformation pathingInformation;
	private MinionModule minionModule;
	private PathingModule pathingModule;
	#endregion

	#region Initialization
	private void Awake()
	{
		minionModule = MinionFactory.CreateMinionModule(gameObject, minionPreset);
		pathingModule = PathingModuleFactory.CreatePathingModule(gameObject, pathingInformation);
	}
	#endregion

	#region Functionality
	private void OnCollisionEnter(Collision collision)
	{
		minionModule.ReactToCollision(gameObject, collision);
	}

	private void FixedUpdate()
	{
		pathingModule.FixedUpdateComponent();
	}
	#endregion
}

