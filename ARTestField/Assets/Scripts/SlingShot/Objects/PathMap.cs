using System.Collections.Generic;
using UnityEngine;

public class PathMap : MonoBehaviour
{
	#region Variables
	public List<NodeSection> nodeSections;
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticRefrences.currentPathMap = this;
	}

	#endregion

	#region Functionality
	#endregion
}


