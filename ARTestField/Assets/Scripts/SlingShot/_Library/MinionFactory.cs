using UnityEngine;
using System.Linq;

public static class MinionFactory
{
	public static MinionModule CreateMinionModule(MinionPreset minionPreset, GameObject gameObject)
	{
		MinionModule minionModule = new MinionModule();
		//Load default minion prefab Assets/Prefabs/SlingShot/Mininon/MinionMaterial_Normal.mat
		Debug.Log($"Prefabs/SlingShot/Minion/MinionMaterial_{minionPreset.Rank.ToString()}");
		minionModule.minion = gameObject;
		gameObject.GetComponent<MeshRenderer>().material = Resources.Load($"Prefabs/SlingShot/Minion/MinionMaterial_{minionPreset.Rank.ToString()}") as Material;
		minionModule.minionValue = (int)minionPreset.Rank;

		if(minionPreset.onHitVibrationLength > 0)
		{
			minionModule.vibrateLength = minionPreset.onHitVibrationLength;
			minionModule.GotHit += minionModule.VibrateDevice;
		}

		if(minionPreset.onHitParticleFeedback != OnHitParticleFeedback.None)
		{
			minionModule.onHitParticleEffect = Resources.Load($"Prefabs/SlingShot/Minion/MinionParticleEffect_{minionPreset.onHitParticleFeedback.ToString()}") as GameObject;
			minionModule.GotHit += minionModule.PlayOnHitParticlEffect;
		}
		if (minionPreset.pathingInformation != null)
		{
			PathingInformation pathingInformation = minionPreset.pathingInformation.Value;
			IPathFinder pathFinderToUse = null;
			switch (pathingInformation.pathingAlgorithm)
			{
				case PathingAlgorithm.AStar:
					pathFinderToUse = new AStarSearchAlgorithm();
					break;
			}
			minionModule.pathFinder = pathFinderToUse;
			minionModule.path = StaticRefrences.currentPathMap.paths.Where(path => path.pathName == pathingInformation.pathName).First();
			minionModule.movementSpeedMultiplier = pathingInformation.speedMultiplier;
		}
		return minionModule;
	}

}


