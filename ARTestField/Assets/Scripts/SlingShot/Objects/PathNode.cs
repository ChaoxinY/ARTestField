using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathMap : MonoBehaviour
{
	#region Variables
	public List<Path> paths;
	#endregion

	#region Initialization
	private void Start()
	{
		StaticRefrences.currentPathMap = this;
	}
	#endregion
}

public class Path : MonoBehaviour
{
	#region Variables
	public List<PathNode> pathNodes;
	public int totalNodeConnections;
	public string pathName;
	#endregion

	#region Initialization
	private void Start()
	{
		GenerateNodeConnections(totalNodeConnections);
	}
	#endregion

	#region Functionality
	private void GenerateNodeConnections(int totalConnections)
	{
		foreach(PathNode pathNode in pathNodes)
		{
			//Set all other previous node this node
			//Calculate distances to this node
			foreach(PathNode otherNode in pathNodes)
			{
				otherNode.PreviousNode = pathNode;
			}
			pathNodes = pathNodes.OrderBy(node => node.DistanceToPreviousNode).ToList();
			for(int i = 0; i < totalConnections; i++)
			{
				pathNode.connectedNodes.Add(pathNodes[i]);
			}
		}	
	}
	#endregion
}

public class PathNode : MonoBehaviour
{
	#region Variables
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
			float distance = PreviousNode == null ? Mathf.Infinity : Vector3.Distance(NodePosition, PreviousNode.NodePosition); return distance;
		}
	}
	public float DistanceToGoal { get { return Vector3.Distance(NodePosition, EndGoal.NodePosition);}}
	public float PathWeight { get { return PathLength + DistanceToGoal; } }
	#endregion

	#region Initialization
	private void Awake()
	{
		NodePosition = transform.position;
	}
	#endregion

	#region Functionality
	void FixedUpdate()
	{
		// always draw a 5-unit colored line from the origin
		Color color = Color.red;
		foreach (PathNode item in connectedNodes)
		{
			Debug.DrawLine(NodePosition, item.NodePosition, color);
		}
	}
	#endregion

}


