using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class BulletSpawner : MonoBehaviour
{
    #region Variables
    public GameObject bulletPrefab;
    public Text debugText;
    public float maximumLaunchForce;
    private Touch touch;
    private float lastFiredAngle;
	private Quaternion originRotation;
    #endregion

    #region Initialization
    private void Start()
    {
		originRotation = transform.localRotation;
		SubscribeEvent();
    }
    #endregion

    #region Functionality
    private void Update()
    {
        debugText.text = $"lastFiredAngle: {lastFiredAngle}";
    }
    private void SubscribeEvent()
    {
        foreach (IEventPublisher eventPublisher in StaticRefrences.EventSubject.EventPublishers)
        {
            if (eventPublisher.GetType() == typeof(SlingShot.InputHandler))
            {
                SlingShot.InputHandler inputHandler = (SlingShot.InputHandler)eventPublisher;
                inputHandler.TouchDetected += OnTouchDetected;
            }
        }
    }

    private void OnTouchDetected(object sender, UserTouchEventArgs userTouchEventArgs)
    {
        touch = userTouchEventArgs.Touch;

        if (touch.position.y <StaticRefrences.MinimumVerticalPoint && touch.phase == TouchPhase.Ended)
        {
            FireSlingShot(CalculateSlingShotForce());
        }
    }
	private Vector3 CalculateSlingShotForce()
    {
        Vector3 force = Vector3.zero;
        //offset from the origin to the touch
        //max drag distance is screen.width*0.3
        float maxDragDistance = Screen.width * 0.3f;
        Vector2 originPoint = StaticRefrences.SlingerOriginPoint;

		float adjescentLength = Math.Abs(originPoint.y - touch.position.y);	
		float oppositeLength = Math.Abs(originPoint.x - touch.position.x);
		float angle = (float)Math.Atan(oppositeLength/adjescentLength) * (float)(180.0 / Math.PI);

		//Debugger.DebugObject(this, $"adjescentLength:{adjescentLength} oppositeLength:{oppositeLength}");
		int rotationDirection = touch.position.x > originPoint.x ? -1 : 1 ;
		//transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle*rotationDirection, 0);
		transform.Rotate(new Vector3(0,1,0), angle*rotationDirection/2,Space.World);
		transform.Rotate(new Vector3(1, 0, 0), -20);
		lastFiredAngle = angle;
		float launchForce = maximumLaunchForce * Mathf.Clamp(Vector2.Distance(originPoint, touch.position) / (Screen.width * 0.3f), 0, 1f);
		force = transform.forward * launchForce;
		transform.localRotation = originRotation;
		return force;
    }

    //Add vector parameter
    public void FireSlingShot(Vector3 force)
    {
		Debug.Log($"Force{force}");
		GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponentInChildren<Rigidbody>().AddForce(force, ForceMode.Impulse);	
	}
    #endregion
}

