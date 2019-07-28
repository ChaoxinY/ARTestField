using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityAD;

public class MinionModule : ICollideAble, IEventPublisher, IUpdater
{
	#region Variables
	public Action GotHit { get; set; }
	public int minionValue;
	public long vibrateLength;
	public GameObject onHitParticleEffect;
	public GameObject minion;
	public event EventHandler<MinionOnHitEventArgs> MinionHit;
	public bool moves;
	public IPathFinder pathFinder;
	public float movementSpeedMultiplier;
	public Path path;
	private Collision lastCollision;
	private Queue<Vector3> currentPath = new Queue<Vector3>();
	private Vector3 nextPosition;
	private Transform minionTransform;
	#endregion

	#region Initialization
	public MinionModule()
	{
		StaticRefrences.EventSubject.Subscribe(this);
		minionTransform = minion.transform;
	}
	#endregion

	#region Functionality
	public void ReactToCollision(GameObject gameObject, Collision collision)
	{
		if (collision.gameObject.tag == "Bullet")
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

	public void UpdateComponent()
	{

	}

	public void FixedUpdateComponent()
	{
		if (moves)
		{
			Move();
		}
	}

	private void Move()
	{
		if (Vector3.Distance(minion.transform.position, nextPosition) > 0.001f && nextPosition != null)
		{
			minionTransform.position = Vector3.MoveTowards(minionTransform.position, nextPosition, Time.deltaTime*movementSpeedMultiplier);
		}
		else if (currentPath.Count != 0)
		{
			nextPosition = currentPath.Dequeue();
		}
		if (currentPath.Count == 0)
		{
			CalculatePath();
		}
	}

	private void CalculatePath()
	{
		PathNode startNode = nextPosition == null ? path.pathNodes[0] : path.pathNodes.Where(node => node.NodePosition == nextPosition).First();
		PathNode endNode = nextPosition == null ? path.pathNodes.Last() :
			path.pathNodes.Where(node => node.NodePosition != nextPosition).ToList()[GenerateRandomIEnumerablePosition(path.pathNodes)];
		List<Vector3> calculatedPath = pathFinder.CalculatePath(path.pathNodes, startNode, endNode);
		foreach (Vector3 node in calculatedPath)
		{
			currentPath.Enqueue(node);
		}
		nextPosition = currentPath.Dequeue();
	}

	private int GenerateRandomIEnumerablePosition<T>(IEnumerable<T> IEnumerable)
	{
		int value = UnityEngine.Random.Range(0, IEnumerable.Count());
		return value;
	}
	#endregion
}