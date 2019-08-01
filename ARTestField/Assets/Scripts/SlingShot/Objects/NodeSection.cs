using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeSection : MonoBehaviour
{
	#region Variables
	public List<PathNode> pathNodes;
	public int totalNodeConnections;
	public string sectionName;
	#endregion

	#region Initialization
	#endregion

	#region Functionality
	public IEnumerator GenerateNodeConnections(int totalConnections)
	{	
		foreach(PathNode pathNode in pathNodes)
		{
			//Set all other previous node this node
			//Calculate distances to this node
			Dictionary<PathNode, float> pathNodeDistances = new Dictionary<PathNode,float>();

			foreach(PathNode otherPathNode in pathNodes)
			{
				pathNodeDistances.Add(otherPathNode, Vector3.Distance(pathNode.NodePosition, otherPathNode.NodePosition));
			}	
			pathNodes = pathNodeDistances.OrderBy(node => node.Value).Select(node => node.Key).ToList();

			int j = 0;
			int i = 0;
			while(j < totalConnections)
			{
				//Closest is not the node itself and connected node does not contain the closest
				if(pathNodes[i] != pathNode)
				{			
					pathNode.connectedNodes.Add(pathNodes[i]);
					//pathNodes[i].connectedNodes.Add(pathNode);
					j++;
				}			
				i++;
				yield return null;
			}
		}
	}
	#endregion
}