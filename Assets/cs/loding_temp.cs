using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loding_temp : MonoBehaviour {
	public Transform loding_bar;
	public Transform text_duration;
	public Transform text_limint;
	public int limite=120;
	int minutos=0;
	float segundos=0;
	GameObject camara;
	public bool fin = false;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;


	void Awake(){
		camara = GameObject.Find ("Main Camera");

	}
	// Use this for initialization
	void Start () {
		int min = ((int)(limite/60));
		int second = ((int) (limite % 60));
		string minu    = (min    < 10) ? "0"+min.ToString() : min.ToString();
		string seconds = (second < 10) ? "0"+second.ToString() : second.ToString();
		text_limint.GetComponent<Text> ().text = minu + ":" + seconds;
		this.contar (minutos, (int)segundos);
	}
	// Update is called once per frame
	void Update () {
		if (currentAmount < limite) {
			currentAmount += speed * Time.deltaTime;
			segundos += speed * Time.deltaTime; 
			int auxSegundos = ((int)(segundos));
			this.contar (minutos,auxSegundos);
			if (auxSegundos == 60) {
				segundos = 0;
				minutos++;
			}
			text_limint.gameObject.SetActive (true);
		} else {
			text_duration.GetComponent<Text> ().text = "Fin";
			text_limint.gameObject.SetActive (false);
			if (!fin) {
				fin = true;
				camara.GetComponent<juego> ().finalizar ();
			}


		}
		loding_bar.GetComponent<Image> ().fillAmount = currentAmount /limite;
	
	}
	public void reiniciar(){
		currentAmount = 0;
		segundos 	  = 0;
		minutos = 0;
		this.contar (minutos, (int)segundos);
	}
	void contar(int minutero,int segundero){
		if (minutos > 9 && segundos > 9) {
			text_duration.GetComponent<Text> ().text = minutero.ToString()+":"+segundero.ToString();
		}else if(minutos>9 && segundos<10){
			text_duration.GetComponent<Text> ().text = minutero.ToString() + ":0" + segundero.ToString();
		}else if(minutos<10 && segundos>9){
			text_duration.GetComponent<Text> ().text = "0" + minutero.ToString() + ":" + segundero.ToString();
		}else{
			text_duration.GetComponent<Text> ().text = "0" + minutero.ToString() + ":0" + segundero.ToString();
		}

	}
}
