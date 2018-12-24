using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameStart : MonoBehaviour {

	public GameObject MenuPlaying;
	public GameObject MenuTitle;
	public GameObject MenuExit;
	public GameObject Display;
	public GameObject MenuRePlay;
	public GameObject WindowHelp;

    public GameObject ilban;
    public GameObject JangAe;
    public GameObject Woobak;
	int Play;

	// Use this for initialization
	void Start () {
		MenuPlaying.gameObject.SetActive (false);
		MenuTitle.gameObject.SetActive (false);
		MenuRePlay.gameObject.SetActive (false);
		Display.gameObject.SetActive (false);
		MenuExit.gameObject.SetActive (false);
		WindowHelp.gameObject.SetActive (false);
        ilban.gameObject.SetActive(false);
        JangAe.gameObject.SetActive(false);
        Woobak.gameObject.SetActive(false);
		Time.timeScale = 1.0f;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(Play == 1)
			{
				Suljung();
				Play = 0;
			}
			else if(Play == 0)
			        {
				Playing();
				Play = 1;
			}
		}
	}

    public void TitleGameStart()
    {
        ilban.gameObject.SetActive(true);
        JangAe.gameObject.SetActive(true);
        Woobak.gameObject.SetActive(true);
        Display.gameObject.SetActive(true);
    }

	public void Suljung()
	{
		MenuPlaying.gameObject.SetActive (true);
		MenuTitle.gameObject.SetActive (true);
		MenuRePlay.gameObject.SetActive (true);
		Display.gameObject.SetActive (true);
		MenuExit.gameObject.SetActive (true);
		Time.timeScale = 0.0f;
	}

	public void Playing()
	{
		MenuPlaying.gameObject.SetActive (false);
		MenuTitle.gameObject.SetActive (false);
		MenuRePlay.gameObject.SetActive (false);
		Display.gameObject.SetActive (false);
		MenuExit.gameObject.SetActive (false);
		Time.timeScale = 1.0f;
	}

    public void TitleDisplay()
    {
        ilban.gameObject.SetActive(false);
        JangAe.gameObject.SetActive(false);
        Woobak.gameObject.SetActive(false);
        Display.gameObject.SetActive(false);
    }

	public void Help()
	{
		WindowHelp.gameObject.SetActive (true);
	}

	public void WindowsHelp()
	{
		WindowHelp.gameObject.SetActive (false);
	}

	public void ilbanSean()
	{
		Application.LoadLevel ("Ilban");
		Time.timeScale = 1.0f;
		//Debug.Log ("클릭 완료");
	}
    public void JangAeSean()
    {
        Application.LoadLevel("JangAe");
        Time.timeScale = 1.0f;
    }
    public void WoobakSean()
    {
        Application.LoadLevel("Woobak");
        Time.timeScale = 1.0f;
    }
	public void ChangeScean()
	{
		Application.LoadLevel ("TitleMain");
		Time.timeScale = 1.0f;
	}
	public void Exit()
	{
		Application.Quit ();
	}
}
