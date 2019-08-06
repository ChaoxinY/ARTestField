using System;
using UnityEngine;
using UnityAD;

/// <summary>
/// Functionalities:
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