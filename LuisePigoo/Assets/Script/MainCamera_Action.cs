using UnityEngine;
using System.Collections;

public class MainCamera_Action : MonoBehaviour {

	public GameObject player;

	public float offsetX = 0f;
	public float offsetY = 4.47f;
	public float offsetZ = -4.31f;
	public float followSpeed = 5;

	Vector3 camerePosition;

	void LateUpdate()
	{
		camerePosition.x = player.transform.position.x + offsetX;
		camerePosition.y = player.transform.position.y + offsetY;
		camerePosition.z = player.transform.position.z + offsetZ;

		transform.position = Vector3.Lerp (transform.position, camerePosition, followSpeed * Time.deltaTime);

	}
}
