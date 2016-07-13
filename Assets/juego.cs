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
	
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			setCorrecto ();
		} else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			setError (50);
		}
	}
	public void finalizar(){
		txt_fin.GetComponent<Text> ().text = ((eficiencia * puntajeActual) / 100) +" pts";
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
		menu_fin.gameObject.SetActive (true);
		Application.ExternalCall ("$juego.game.finish");


		
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
		score.GetComponent<Text> ().text = "0 pts";
		Application.ExternalCall ("$juego.game.restart");
	}
	public void salir(){
		Application.ExternalCall ("$juego.game.unity.salir");
	}
	void setCombo(){

	}
	void setCorrecto(){
		continuo++;
		aciertos++;
		intentos++;
		eficiencia = (int)(aciertos * 100) / intentos;
		puntajeActual += valorPuntos;
		score.GetComponent<Text>().text = ((eficiencia * puntajeActual) / 100)+" pts";
		determinarCombo();
		Application.ExternalCall ("$juego.game.setCorrecto");
	}
	void setError(int puntosMenos){
		continuo = 0;
		intentos++;
		if(puntajeActual>puntosMenos){
			score.GetComponent<Text>().text = (puntajeActual-=puntosMenos)+" pts";
		}
		valorPuntos = 100;
		Application.ExternalCall ("$juego.game.setError",50);

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
		Application.ExternalCall("$juego.game.unity.ayudar");
	}
}
