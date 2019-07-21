using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	#region Variables
	public float lifeTime;
	public Rigidbody bulletRigidBody;
	#endregion

	#region Initialization

	private void Start()
	{
		bulletRigidBody.mass = StaticRefrences.bulletMass;
		Destroy(gameObject.transform.parent.gameObject, lifeTime);
	}
	#endregion

	#region Functionality
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Minion")
		{
			Destroy(gameObject.transform.parent.gameObject);
		}
	}
	#endregion
}

