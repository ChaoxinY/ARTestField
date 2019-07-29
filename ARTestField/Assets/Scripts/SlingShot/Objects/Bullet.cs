using UnityEngine;

public class Bullet : MonoBehaviour
{
	#region Variables
	public float lifeTime;
	public Rigidbody bulletRigidBody;
	private int bouncesLeft = 2;
	#endregion

	#region Initialization

	private void Awake()
	{
		bulletRigidBody.mass = StaticRefrences.bulletMass;
	}

	private void Start()
	{	
		Destroy(gameObject.transform.parent.gameObject, lifeTime);
	}
	#endregion

	#region Functionality
	private void OnCollisionEnter(Collision collision)
	{
		bouncesLeft -=1;
		if(collision.gameObject.tag == "Minion")
		{
			Destroy(gameObject.transform.parent.gameObject);
		}
		if(bouncesLeft <= 0)
		{
			Destroy(gameObject.transform.parent.gameObject, lifeTime);
		}
	}
	#endregion
}

