using UnityEngine;
using System.Collections;

public class AiMove : MonoBehaviour
{

    public float speed = 3.0f;
    public float SpeedX;
    public float SpeedZ;
    public float minSpeed = 1.0f;
    public float maxSpeed = 3.0f;
    float Ttime = 2.0f;

    public float maxX = 8.0f;
    public float minX = -8.0f;
    public float maxY = 14.5f;
    public float minY = 7.0f;

    public Vector3 Sport;
    void md()
    {
         SpeedX = transform.position.x;
         SpeedZ = transform.position.z;

    }

 

    //랜덤움직임
    void Move()
    {
        print("냠");
        Sport = Vector3.right * Random.Range(minSpeed, maxSpeed)
                        + Vector3.forward * Random.Range(minSpeed, maxSpeed);
    }

    void Start()
    {
        Move();
    }

    void Update()
    {

        Ttime -= Time.deltaTime;

        if (Ttime <= 0.0f)
        {
            //Ttime = 2.0f;
            Ttime = Random.Range(1.0f, 3.0f);
            Move();

        }

        transform.position += Sport * Time.deltaTime;
        md();
        
        if (SpeedX >= maxX || SpeedX <= minX)
        {
            print("냠");
            transform.position -= Sport * Time.deltaTime;
            Sport.x *= -1;
        }

        if (SpeedZ >= maxY || SpeedZ <= minY)
        {
            print("냠");
            transform.position -= Sport * Time.deltaTime;
            Sport.z *= -1;
        }
    }
}
