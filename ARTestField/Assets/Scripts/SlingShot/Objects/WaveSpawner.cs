using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAD;

[Serializable]
public struct Wave
{
	public int startingMinions;
	public int waveValue;
	[SerializeField]
	private float minMovementSpeed, maxMovementSpeed, minSpawnInterval, maxSpawnInterval;
	public MinMaxValue<float> MinionMovementSpeed { get { return new MinMaxValue<float> { minValue = minMovementSpeed, maxValue = maxMovementSpeed }; } }
	public MinMaxValue<float> SpawnInterval { get { return new MinMaxValue<float> { minValue = minSpawnInterval, maxValue = maxSpawnInterval }; } }
}

public class WaveSpawner : MonoBehaviour, IEventHandler
{
	#region Variables
	[SerializeField]
	private List<Wave> waves;
	[SerializeField]
	private List<GameObject> availableMinionTypes;
	[SerializeField]
	private Transform levelParent;
	private PathMap pathMap;
	private Queue<Wave> waveQueue = new Queue<Wave>();
	private List<GameObject> spawnedMinions = new List<GameObject>();
	private int currentWaveValue;
	private Wave currentWave;
	private IEnumerator currentSpawnLoop;
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticReferences.EventSubject.PublisherSubscribed += SubscribeEvent;
		waveQueue = new Queue<Wave>(waves);
	}

	private void Start()
	{
		pathMap = StaticReferences.currentPathMap;
	}
	#endregion

	#region Functionality
	public void SubscribeEvent(object eventPublisher, PublisherSubscribedEventArgs publisherSubscribedEventArgs)
	{
		if(publisherSubscribedEventArgs.Publisher.GetType()== typeof(MinionModule))
		{
			MinionModule minionModule = (MinionModule)publisherSubscribedEventArgs.Publisher;
			minionModule.MinionHit += SpawnMinion;
		}
		else if(publisherSubscribedEventArgs.Publisher.GetType()== typeof(LevelInitializer))
		{
			LevelInitializer levelInitializer = (LevelInitializer)publisherSubscribedEventArgs.Publisher;
			levelInitializer.LevelStarted += StartSpawning;
			levelInitializer.LevelEnded += StopSpawning;
		}
	}

	public void UnSubScribeEvent()
	{
		StaticReferences.EventSubject.PublisherSubscribed -= SubscribeEvent;
		foreach(IEventPublisher eventPublisher in StaticReferences.EventSubject.EventPublishers)
		{
			if(eventPublisher.GetType()== typeof(LevelInitializer))
			{
				LevelInitializer levelInitializer = (LevelInitializer)eventPublisher;
				levelInitializer.LevelStarted -= StartSpawning;
				levelInitializer.LevelEnded -= StopSpawning;
			}

			else if(eventPublisher.GetType()== typeof(MinionModule))
			{
				MinionModule minionModule = (MinionModule)eventPublisher;
				minionModule.MinionHit -= SpawnMinion;
			}
		}
	}
	private void OnDestroy()
	{
		UnSubScribeEvent();
	}

	private void SpawnMinion(object eventPublisher, MinionOnHitEventArgs minionOnHitEventArgs)
	{
		if(spawnedMinions.Any(minion => GameObject.ReferenceEquals(minion, minionOnHitEventArgs.MinionModule.minion)))
		{
			spawnedMinions.Remove(minionOnHitEventArgs.MinionModule.minion);
			if(currentWaveValue >0)
			{
				InitializeMinion();
			}
		}
	}

	private void StopSpawning(object sender, EventArgs e)
	{
		StopCoroutine(currentSpawnLoop);
	}

	private void StartSpawning(object objects, EventArgs e)
	{
		currentSpawnLoop = SpawnLoop();
		StartCoroutine(currentSpawnLoop);
	}

	private IEnumerator SpawnLoop()
	{
		while(waveQueue.Count > 0)
		{
			yield return StartCoroutine(SpawnWave());
		}
	}

	private IEnumerator SpawnWave()
	{
		currentWave = waveQueue.Peek();
		currentWaveValue = currentWave.waveValue;
		for(int i = 0; i < currentWave.startingMinions; i++)
		{
			InitializeMinion();
		}

		float spawnTimer = UnityEngine.Random.Range(currentWave.SpawnInterval.minValue, currentWave.SpawnInterval.maxValue);
		while(currentWaveValue > 0)
		{
			spawnTimer -= Time.fixedDeltaTime;
			yield return new WaitForSeconds(Time.fixedDeltaTime);
			if(spawnTimer <=0)
			{
				InitializeMinion();
				spawnTimer = UnityEngine.Random.Range(currentWave.SpawnInterval.minValue, currentWave.SpawnInterval.maxValue);
			}
		}
		waveQueue.Dequeue();
	}

	private void InitializeMinion()
	{
		int randomMinionTypeValue = StaticReferences.SystemToolMethods.GenerateRandomIEnumerablePosition(availableMinionTypes);
		GameObject minion = availableMinionTypes[randomMinionTypeValue];
		MinionPreset minionPreset = minion.GetComponent<Minion>().minionPreset;

		//If the minion value exceeds the wavevalue left. Force select a minion with the same value as the current wavevalue
		int predictionValue = currentWaveValue;
		if((predictionValue -= (int)minionPreset.rank) < 0)
		{
			minion = availableMinionTypes.Where(minionType => (int)minionType.GetComponent<Minion>().minionPreset.rank == currentWaveValue).ToList()[0];
			minionPreset = minion.GetComponent<Minion>().minionPreset;
		}
		//Debug.Log($"CurrentWave {currentWaveValue} MinionValue {(int)minionPreset.rank}");
		currentWaveValue -= (int)minionPreset.rank;
		int randomNodeSectionValue = StaticReferences.SystemToolMethods.GenerateRandomIEnumerablePosition(pathMap.INodeSections);
		int randomPathNodeValue = StaticReferences.SystemToolMethods.GenerateRandomIEnumerablePosition(pathMap.INodeSections[randomNodeSectionValue].PathNodes);
		Vector3 randomNodePosition = pathMap.INodeSections[randomNodeSectionValue].PathNodes[randomPathNodeValue].NodePosition;

		if(minion.GetComponent<Mover>())
		{
			Mover mover = minion.GetComponent<Mover>();
			mover.pathingInformation = new PathingInformation
			{
				nodeSectionName = pathMap.INodeSections[randomNodeSectionValue].SectionName,
				speedMultiplier = UnityEngine.Random.Range(currentWave.MinionMovementSpeed.minValue, currentWave.MinionMovementSpeed.maxValue)
			};
		}
		GameObject spawnedMinion = Instantiate(minion, randomNodePosition, Quaternion.identity, levelParent);
		spawnedMinions.Add(spawnedMinion);
	}
	#endregion
}

