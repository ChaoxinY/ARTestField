using System;
using UnityEngine;
using UnityAD;

public static class MinionFactory
{
	public static MinionModule CreateMinionModule(GameObject gameObject, MinionPreset minionPreset)
	{
		MinionModule minionModule = new MinionModule();
		minionModule.minion = gameObject;
		gameObject.GetComponent<MeshRenderer>().material = Resources.Load($"Prefabs/SlingShot/Minion/MinionMaterial_{minionPreset.rank.ToString()}") as Material;
		minionModule.minionValue = (int)minionPreset.rank;

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
		return minionModule;
	}
}

public class MinionOnHitEventArgs : EventArgs
{
	public int MinionValue;
	public MinionModule MinionModule;
	public MinionOnHitEventArgs(int minionValue, MinionModule minionModule)
	{
		MinionValue = minionValue;
		MinionModule = minionModule;
	}
}
/// <summary>
/// Functionalities: Calls on hit feedback when collided with a bullet. Also sends an event when this happens. 
/// </summary>
public class MinionModule : ICollideAble, IEventPublisher
{ 
	#region Variables
	public Action GotHit { get; set; }
	public int minionValue;
	public long vibrateLength;
	public GameObject onHitParticleEffect;
	public GameObject minion;
	public event EventHandler<MinionOnHitEventArgs> MinionHit;
	private Collision lastCollision;
	#endregion

	#region Initialization
	public MinionModule()
	{
		//Debug.Log(StaticRefrences.EventSubject.PublisherSubscribed);	
		StaticRefrences.EventSubject.Subscribe(this);
	}
	#endregion

	#region Functionality
	public void ReactToCollision(GameObject gameObject, Collision collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			lastCollision = collision;
			MinionHit?.Invoke(this, new MinionOnHitEventArgs(minionValue,this));
			GotHit();
			UnSubscribeFromSubject();
			GameObject.Destroy(gameObject);
		}
	}

	public void VibrateDevice()
	{
		FeedBackToolMethods.VibrateAndroidDevice(vibrateLength);
	}

	public void PlayOnHitParticlEffect()
	{
		FeedBackToolMethods.SpawnOnHitEffect(onHitParticleEffect, lastCollision);
	}

	public void UnSubscribeFromSubject()
	{
		StaticRefrences.EventSubject.UnSubscribe(this);
	}
	#endregion
}