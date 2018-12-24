using UnityEngine;
using System.Collections;

public class FallingObject : MonoBehaviour {

    public bool falling = true;

    private Vector3 randomRot;

    void Start()
    {
        randomRot = Random.insideUnitSphere * 10;
    }

    void Update()
    {
        if (falling)
        transform.Rotate(randomRot);
    }

    void OnCollisionEnter(Collision col)
    {
        StartCoroutine(Falled());
    }

    IEnumerator Falled()
    {
        yield return new WaitForEndOfFrame();

        falling = false;
    }
}
