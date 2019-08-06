using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

[System.Serializable]
public struct PathingInformation
{
	public PathingAlgorithm pathingAlgorithm;
	public string nodeSectionName;
	public float speedMultiplier;
}

public enum PathingAlgorithm
{
	AStar = 0
}

public class PathfindingCalculationParameters
{
	public PathNode StartNode;
	public PathNode EndNode;
}

public class AStarPathNode
{
	#region Variables	
	public PathNode pathNode;
	public AStarPathNode previousNode;
	public PathNode endGoal;
	public float pathLength;
	public float DistanceToPreviousNode
	{
		get
		{
			float distance = previousNode.pathNode == null ? Mathf.Infinity : Vector3.Distance(pathNode.NodePosition, previousNode.pathNode.NodePosition);
			return distance;
		}
	}
	public float DistanceToGoal { get { return Vector3.Distance(pathNode.NodePosition, endGoal.NodePosition); } }
	public float PathWeight { get { return pathLength + DistanceToGoal; } }
	#endregion
}

public static class PathFindingAlgorithms
{
	//Coroutine calcualtion
	public async static Task<List<Vector3>> CalculateAStarPath(PathfindingCalculationParameters pathfindingCalculationParameters)
	{
		PathNode startNode = pathfindingCalculationParameters.StartNode;
		PathNode endNode = pathfindingCalculationParameters.EndNode;

		List<Vector3> path = new List<Vector3>();
		List<AStarPathNode> priorityQueue = new List<AStarPathNode>();
		List<AStarPathNode> travelledNodes = new List<AStarPathNode>();

		priorityQueue.Add(new AStarPathNode { pathNode = startNode, endGoal = endNode });

		//If the endnode is still not reached 
		while(priorityQueue.Count != 0 && !travelledNodes.Select(node => node.pathNode).Contains(endNode))
		{
			AStarPathNode currentPathNode = priorityQueue[0];
			priorityQueue.Remove(currentPathNode);
			travelledNodes.Add(currentPathNode);
			foreach(PathNode adjacentNode in currentPathNode.pathNode.connectedNodes)
			{
				AStarPathNode adjacentAstarNode = new AStarPathNode
				{
					pathNode = adjacentNode,
					endGoal = endNode,
					previousNode = currentPathNode,
				};
				adjacentAstarNode.pathLength = currentPathNode.pathLength + adjacentAstarNode.DistanceToPreviousNode;
				if(!travelledNodes.Select(node => node.pathNode).ToList().Contains(adjacentAstarNode.pathNode) && !priorityQueue.Select(node => node.pathNode).ToList().Contains(adjacentAstarNode.pathNode))
				{
					priorityQueue.Add(adjacentAstarNode);
				}
			}

			//Return failed path if the end node could not be reached with the nodes given.
			if(priorityQueue.Count == 0 && !travelledNodes.Select(node => node.pathNode).Contains(endNode))
			{
				return path;
			}

			priorityQueue = priorityQueue.OrderBy(node => node.PathWeight).ToList();
			await Task.Delay(StaticRefrences.FixedTimeInMiliseconds);
		}
	
		//Keep adding the pathnode to the path untill we hit the start pathnode
		AStarPathNode pathNodeToAdd = travelledNodes.Last();
		while(pathNodeToAdd.pathNode != startNode)
		{
			path.Add(pathNodeToAdd.pathNode.NodePosition);
			pathNodeToAdd = pathNodeToAdd.previousNode;
			await Task.Delay(StaticRefrences.FixedTimeInMiliseconds);
		}
		path.Reverse();
		return path;
	}
}