using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class NodeSection 
{
	#region Variables
	public List<PathNode> pathNodes;
	public int totalNodeConnections;
	public string sectionName;
	#endregion

	#region Functionality
	public void GenerateNodeConnections(int totalConnections)
	{
		foreach(PathNode pathNode in pathNodes)
		{
			//Set all other previous node this node
			//Calculate distances to this node
			Dictionary<float, PathNode> pathNodeDistances = new Dictionary<float, PathNode>();

			foreach(PathNode otherPathNode in pathNodes)
			{
				pathNodeDistances.Add(Vector3.Distance(pathNode.NodePosition, otherPathNode.NodePosition), otherPathNode);
			}

			pathNodes = pathNodeDistances.OrderBy(node => node.Key).Select(node => node.Value).ToList();

			int i = 0;
			while(pathNode.connectedNodes.Count < totalConnections)
			{
				if(pathNodes[i] != pathNode)
				{
					pathNode.connectedNodes.Add(pathNodes[i]);
				}
				i++;
			}
		}
	}
	#endregion
}