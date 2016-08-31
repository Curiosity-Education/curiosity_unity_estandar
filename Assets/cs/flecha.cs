using UnityEngine;
using System.Collections;


public class flecha : MonoBehaviour {
	GameObject camara;
	void Awake(){
		camara = GameObject.Find ("Main Camera");
	}
	 void OnMouseDown()
	{
		switch (gameObject.tag) {
			case "Left":
				camara.GetComponent<SEINSTAN> ().Check (0);
			break;
			case "Right":
				camara.GetComponent<SEINSTAN> ().Check (1);
			break;
			case "Bottom":
				camara.GetComponent<SEINSTAN> ().Check (2);
			break;
		}
	}
}
