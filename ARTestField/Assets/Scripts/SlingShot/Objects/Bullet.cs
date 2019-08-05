using UnityEngine;

public class Bullet : MonoBehaviour
{
	#region Variables
	public Rigidbody bulletRigidBody;
	private int bouncesLeft = 2;
	#endregion

	#region Initialization

	private void Awake()
	{
		bulletRigidBody.mass = StaticRefrences.bulletMass;
	}

	#endregion

	#region Functionality
	private void OnCollisionEnter(Collision collision)
	{
		bouncesLeft -=1;
		if(collision.gameObject.tag == "Minion")
		{
			//Destroy(gameObject.transform.parent.gameObject);
		}
		if(bouncesLeft <= 0)
		{
			//Destroy(gameObject.transform.parent.gameObject);
		}
	}
	#endregion
}

