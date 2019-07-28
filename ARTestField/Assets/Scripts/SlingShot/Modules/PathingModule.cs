using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityAD;

/// <summary>
/// Functionalities:
/// Required modules:
/// Usage Required modules:
/// </summary>

public class PathingModule : IFixedUpdater
{
	#region Variables
	public Func<List<PathNode>, PathNode, PathNode, List<Vector3>> CalculatePath;
	public Path path;
	public float movementSpeedMultiplier;
	private Queue<Vector3> currentPath = new Queue<Vector3>();
	private Vector3 nextPosition;
	private Transform moduleTransform;
	#endregion

	#region Initialization
	#endregion

	#region Functionality
	public void FixedUpdateComponent()
	{
		Move();
	}

	private void Move()
	{
		if(Vector3.Distance(moduleTransform.position, nextPosition) > 0.001f && nextPosition != null)
		{
			moduleTransform.position = Vector3.MoveTowards(moduleTransform.position, nextPosition, Time.deltaTime*movementSpeedMultiplier);
		}
		else if(currentPath.Count != 0)
		{
			nextPosition = currentPath.Dequeue();
		}
		if(currentPath.Count == 0)
		{
			AddPath();
		}
	}

	private void AddPath()
	{
		PathNode startNode = nextPosition == null ? path.pathNodes[0] : path.pathNodes.Where(node => node.NodePosition == nextPosition).First();
		PathNode endNode = nextPosition == null ? path.pathNodes.Last() :
			path.pathNodes.Where(node => node.NodePosition != nextPosition).ToList()[StaticRefrences.SystemToolMethods.GenerateRandomIEnumerablePosition(path.pathNodes)];
		List<Vector3> calculatedPath = CalculatePath(path.pathNodes, startNode, endNode);
		foreach(Vector3 node in calculatedPath)
		{
			currentPath.Enqueue(node);
		}
		nextPosition = currentPath.Dequeue();
	}
	#endregion
}

