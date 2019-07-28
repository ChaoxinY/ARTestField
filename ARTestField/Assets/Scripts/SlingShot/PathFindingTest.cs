using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

class PathFindingTest : MonoBehaviour
{
	public List<PathNode> pathNodes;
	private Queue<Vector3> path = new Queue<Vector3>();
	private Vector3 nextPosition;
	private void Start()
	{
		StartCoroutine(CalculatePath());
	}

	private IEnumerator CalculatePath()
	{
		List<Vector3> calculatedPath =  new AStarSearchAlgorithm().CalculatePath(pathNodes, pathNodes[0], pathNodes.Last());
		foreach (Vector3 node in calculatedPath)
		{
			path.Enqueue(node);
		}
		nextPosition = path.Dequeue();

		yield break;
	}

	private void Update()
	{
		if (Vector3.Distance(transform.position, nextPosition) > 0.001f)
		{
			transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime);
		}
		else if (path.Count != 0)
		{
			nextPosition = path.Dequeue();
		}
	}
}


