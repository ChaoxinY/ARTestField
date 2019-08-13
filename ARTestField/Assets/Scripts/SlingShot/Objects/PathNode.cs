using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathNode : MonoBehaviour
{
	#region Variables
	public List<PathNode> ConnectedNodes { get; set; }
	public Vector3 NodePosition { get; private set; }
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
		Color color = Color.red;
		foreach(PathNode item in ConnectedNodes)
		{
			Debug.DrawLine(NodePosition, item.NodePosition, color);
		}
	}
	#endregion

}


