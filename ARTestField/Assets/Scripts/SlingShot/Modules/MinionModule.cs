 using System;
using UnityEngine;
using UnityAD;

public class MinionModule : ICollideAble, IEventPublisher
{
	#region Variables
	public Action GotHit { get; set; }
	public int minionValue;
	public long vibrateLength;
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
	public void ReactToCollision(GameObject gameObject, Collision collision)
	{
		if(collision.gameObject.tag == "Bullet")
		{
			lastCollision = collision;
			GameObject.Destroy(collision.gameObject);
			MinionHit(this, new MinionOnHitEventArgs(minionValue));
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

