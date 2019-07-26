using System.Collections.Generic;
using UnityEngine;

interface IPathFinder
{
	List<Vector3> CalculatePath(List<PathNode> nodeMap, PathNode startNode, PathNode Goal);
}
