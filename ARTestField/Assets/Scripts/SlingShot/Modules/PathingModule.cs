using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityAD;

/// <summary>
/// Functionalities:
/// </summary>

public class PathingModule : IFixedUpdater
{
	#region Variables
	public Func<PathNode, PathNode, List<Vector3>> CalculatePath;
	public NodeSection nodeSection;
	public float movementSpeedMultiplier;
	public Transform moduleTransform;
	private Queue<Vector3> currentPath = new Queue<Vector3>();
	private Vector3 nextPosition = Vector3.zero;
	#endregion

	#region Initialization
	#endregion

	#region Functionality
	public void FixedUpdateComponent()
	{	
		Move();
		DebugCurrentPath();
	}

	private void Move()
	{
		if(Vector3.Distance(moduleTransform.position, nextPosition) > 0.001f && nextPosition != Vector3.zero)
		{
			moduleTransform.position = Vector3.MoveTowards(moduleTransform.position, nextPosition, Time.deltaTime*movementSpeedMultiplier);
		}
		else
		{
			if(currentPath.Count != 0)
			{
				nextPosition = currentPath.Dequeue();
			}
			else
			{
				AddPath();
			}
		}
	}

	private void DebugCurrentPath()
	{
		List<Vector3> positions = currentPath.ToList();
		for(int i = 0; i < positions.Count; i++)
		{
			int j = i == positions.Count-1f ? 0 : 1;
			Debug.DrawLine(positions[i], positions[i+j], Color.green);
		}	
	}

	private void AddPath()
	{
		currentPath.Clear();
		PathNode startNode = nextPosition == Vector3.zero ? nodeSection.pathNodes[0] : nodeSection.pathNodes.Where(node => node.NodePosition == nextPosition).ToList().First();
		List<PathNode> uniqueNodeList = nodeSection.pathNodes.Where(node => node.NodePosition != nextPosition).ToList();
		int randomListValue = StaticRefrences.SystemToolMethods.GenerateRandomIEnumerablePosition(uniqueNodeList);
		PathNode endNode = nextPosition == Vector3.zero ? nodeSection.pathNodes.Last() : uniqueNodeList[randomListValue];

		List<Vector3> calculatedPath = CalculatePath(startNode, endNode);
		Debug.Log($"StartNode: {startNode.gameObject} EndNode: {endNode}");
		foreach(Vector3 node in calculatedPath)
		{
			currentPath.Enqueue(node);
		}
		nextPosition = currentPath.Dequeue();
	}
	#endregion
}

