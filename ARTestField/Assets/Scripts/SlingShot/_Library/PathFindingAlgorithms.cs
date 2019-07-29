using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public struct PathingInformation
{
	public PathingAlgorithm pathingAlgorithm;
	public string nodeSectionName;
	public float speedMultiplier;
}

public struct AStarPathNode
{
	#region Variables	
	public PathNode pathNode;
	public List<PathNode> path;
	public PathNode previousNode;
	public PathNode endGoal;
	public float pathLength;
	public float DistanceToPreviousNode
	{
		get
		{
			float distance = previousNode == null ? Mathf.Infinity : Vector3.Distance(pathNode.NodePosition, previousNode.NodePosition);
			return distance;
		}
	}
	public float DistanceToGoal { get { return Vector3.Distance(pathNode.NodePosition, endGoal.NodePosition); } }
	public float PathWeight { get { return pathLength + DistanceToGoal; } }
	#endregion
}

public static class PathFindingAlgorithms
{
	public static List<Vector3> CalculateAStarPath(PathNode startNode, PathNode goal)
	{
		List<Vector3> path = new List<Vector3>();

		List<AStarPathNode> priorityQueue = new List<AStarPathNode>();
		List<AStarPathNode> travelledNodes = new List<AStarPathNode>();
		List<AStarPathNode> nodeModified = new List<AStarPathNode>();
		AStarPathNode currentPathNode = new AStarPathNode
		{
			pathNode = startNode,
			path = new List<PathNode>()
		};

		while(currentPathNode.pathNode != goal)
		{
			foreach(PathNode adjacentNode in currentPathNode.pathNode.connectedNodes)
			{
				AStarPathNode adjacentAstarNode =  new AStarPathNode
				{
					pathNode = adjacentNode,
					path = new List<PathNode>(),
					endGoal = goal,
					previousNode = currentPathNode.pathNode,					
				};
				adjacentAstarNode.pathLength = currentPathNode.pathLength + adjacentAstarNode.DistanceToPreviousNode;
				if(!travelledNodes.Contains(adjacentAstarNode) && !priorityQueue.Contains(adjacentAstarNode))
				{
					priorityQueue.Add(adjacentAstarNode);				
				}			
			}
			Debug.Log(priorityQueue[0].endGoal);
			priorityQueue = priorityQueue.OrderBy(node => node.PathWeight).ToList();

			AStarPathNode closestNode = priorityQueue.First();

			priorityQueue.Remove(closestNode);
			travelledNodes.Add(currentPathNode);
			closestNode.path = currentPathNode.path;
			closestNode.path.Add(closestNode.pathNode);

			currentPathNode = closestNode;
		}
		path = currentPathNode.path.Select(node => node.NodePosition).ToList();

		return path;

	}
}