using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class LevelInitializer : MonoBehaviour, IEventPublisher
{
	#region Variables
	public event EventHandler LevelStarted;
	public event EventHandler LevelEnded;
	[SerializeField]
	private readonly List<GameObject> startingDummies;
	[SerializeField]
	private readonly Text countDownText;
	[SerializeField]
	private readonly float levelLength;
	#endregion

	#region Initialization
	private void Start()
	{
		StaticReferences.EventSubject.Subscribe(this);
	}
	#endregion

	#region Functionality
	public void StartLevel()
	{
		foreach(GameObject gameObject in startingDummies)
		{
			if(gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
		LevelStarted?.Invoke(this, new EventArgs());
		StartCoroutine(LevelCountDown());
	}

	private void OnDestroy()
	{
		UnSubscribeFromSubject();
	}

	public void UnSubscribeFromSubject()
	{
		StaticReferences.EventSubject.UnSubscribe(this);
	}

	private IEnumerator LevelCountDown()
	{
		float timeLeft = levelLength;

		while(timeLeft > 0)
		{
			timeLeft -= Time.fixedDeltaTime;
			yield return new WaitForSeconds(Time.fixedDeltaTime);
			countDownText.text = ((int)timeLeft).ToString();
		}

		LevelEnded?.Invoke(this, new EventArgs());

	}
	#endregion
}

