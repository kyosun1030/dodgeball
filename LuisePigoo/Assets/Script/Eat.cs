using UnityEngine;
using System.Collections;

public class Eat: MonoBehaviour {
	
	public enum EnemyState { move, attack,idle };
	public EnemyState enemystate = EnemyState.move;
	
	private Animator anim;
	
	public Transform Enemy;
	private Transform Ball;
	private NavMeshAgent Agent;
	private bool Attack = false;
	private bool Picked = false;
	private bool iDle = true;
	
	
	public bool isAlive =true;
	
	public float throwPower = 4f;
	public Transform pickPosition;
	public float Speedio = 4;

    public GameObject _hpBar;
	public int Hp = 3;
	
	
	public float speed = 3.0f;
	//public float SpeedX;
	//public float SpeedZ;
	public float minSpeed = -3.0f;
	public float maxSpeed = 3.0f;
	float Ttime = 2.0f;
	
	public float maxX = 8.0f;
	public float minX = -8.0f;
	public float maxY = 14.5f;
	public float minY = 7.0f;
	public Vector3 Sport;

    // GameObject.Find("Ball").SetActive(true); 

    
	public GameObject Win;
	
	
	
	void Start()
	{
		RandomDirection();
		
		
		//적 Ai트랜스폼
		Enemy = GameObject.FindGameObjectWithTag("Target").transform;
		
		//추적대상
		Ball = GameObject.FindWithTag("Ball").GetComponent<Transform>();
		
		//네비매쉬 컴퍼넌트 할당
		Agent = this.gameObject.GetComponent<NavMeshAgent>();
		
		//---
		anim = GetComponent<Animator>();
		//---
		
		StartCoroutine(CheckEnemyState());
		
		StartCoroutine(EnemyAction());
		Win.SetActive (false);
	}
	
	void RandomDirection()
	{
		print("냠");
		Sport = Vector3.right * Random.Range(minSpeed, maxSpeed)
			+ Vector3.forward * Random.Range(minSpeed, maxSpeed);
	}
    //void md()
    //{
    //    SpeedX = transform.position.x;
    //    SpeedZ = transform.position.z;

    //}
	
	IEnumerator CheckEnemyState()
	{
		Ball_Action ballAction = Ball.GetComponent<Ball_Action>();
		while (isAlive)
		{
			
			yield return null;
			
			
			if (!ballAction.isAttacking && ballAction.inEnemySide)
			{
				print("안전볼 && 적영역");
				enemystate = Picked ? EnemyState.attack : EnemyState.move;
			}
			else if (!ballAction.isAttacking && !ballAction.inEnemySide)
			{
				print("안전볼 && 플레이어영역");
				enemystate = EnemyState.idle;
			}
			
			
		}
		
	}
	
	
	IEnumerator EnemyAction()
	{
		while (isAlive)
		{
			yield return null;

            anim.SetFloat("Speed", rigidbody.velocity.magnitude);

			switch (enemystate)
			{
				
				//공을잡으면 공격
			case EnemyState.attack:
				if (!Attack)
				{
					yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
					
					print("ㅇ_");

                    Vector3 dir = (Enemy.position - transform.position).normalized;

                    Vector3 newFor = dir;
                    newFor.y = 0;
                    transform.forward = newFor;

                    float chance = Random.value * 100;
                    if (chance <= 75)
                    anim.SetBool("AttackH", true);
                    else
                    anim.SetBool("AttackJ", true);
                    //ThrowBall();

                    yield return new WaitForEndOfFrame();

                    anim.SetBool("AttackH", false);
                    anim.SetBool("AttackJ", false);

					yield return new WaitForSeconds(1);
					enemystate = EnemyState.move;
				}
				break;
				
				
				
				//  추적대상의 위치를 설정하면 바로 추적시작
			case EnemyState.move:
				Vector3 navPos = Ball.position;
				navPos -= transform.position;
				navPos.y = 0;
				transform.forward = navPos;
				
				transform.position += transform.forward * Speedio * Time.deltaTime;
				break;
				
				
				
				
				//공이 넘어오기전 대기상태
			case EnemyState.idle:
				Ttime -= Time.deltaTime;
				
				if (Ttime <= 0.0f)
				{
					//Ttime = 2.0f;
					Ttime = Random.Range(2.0f, 4.0f);
					RandomDirection();
					
				}
                Vector3 dir = Sport;
                dir.y = transform.position.y;
                transform.forward = dir;
				transform.position += Sport * Time.deltaTime;
				
                //md();
				
				//if (SpeedX >= maxX || SpeedX <= minX)
				//{
				//	print("항");
				//	transform.position -= Sport * Time.deltaTime;
				//	Sport.x *= -1;
				//}
				//
				//if (SpeedZ >= maxY || SpeedZ <= minY)
				//{
				//	print("힝");
				//	transform.position -= Sport * Time.deltaTime;
				//	Sport.z *= -1;
				//}

				break;
				
			}
		}
	}
	
	
	
	
	void PickBall(Transform ball)
	{
		SetEquip(ball.gameObject, true);
       // Ball.gameObject.SetActive(false);
	
		ball.SetParent(pickPosition);
		ball.transform.localPosition = Vector3.zero;
		Attack = false;
		Picked = true;
		iDle = false;
	}
	
	public void ThrowBall()
	{
		Vector3 ballDir = (Enemy.position - pickPosition.position);
        //ballDir = ballDir.normalized * ballDir.magnitude * 0.1f;
		
		Ball.parent = null;
		SetEquip(Ball.gameObject, false);
		Ball.GetComponent<Rigidbody>().AddForce(ballDir * throwPower);
		
		Ball.GetComponent<Ball_Action>().isAttacking = true;
		
		Ball.GetComponent<Ball_Action>().isAttackerPlayer = false;
		
		Attack = true;
		Picked = false;
		iDle = false;
		
	}

	void OnCollisionEnter(Collision col)
	{
		
		if (col.transform.tag == "Ball") {
			Ball_Action bAction = col.gameObject.GetComponent<Ball_Action> ();
			if (bAction.isAttacking && bAction.isAttackerPlayer == true) {
                float chance = Random.Range(0.0f, 100.0f);
                if (chance <= 80)
                {
                    Hp--;
                    print("윽");

                    if (_hpBar != null)
                        _hpBar.transform.localPosition = new Vector3(0.1f, 0, 0);
                    _hpBar.transform.localScale = new Vector3(Hp * 0.2f, 0.2f, 1);
                }

                else
                {
                    PickBall(col.transform);
                }

                if (Hp <= 0)
                {
                    print("으앙죽음ㅋ");
                    if (_hpBar != null) _hpBar.transform.localScale = new Vector3(0, 1, 1);
					Destroy (this.gameObject);
					Win.SetActive(true);
				}
			}
            else if (!bAction.isAttacking)
            {
                PickBall(col.transform);
            }
		}

		if (col.transform.CompareTag ("Wall")) {
			transform.position -= Sport * Time.deltaTime;
			Vector3 newDir = Vector3.zero;
			foreach(ContactPoint point in col.contacts) {
				newDir += transform.position - point.point;
			}
			newDir /= col.contacts.Length;

			Sport = Sport.magnitude * newDir.normalized;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Ball"))
		{
			Ball_Action  bAction = other.GetComponent<Ball_Action>();
			if (!bAction.isAttacking)
			{
				print("마이볼");
				PickBall(other.transform);
				enemystate = EnemyState.attack;
				//Agent.SetDestination(new Vector3())
			}
		}
	}

	void onTriggerStay(Collider other) {
		Ball_Action  bAction = other.GetComponent<Ball_Action>();
		if (!bAction.isAttacking)
		{
			print("마이볼");
			PickBall(other.transform);
			enemystate = EnemyState.attack;
			//Agent.SetDestination(new Vector3())
		}
	}
	
	void SetEquip(GameObject item, bool isEquip)
	{
		Collider[] itemColliders = item.GetComponents<Collider>();
		Rigidbody itemRigibody = item.GetComponent<Rigidbody>();
		
		item.GetComponent<Ball_Action>().isAttacking = !isEquip;
		
		foreach (Collider itemCollider in itemColliders)
		{
			print(itemColliders.Length);
			itemCollider.enabled = !isEquip;
		}
		itemRigibody.isKinematic = isEquip;
	}
}

