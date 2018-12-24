using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

    private float a = 1f;
    //int b = 1;
    public float Speed = 3.0f;

    void Update()
    {
        //if (transform.localPosition.x < -7f)
        //{
        //    a = -1f;
        //}
        //else if (transform.localPosition.x > 7f)
        //{
        //    a = 1f;
        //}
        //else if (transform.localPosition.z < -0.1f)
        //{
        //    b = -1;
        //}
        //else if (transform.localPosition.z > 0.1f)
        //{
        //    b = 1;
        //}

        transform.Translate(Vector3.left * Speed * Time.deltaTime  * a);
       
       // transform.Translate(Vector3.left * 5.0f * Time.deltaTime * Speed * b);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            a *= -1;
        }
    }
}