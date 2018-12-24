using UnityEngine;
using System.Collections;

public class Ball_Action : MonoBehaviour {
	
	public GameObject player;
	public GameObject playerEquipPoint;
	public PlayerControl PlayerLogic;
	
	Vector3 forceDirection;
	public bool isPlayerEnter;
	
	public bool isAttackerPlayer;
	public bool isAttacking;
	
	public bool inEnemySide;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerEquipPoint = GameObject.FindGameObjectWithTag ("EquipPoint");
		
		PlayerLogic = player.GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Action") && isPlayerEnter) {
			
			transform.SetParent (playerEquipPoint.transform);
			transform.localPosition = Vector3.zero;
			transform.rotation = new Quaternion (0, 0, 0, 0);
			
			PlayerLogic.Pickup(this.gameObject);
			
			isPlayerEnter = false;
		}
		
		inEnemySide = transform.position.z > 0 ? true : false;
	}
	
	void OnTriggerEnter(Collider other)
	{
		//print ("dd");
		if (other.CompareTag ("Player")) {
			isPlayerEnter = true;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject == player) {
			isPlayerEnter = false;
		}
	}
	
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag("Ground"))
		{
			isAttacking = false;
		}
	}
	
}





