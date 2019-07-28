using System.Collections.Generic;
using UnityEngine;

public class PathMap
{
	public List<Path> paths = new List<Path>();
}

public class Path
{
	public List<PathNode> pathNodes = new List<PathNode>();
	public string pathName;
}

public class PathNode : MonoBehaviour
{
	public List<PathNode> connectedNodes;
	public Vector3 NodePosition { get; set; }
	public List<PathNode> PreviousPathNodes { get; set; } = new List<PathNode>();
	public PathNode PreviousNode { get; set; }
	public PathNode EndGoal { get; set; }
	public float PathLength { get; set; }
	public float DistanceToPreviousNode
	{
		get
		{
			float distance = PreviousNode == null ? 0 : Vector3.Distance(NodePosition, PreviousNode.NodePosition); return distance;
		}
	}
	public float DistanceToGoal { get { return Vector3.Distance(NodePosition, EndGoal.NodePosition);}}
	public float PathWeight { get { return PathLength + DistanceToGoal; } }

	private void Awake()
	{
		NodePosition = transform.position;
	}

	void FixedUpdate()
	{
		// always draw a 5-unit colored line from the origin
		Color color = Color.red;
		foreach (PathNode item in connectedNodes)
		{
			Debug.DrawLine(NodePosition, item.NodePosition, color);
		}

	}
}


