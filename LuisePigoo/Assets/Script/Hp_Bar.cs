using UnityEngine;
using System.Collections;

public class Hp_Bar : MonoBehaviour {
	
	// 타겟 오브젝트 변수 선언
	public GameObject moaiMan;
	// hp 값 표현 오브젝트 변수 선언
	private TextMesh _HpVal;
	// 타겟 오브젝트 이름 표현 오브젝트 변수 선언
	private TextMesh _name;
	// hp bar 오브젝트 변수 선언
	private GameObject _hpBar;
	
	// Use this for initialization
	void Start () {
		
		//타겟이 존재할때
        if (moaiMan != null)
		{			
			_hpBar = transform.FindChild("1_HpBarParent").gameObject;

		}
	}
	
	// Update is called once per frame
	void Update () {
		
		// 타겟 오브젝트를 따라다님
        transform.position = new Vector3(moaiMan.transform.position.x, transform.position.y, moaiMan.transform.position.z);
		
	}
}
