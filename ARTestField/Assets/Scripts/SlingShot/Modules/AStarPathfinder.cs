using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class AStarPathfinder
{
	//Do this in coroutine to calculate over the frames
	public static List<Vector3> CalculatePath(List<PathNode> nodeMap, PathNode startNode, PathNode goal)
	{
		List<Vector3> path = new List<Vector3>();

		List<PathNode> priorityQueue = new List<PathNode>();
		List<PathNode> travelledNodes = new List<PathNode>();
		PathNode currentPathNode = startNode;

		while (currentPathNode != goal)
		{		
			foreach (PathNode adjacentNode in currentPathNode.connectedNodes)
			{
				if (!travelledNodes.Contains(adjacentNode) && !priorityQueue.Contains(adjacentNode))
				{
					priorityQueue.Add(adjacentNode);
					adjacentNode.endGoal = goal;
					//Sort by current path length traveled and the distance to goal 
					//current path length traveled =  current selected node length traveled + length distance to current path node
					//+ length distance from the current selected node to this node 
					adjacentNode.pathLength = currentPathNode.pathLength + adjacentNode.DistanceToPreviousNode;
				}			
			}
	

			priorityQueue = priorityQueue.OrderBy(node => node.PathWeight).ToList();
			foreach (PathNode node in priorityQueue)
			{
				Debug.Log($"PriorityQueue Node: {node.gameObject.name} {node.PathWeight}");
			}
			PathNode closestNode = priorityQueue.First();
			Debug.Log($"Current node {currentPathNode.gameObject.name} Closest node: {closestNode} node weight{closestNode.PathWeight}");

			priorityQueue.Remove(closestNode);
			travelledNodes.Add(currentPathNode);
			closestNode.previousPathNodes = currentPathNode.previousPathNodes;
			closestNode.previousPathNodes.Add(closestNode);

			// Set the current path node to the node in priorty queue with the loweset weight.
			currentPathNode = closestNode;
		}
	
		path = currentPathNode.previousPathNodes.Select(node => node.nodePosition).ToList();

		return path;
	}
}

