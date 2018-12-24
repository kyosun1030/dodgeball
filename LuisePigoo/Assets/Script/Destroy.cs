using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {
    public int Hp = 3;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Ball")
        {
            if (other.GetComponent<Ball_Action>().isAttacking)
            {
                Hp--;

                if (Hp <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

}
