using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
	public List<PathNode> connectedNodes;
	[System.NonSerialized]
	public Vector3 nodePosition;
	[System.NonSerialized]
	public List<PathNode> previousPathNodes = new List<PathNode>();
	[System.NonSerialized]
	public PathNode previousNode;
	[System.NonSerialized]
	public PathNode endGoal;
	[System.NonSerialized]
	public float pathLength;
	public float DistanceToPreviousNode
	{
		get
		{
			float distance = previousNode == null ? 0 : Vector3.Distance(nodePosition, previousNode.nodePosition); return distance;
		}
	}
	public float DistanceToGoal { get { return Vector3.Distance(nodePosition, endGoal.nodePosition);}}
	public float PathWeight { get { return pathLength + DistanceToGoal; } }

	private void Awake()
	{
		nodePosition = transform.position;
	}

	void FixedUpdate()
	{
		// always draw a 5-unit colored line from the origin
		Color color = Color.red;
		foreach (PathNode item in connectedNodes)
		{
			Debug.DrawLine(nodePosition, item.nodePosition, color);
		}

	}
}


