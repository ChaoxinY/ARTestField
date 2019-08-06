using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class LevelInitializer : MonoBehaviour, IEventPublisher
{
	#region Variables
	public float levelLength;
	public event EventHandler LevelStarted;
	public event EventHandler LevelEnded;
	public List<GameObject> startingDummies;
	public Text countDownText;
	#endregion

	#region Initialization
	private void Start()
	{
		StaticRefrences.EventSubject.Subscribe(this);
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
		StaticRefrences.EventSubject.UnSubscribe(this);
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

