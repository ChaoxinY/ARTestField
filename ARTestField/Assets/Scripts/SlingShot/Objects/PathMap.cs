using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityAD;
using System;

public class PathMap : MonoBehaviour, IEventPublisher
{
	#region Variables
	public List<NodeSection> nodeSections;
	public event EventHandler<NodeConnectionsGeneratedEventArgs> ConnectionsGenerated;
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticRefrences.currentPathMap = this;
	}
	private void Start()
	{
		StaticRefrences.EventSubject.Subscribe(this);
		StartCoroutine(GenerateNodeConnections());
	}
	#endregion

	#region Functionality
	public void UnSubscribeFromSubject()
	{
		StaticRefrences.EventSubject.UnSubscribe(this);
	}

	private IEnumerator GenerateNodeConnections()
	{
		foreach(NodeSection nodeSection in nodeSections)
		{
			yield return StartCoroutine(nodeSection.GenerateNodeConnections(nodeSection.totalNodeConnections));
		}
		ConnectionsGenerated(this,new NodeConnectionsGeneratedEventArgs(true));
	}

	private void OnDestroy()
	{
		UnSubscribeFromSubject();
	}
	#endregion
}


