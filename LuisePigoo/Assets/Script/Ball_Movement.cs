using UnityEngine;
using System.Collections;

public class Ball_Movement : MonoBehaviour {

	public float speed =1;

	GameObject player;
	Rigidbody rigdbody;
	Vector3 forceDirection;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("player");
		rigdbody = GetComponent<Rigidbody> ();
	}

	void OnTriggerStay (Collider other)
	{
		if(other.gameObject == player){
			forceDirection = transform.position;

			forceDirection.x = player.transform.position.x> forceDirection.x ? -1f:1f;
			forceDirection.y = 0;
			forceDirection.z = player.transform.position.z> forceDirection.z ? -1f:1f;

			rigdbody.AddForce(forceDirection * speed, ForceMode.Impulse);
		}
	}
}
