using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathMap : MonoBehaviour
{
	#region Variables
	public List<NodeSection> nodeSection;
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticRefrences.currentPathMap = this;
		foreach(NodeSection nodeSection in nodeSection)
		{
			nodeSection.GenerateNodeConnections(nodeSection.totalNodeConnections);
		}		
	}
	#endregion
}
	

