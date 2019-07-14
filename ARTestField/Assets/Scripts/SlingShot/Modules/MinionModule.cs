using System;
using UnityEngine;
using UnityAD;

public class MinionModule : ICollideAble, IEventPublisher
{
	#region Variables
	public Action GotHit { get; set; }
	public int minionValue, vibrateLength;
	public GameObject onHitParticleEffect;
	public event EventHandler<MinionOnHitEventArgs> MinionHit;
	private Collision lastCollision;
	#endregion

	#region Initialization
	public MinionModule()
	{
		StaticRefrences.EventSubject.Subscribe(this);
	}
	#endregion

	#region Functionality
	public void ReactToCollision(Collision collision)
	{
		if(collision.gameObject.tag == "Bullet")
		{
			lastCollision = collision;
			MinionHit(this, new MinionOnHitEventArgs(minionValue));
			GotHit();
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

