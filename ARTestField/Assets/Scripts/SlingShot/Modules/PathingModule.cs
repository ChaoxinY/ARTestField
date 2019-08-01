using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityAD;
using System.Threading.Tasks;

/// <summary>
/// Functionalities:
/// </summary>

public class PathingModule : IFixedUpdater, IEventHandler
{
	#region Variabless
	public Func<PathfindingCalculationParameters, Task<List<Vector3>>> CalculatePath;
	public NodeSection nodeSection;
	public float movementSpeedMultiplier;
	public Transform objectTransform;
	private Queue<Vector3> currentPath = new Queue<Vector3>();
	private Vector3 nextPosition = Vector3.zero;
	private List<Vector3> debugPath = new List<Vector3>();
	private bool activated, calculatingPath;
	#endregion

	#region Initialization
	public PathingModule()
	{
		StaticRefrences.EventSubject.PublisherSubscribed += SubscribeEvent;
	}
	#endregion

	#region Functionality
	public void SubscribeEvent(object eventPublisher, PublisherSubscribedEventArgs publisherSubscribedEventArgs)
	{
		if(publisherSubscribedEventArgs.Publisher.GetType()== typeof(PathMap))
		{
			PathMap pathMap = (PathMap)publisherSubscribedEventArgs.Publisher;
			pathMap.ConnectionsGenerated += ActivateModule;
		}
	}

	public void FixedUpdateComponent()
	{
		if(activated)
		{
			DebugCurrentPath();
			if(currentPath.Count != 0) { Move(); }
			else if(currentPath.Count == 0 && !calculatingPath) { AddPath(); }
		}
	}

	private void ActivateModule(object sender, NodeConnectionsGeneratedEventArgs nodeConnectionsGenerateEventArgs)
	{
		activated = nodeConnectionsGenerateEventArgs.Finished;
	}

	private void Move()
	{
		nextPosition = currentPath.Peek();
		if(Vector3.Distance(objectTransform.position, nextPosition) > 0.001f && nextPosition != Vector3.zero)
		{
			objectTransform.position = Vector3.MoveTowards(objectTransform.position, nextPosition, Time.deltaTime*movementSpeedMultiplier);
		}
		else 
		{
			nextPosition = currentPath.Dequeue();
		}
	}

	private void DebugCurrentPath()
	{
		for(int i = 0; i < debugPath.Count; i++)
		{
			int j = i == debugPath.Count-1 ? i-1 : i+1;
			if(i == 0)
			{
				j = 0;
			}
			Debug.DrawLine(debugPath[i], debugPath[j], Color.green);
		}
	}

	private async void AddPath()
	{
		calculatingPath = true;
		currentPath.Clear();
		PathNode startNode = nextPosition == Vector3.zero ? nodeSection.pathNodes[0] : nodeSection.pathNodes.Where(node => node.NodePosition == nextPosition).ToList().First();
		List<PathNode> uniqueNodeList = nodeSection.pathNodes.Where(node => node.NodePosition != nextPosition).ToList();
		int randomListValue = StaticRefrences.SystemToolMethods.GenerateRandomIEnumerablePosition(uniqueNodeList);
		PathNode endNode = nextPosition == Vector3.zero ? nodeSection.pathNodes.Last() : uniqueNodeList[randomListValue];

		PathfindingCalculationParameters pathfindingCalculationParameters = new PathfindingCalculationParameters
		{
			StartNode = startNode,
			EndNode = endNode
		};
		List<Vector3> calculatedPath = await CalculatePath(pathfindingCalculationParameters);
		calculatingPath = false;
		if(calculatedPath.Count == 0)
		{			
			return;
		}
		debugPath = calculatedPath;
		foreach(Vector3 node in calculatedPath)
		{
			currentPath.Enqueue(node);
		}
	}

	#endregion
}

