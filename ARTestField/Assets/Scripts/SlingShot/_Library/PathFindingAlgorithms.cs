﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AStarSearchAlgorithm : IPathFinder
{
	//Do this in coroutine to calculate over the frames
	public List<Vector3> CalculatePath(List<PathNode> nodeMap, PathNode startNode, PathNode goal)
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
					adjacentNode.EndGoal = goal;
					
					adjacentNode.PathLength = currentPathNode.PathLength + adjacentNode.DistanceToPreviousNode;
				}			
			}
			priorityQueue = priorityQueue.OrderBy(node => node.PathWeight).ToList();
			PathNode closestNode = priorityQueue.First();
	
			priorityQueue.Remove(closestNode);
			travelledNodes.Add(currentPathNode);
			closestNode.PreviousPathNodes = currentPathNode.PreviousPathNodes;
			closestNode.PreviousPathNodes.Add(closestNode);

			currentPathNode = closestNode;
		}
	
		path = currentPathNode.PreviousPathNodes.Select(node => node.NodePosition).ToList();

		return path;
	}
}

