using UnityEngine;
using System.Collections;

public class AimCamera : MonoBehaviour {

    public float turnSpeed = 100;

	void Update () {
        float v = Input.GetAxis("Mouse Y");
        float h = Input.GetAxis("Mouse X");

        transform.Rotate(v, h, 0);
	}
}
