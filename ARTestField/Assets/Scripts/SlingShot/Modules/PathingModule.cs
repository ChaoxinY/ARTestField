using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityAD;
using System.Threading.Tasks;

/// <summary>
/// Functionalities:
/// </summary>
public static class PathingModuleFactory
{
	public static PathingModule CreatePathingModule(GameObject gameObject, PathingInformation pathingInformation)
	{
		PathingModule pathingModule = new PathingModule();
		pathingModule.objectTransform = gameObject.transform;

		switch(pathingInformation.pathingAlgorithm)
		{
			case PathingAlgorithm.AStar:
				pathingModule.CalculatePath += PathFindingAlgorithms.CalculateAStarPath;
				break;
		}

		try
		{
			pathingModule.nodeSection = StaticRefrences.currentPathMap.INodeSections.Where(nodeSection => nodeSection.SectionName == pathingInformation.nodeSectionName).First();
		}
		catch(NullReferenceException e)
		{
			Debug.Log("No pathing node section found");
		}
		pathingModule.movementSpeedMultiplier = pathingInformation.speedMultiplier;
		return pathingModule;
	}
}

public class PathingModule : IFixedUpdater
{
	#region Variabless
	public Func<PathfindingCalculationParameters, Task<List<Vector3>>> CalculatePath;
	public INodeSection nodeSection;
	public float movementSpeedMultiplier;
	public Transform objectTransform;
	private Queue<Vector3> currentPath = new Queue<Vector3>();
	private Vector3 nextPosition = Vector3.zero;
	private List<Vector3> debugPath = new List<Vector3>();
	private bool activated, calculatingPath;
	#endregion

	#region Initialization
	#endregion

	#region Functionality

	public void FixedUpdateComponent()
	{
		if(nodeSection.GenerationFinished)
		{
			DebugCurrentPath();
			if(currentPath.Count != 0) { Move(); }
			else if(currentPath.Count == 0 && !calculatingPath) { AddPath(); }
		}
	}

	private void Move()
	{
		nextPosition = currentPath.Peek();
		if(Vector3.Distance(objectTransform.position, nextPosition) > 0.001f && nextPosition != Vector3.zero)
		{
			//Move behaviour 
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
		if(nextPosition == Vector3.zero)
		{
			nextPosition = nodeSection.PathNodes[0].NodePosition;
		}		
		calculatingPath = true;
		currentPath.Clear();
		PathNode startNode = nodeSection.PathNodes.Where(node => node.NodePosition == nextPosition).ToList().First();
		List<PathNode> uniqueNodeList = nodeSection.PathNodes.Where(node => node.NodePosition != nextPosition).ToList();
		int randomListValue = StaticRefrences.SystemToolMethods.GenerateRandomIEnumerablePosition(uniqueNodeList);
		PathNode endNode = uniqueNodeList[randomListValue];
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
		currentPath = new Queue<Vector3>(calculatedPath);
	}

	#endregion
}

