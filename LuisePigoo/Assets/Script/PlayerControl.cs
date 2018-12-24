using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour 
	{

    public Slider healthSlider;
	public GameObject playerEquipPoint;

    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.05f);

    public Image damageImage;

	public float speed=10;
	public float jumpPower = 5f;
	public float rotatespeed = 5;
	public float throwPower = 0;
    public int Oni;
    public int MaxthroePower = 15;
    public int MinthroePower = 1;
	public int Hp = 3;
	public GameObject Lose;
    public bool Grounded = true;
    public bool HolyDash;

    public GameObject Gauae;
    public GameObject Two;
    public GameObject Five;
    public GameObject Seven;
    public GameObject Ten;

    int isDash;

	Rigidbody rbody;
	Animator animator;

	Vector3 movement;
	float horizontalMove;
	float verticalMove;
	bool isJumping;
	bool isPicking;
	bool isAimming;
    bool damaged;
    bool getHit;

    LineRenderer lineRenderer;
	void Start()
	{
		Lose.SetActive (false);
        HolyDash = true;
        isDash = 0;
	}
	void Awake()
	{
		rbody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();

        lineRenderer = gameObject.GetComponentInChildren<LineRenderer>();
	}

	bool isAttacking = false;

	void Update()
	{

        healthSlider.value = /*Hp;*/ Mathf.Lerp(healthSlider.value, (float)Hp, Time.deltaTime);
		
		//움직이기 
		horizontalMove = Input.GetAxisRaw ("Horizontal");
		verticalMove = Input.GetAxisRaw ("Vertical");
        
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)  && HolyDash == true)
        {
            speed = 10;
            HolyDash = false;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && HolyDash == true)
        {
            speed = 10;
            HolyDash = false;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && HolyDash == true)
        {
            speed = 10;
            HolyDash = false;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) && HolyDash == true)
        {
            speed = 10;
            HolyDash = false;
        }

        if (HolyDash == false)
        {

            isDash++;
            if (isDash == 20)
            {
                speed = 3;
            }
            if (isDash > 100)
            {
                HolyDash = true;
                isDash = 0;
            }
        }



		//점프 
        if (Input.GetKeyDown(KeyCode.Q)&&Grounded == true)
			isJumping = true;
        if (this.gameObject.transform.position.y < 0.1f)
        {
            Grounded = true;
        }

		//if (!lineRenderer.gameObject.activeSelf)
			lineRenderer.gameObject.SetActive(isPicking);
		AimLine(isPicking);
		
		//버리기 
		if (Input.GetKey(KeyCode.E) && isPicking && !isAttacking)
        {
			isAttacking = true;
			animator.SetTrigger("Shoot");
            throwPower = 0;
        }
		if (Input.GetKey (KeyCode.E) && isPicking)
		{
            Gauae.SetActive(true);
            //.GetComponent().enabled = false;
             throwPower += 10 * Time.deltaTime;
             //Oni = (int)throwPower;
            
            //if (throwPower >= MaxthroePower)
            //{
            //    throwPower = MinthroePower;

            //    switch ((int)throwPower)
            //    {

            //        case 3:
            //            Two.SetActive(true);
            //            break;

            //        case 6:
            //            Two.SetActive(false);
            //            Five.SetActive(true);
            //            break;
            //        case 9:
            //            Seven.SetActive(true);
            //            Five.SetActive(false);
            //            break;
            //        case 12:
            //            Seven.SetActive(false);
            //            Ten.SetActive(true);
            //            break;
            //        default:
            //            Ten.SetActive(false);
            //            break;
            //    }
            //}

                //lineRenderer.gameObject.SetActive(false); 
			print ("플레이어 공격");
			//Drop ();
		}

        if (throwPower >= MaxthroePower)
        {
            throwPower = MinthroePower;
        }
        switch ((int)throwPower)
        {

            case 3:
                Two.SetActive(true);
                break;

            case 6:
                Two.SetActive(false);
                Five.SetActive(true);
                break;
            case 9:
                Seven.SetActive(true);
                Five.SetActive(false);
                break;
            case 12:
                Seven.SetActive(false);
                Ten.SetActive(true);
                break;
            default:
                Ten.SetActive(false);
                break;
        }

		AnimationUpdate ();

        if(damaged){

            damageImage.color = flashColor;
          }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color,Color.clear,flashSpeed*Time.deltaTime);
        }
        damaged = false;
		
	}

	void FixedUpdate ()
	{
        if (getHit) return;

		Run ();
		Jump();
		Turn ();

	}
    //IEnumerator Udate() { 
    void OnCollisionEnter(Collision other)
    {

        if (other.transform.tag == "Ball")
        {
            Ball_Action bAction = other.gameObject.GetComponent<Ball_Action>();
            if (bAction.isAttacking && bAction.isAttackerPlayer == false)
            {
                Hp--;
                StartCoroutine(HitProcess(true));
                
                print("플레이어 얻어맞음");
                animator.SetTrigger("Damage");
                // yield return new WaitForSeconds(1);
                damaged = true;


                if (Hp <= 0)
                {
                    //healthSlider.value = 0;
                    print("플레이어죽음ㅋ");
                    //Destroy(this.gameObject);
                    Lose.SetActive(true);
                }
            }
        }
        else if (other.transform.tag == "Hurdle")
        {
            rbody.AddExplosionForce(500f, other.contacts[0].point, 10f);

            Hp--;
            print("플레이어 얻어맞음");

            if (Hp <= 0)
            {
                print("플레이어죽음ㅋ");

                Lose.SetActive(true);
            }
        }
        else if(other.transform.tag == "Drop")
        {
            FallingObject fo = other.gameObject.GetComponent<FallingObject>();
            if (fo.falling)
            {
                animator.SetBool("Dozed", true);
                StartCoroutine(HitProcess(false));
            }
        }
      
    }


	void AnimationUpdate()
	{
		if (horizontalMove == 0 && verticalMove == 0) {
			animator.SetBool("IsRunning", false);		
		} 
		else 
		{
			animator.SetBool("IsRunning", true);
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			//animator.SetBool ("isThrowing", true);
		} else {
			//animator.SetBool ("isThrowing", false);
		}

	}

	void Run()
	{
		movement.Set (horizontalMove, 0, verticalMove);
		movement = movement.normalized * speed * Time.deltaTime;

		rbody.MovePosition (transform.position + movement);
	}

	void Turn()
	{
		if (movement.magnitude > 0) {
			Quaternion newRotation = Quaternion.LookRotation (movement);
			rbody.rotation = Quaternion.Slerp (rbody.rotation, newRotation, rotatespeed * Time.deltaTime);
		}
	}


	void Jump()
	{
		if (!isJumping)
			return;

		rbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

		isJumping = false;
        Grounded = false;
	}

	public void Pickup(GameObject ball)
	{
		SetEquip (ball, true);
		animator.SetTrigger ("doPickup");
		isPicking = true;
	}

    Vector3 AimLine(bool ret)
    {
        //Ray준비
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        float rayLength = 500f;
        int floorMask = LayerMask.GetMask("plane");
        Vector3 throwAngle;

        //RayHit방향 
        if (Physics.Raycast(ray, out rayHit, rayLength, floorMask))
        {
            throwAngle = rayHit.point - playerEquipPoint.transform.position;

            if (ret)
            {
                lineRenderer.SetPosition(0, playerEquipPoint.transform.position);
                lineRenderer.SetPosition(1, rayHit.point);
            }

            return throwAngle;
        }
        else
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);

            return transform.forward;
        }
    }

	public void Drop()
	{
		isAttacking = false;

		//아이템 호출 
		GameObject ball = playerEquipPoint.GetComponentInChildren<Rigidbody> ().gameObject;
		Rigidbody rigibody = ball.GetComponent<Rigidbody> ();
		
		ball.GetComponent<Ball_Action>().isAttackerPlayer = true;
		
		//종속 관계 해제 
		SetEquip (ball, false);
		playerEquipPoint.transform.DetachChildren ();

        /*----------------------------------------
		//Ray준비
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit rayHit;
		float rayLength = 500f;
		int floorMask = LayerMask.GetMask ("plane");
         * --------------------------------------*/
        Vector3 throwAngle = AimLine(false);

        /*---------------------------------------
		//RayHit방향 
		if (Physics.Raycast (ray, out rayHit, rayLength, floorMask)) {
			throwAngle = rayHit.point - playerEquipPoint.transform.position;
		}
		//정면방향
		else {
			throwAngle = transform.forward;
		}

         * --------------------------------------*/

		//던지기 
		rigibody.AddForce (throwAngle * throwPower, ForceMode.Impulse);
		
		//animator.SetTrigger ("doThrow");
		isAimming = false;
		isPicking = false;

        throwPower = 0;
        Gauae.SetActive(false);
        Two.SetActive(false);
        Five.SetActive(false);
        Seven.SetActive(false);
        Ten.SetActive(false);
		
		
		playerEquipPoint.transform.DetachChildren ();
		
		SetEquip (ball, false);
		
		isPicking = false;
	}
	void SetEquip(GameObject ball,bool isEquip)
	{
		Collider[] ballColliders = ball.GetComponents<Collider> ();
		Rigidbody ballRigibody = ball.GetComponent<Rigidbody> ();

        ball.GetComponent<Ball_Action>().isAttacking = !isEquip;

		foreach (Collider ballCollider in ballColliders) {
			//print (ballColliders.Length);
			ballCollider.enabled = !isEquip;
		}
		ballRigibody.isKinematic = isEquip;
	}


    void Aim()
  {
		if(isAimming)
			return;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayHit;
		float rayLength = 500f;
		int floorMask = LayerMask.GetMask("Plane");
		
		if (Physics.Raycast (ray, out rayHit, rayLength, floorMask)) {
			Debug.DrawRay (playerEquipPoint.transform.position, rayHit.point, Color.red);
			
			Vector3 playerToMouse = rayHit.point - playerEquipPoint.transform.position;
			playerToMouse.y = 0f;
			
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			rbody.MoveRotation (newRotation);
		}
	}

    IEnumerator HitProcess(bool type)
    {
        getHit = true;

        if (Hp > 0 && type)
        {
            yield return new WaitForSeconds(4.5f);
        }
        else
        {
            yield return new WaitForSeconds(2.1f);
            animator.SetBool("Dozed", false);
            if (Hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        getHit = false;
    }
}





