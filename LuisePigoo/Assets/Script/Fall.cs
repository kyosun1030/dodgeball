using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour
{
    public bool enableSpawn = false;

    public GameObject Enemy; //Prefab을 받을 public 변수 입니다.
    public GameObject Enemy2;
    public GameObject Enumy3;
    public GameObject Enumy4;

    float randomX; //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
    float randomZ;


    public void foo()
    {
        StartCoroutine(updateData());
    }



    IEnumerator updateData()
    {
        if (enableSpawn)
        {
            while (true)
            {
                randomX = Random.Range(-7.0f, 7.0f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
                randomZ = Random.Range(-13.0f, 0.0f);

                GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(randomX, 6.0f, randomZ), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                enemy.AddComponent<FallingObject>();
                
                randomX = Random.Range(-7.0f, 7.0f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
                randomZ = Random.Range(-13.0f, 0.0f);
                
                GameObject enemy3 = (GameObject)Instantiate(Enemy, new Vector3(randomX, 6.0f, randomZ), Quaternion.identity);
                enemy3.AddComponent<FallingObject>();

                randomX = Random.Range(-7.0f, 7.0f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
                randomZ = Random.Range(-13.0f, 0.0f);

                GameObject enemy2 = (GameObject)Instantiate(Enemy2, new Vector3(randomX, 6.0f, randomZ), Quaternion.identity);
                enemy2.AddComponent<FallingObject>();

                randomX = Random.Range(-7.0f, 7.0f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
                randomZ = Random.Range(-13.0f, 0.0f);


                GameObject enemy4 = (GameObject)Instantiate(Enemy2, new Vector3(randomX, 6.0f, randomZ), Quaternion.identity);
                enemy2.AddComponent<FallingObject>();

                yield return new WaitForSeconds(0.5f); //2초대기후삭제
                //foo();

                StartCoroutine(KillObject(enemy));
                StartCoroutine(KillObject(enemy2));
                StartCoroutine(KillObject(enemy3));
                StartCoroutine(KillObject(enemy4));

                //yield return new WaitForSeconds(2.0f); //재생성
            }
        }
    }
    void Start()
    {
        StartCoroutine(updateData());
    }

    //void Update()
    //{
    //    randomX = Random.Range(-7.0f, 7.0f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
    //    randomZ = Random.Range(-140.0f, -50.0f); 
    //    //StartCoroutine(updateData());
    //}

    IEnumerator KillObject(GameObject obj)
    {
        yield return new WaitForSeconds(2);

        Destroy(obj);
    }
}
