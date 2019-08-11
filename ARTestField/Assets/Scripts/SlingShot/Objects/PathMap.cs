using System.Collections.Generic;
using UnityEngine;

public class PathMap : MonoBehaviour
{
	#region Variables
	[SerializeField]
	private List<NodeSection> nodeSections;
	public List<INodeSection> INodeSections = new List<INodeSection>();
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticReferences.currentPathMap = this;
		foreach(NodeSection nodeSection in nodeSections)
		{
			INodeSections.Add(nodeSection);
		}
	}
	#endregion

	#region Functionality
	#endregion
}


