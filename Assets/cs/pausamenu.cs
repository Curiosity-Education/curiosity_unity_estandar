using UnityEngine;
using System.Collections;

public class pausamenu : MonoBehaviour {
	public GameObject menu_pausa;
	// Use this for initialization
	void Start () {
		menu_pausa.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			cambio ();	
		} 
	}
	void cambio(){
		if (Time.timeScale == 1)
			pausar ();
		else
			continuar ();
	}
	public void pausar(){
		GameObject.Find ("Main Camera").GetComponent<SEINSTAN>().enabled=false;
		menu_pausa.SetActive (true);
		GameObject.Find ("Main Camera").GetComponent<AudioSource>().Pause();
		Time.timeScale = 0;
	}
	public void continuar(){
		GameObject.Find ("Main Camera").GetComponent<SEINSTAN>().enabled=true;
		menu_pausa.SetActive (false);
		GameObject.Find ("Main Camera").GetComponent<AudioSource>().Play();
		Time.timeScale = 1;
	}
	void OnMouseUp(){
		pausar ();
	}
}
