using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class juego : MonoBehaviour {

	int intentos = 0;
	int valorPuntos = 100;
	double puntajeActual  = 0;
    double	puntajeMaximo = 0;
	double eficiencia = 0;
	int continuo = 0;
	public GameObject score;
	public GameObject menu_fin;
	public GameObject txt_fin;
	public GameObject menu_pausa;
	int aciertos = 0;
	GameObject temp_conten;
	GameObject txt_score_fin;
	

	void Awake(){
		temp_conten  = GameObject.Find ("temp_conten");
		txt_score_fin = GameObject.Find ("txt_score");
	
	}
	// Use this for initialization
	void Start () {
	
	}
	

	public void finalizar(){
        Debug.Log(puntajeActual);
		txt_fin.GetComponent<Text> ().text = ((puntajeActual)) +" pts";
		if (intentos == 0) {
			eficiencia = 0;
		} else {
			eficiencia = (int)(aciertos * 100) / intentos;
		}
		aciertos = 0;
		continuo = 0;
		if (puntajeActual > puntajeMaximo) {
			puntajeMaximo = puntajeActual;
		}
		intentos = 0;
		puntajeActual = 0;
		GameObject.Find ("Main Camera").GetComponent<SEINSTAN>().enabled = false;
		GameObject.Find ("Main Camera").GetComponent<AudioSource>().enabled=false;
		menu_fin.gameObject.SetActive (true);
		Application.ExternalCall ("parent.$juego.game.finish_game_unity");
	}
	public void reiniciar(){
		aciertos = 0;
		intentos = 0;
		continuo = 0;
		puntajeActual = 0;
		temp_conten.GetComponent<loding_temp> ().reiniciar ();
		temp_conten.GetComponent<loding_temp> ().fin = false;
		menu_fin.gameObject.SetActive (false);
		menu_pausa.gameObject.SetActive (false);
		Time.timeScale = 1;
		txt_score_fin.GetComponent<Text> ().text = "0 pts";
		GameObject.Find ("Main Camera").GetComponent<SEINSTAN>().enabled = true;
		Application.ExternalCall ("parent.$juego.game.restart_game_unity");
		GameObject.Find ("Main Camera").GetComponent<AudioSource>().enabled=true;
		GameObject.Find ("Main Camera").GetComponent<SEINSTAN> ().reiniciar ();
	}
	public void salir(){
		Application.ExternalCall ("parent.$juego.game.unity.salir");
	}
	void setCombo(){

	}
	public void setCorrecto(){
		continuo++;
		aciertos++;
		intentos++;
		puntajeActual += valorPuntos;
		txt_score_fin.GetComponent<Text>().text = puntajeActual+" pts";
		determinarCombo();
		Application.ExternalCall ("parent.$juego.game.setCorrecto");
	}
	public void setError(int puntosMenos){
		continuo = 0;
		intentos++;
		if(puntajeActual>puntosMenos){
			txt_score_fin.GetComponent<Text>().text = (puntajeActual-=puntosMenos)+" pts";
		}
		valorPuntos = 100;
		Application.ExternalCall ("parent.$juego.game.setError",50);

	}
	void determinarCombo(){
		if(continuo!=0){
			if(continuo == 5){
				valorPuntos = 150;
			}else if(continuo == 10){
				valorPuntos = 150;		
			}else if(continuo == 15){
				valorPuntos = 500;
			}else if(continuo == 20){
				valorPuntos = 1000;
			}
		}
	}
	public void ayudar(){
		Application.ExternalCall("parent.$juego.game.unity.ayudar");
	}
}
