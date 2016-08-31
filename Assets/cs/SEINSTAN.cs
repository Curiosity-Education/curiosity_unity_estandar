using UnityEngine;
using System.Collections;
using UnityEngine.Sprites;
public class SEINSTAN : MonoBehaviour {

	// VARIABLES GLOBALES DEL JUEGO
	public GameObject preBaraja;//Game object para guardar el prefab del diseño de la baraja.
	public GameObject preOpcion;//Game object para guardar el prefab del diseño de las opciones de respuestas laterales.
	public GameObject preCorrecto;//Game object para guardar el prefab del diseño de la palomita que indica cuando la respuesta es correct.
	public GameObject preIncorrecto; //lo mismos que la de arriba pero para indicar cuando es incorrecta.
	public GameObject[] opciones_hijos = new GameObject[3]; //Game objects para guardar las opciones del diseño de las figuras ya formadas.
	public GameObject[] figuras_baraja= new GameObject[3];//Game objects para guardar el diseño de las operaciones de las figuras.
	private string[] etiquetas ={"Rectangulo","Diamante","Cuadrilatero"};//Etiquetas que serán puestas a los Games objects que nos servirán para referenciar a la respuesta corrrecta.
	private string[] etiquetas_flechas={"Left","Right","Bottom"};
	private int index;
	private float  x;
	private float y;
	private float z;
	public RuntimeAnimatorController incorrecto;
	public RuntimeAnimatorController desaparecer;
	private Animation animacion;
	GameObject flecha_bottom;
	//lista mi_lista = new lista ();
	int control_nivel=0;
	int[] numeros_aleatorios_nivel_3 = new int[3];
	int[] numeros_aleatorios_nivel_2 = new int[2];
	int nivel = 0;
	GameObject camara;
	void fill_array(int[] numeros){
		int i = 0;
		int j =0;
		bool repeat = false;
		while(i<numeros.Length){
			numeros [i] = Random.Range (0,numeros.Length);
			j = 0;
			while(j<i){
				if (numeros [j] == numeros [i]) {
					repeat = true;
					j = i;
				} else {
					j++;
					if(j==i){
						repeat = false;
					}
				}
			}
			if (!repeat) {
				i++;
			}
		}
	}
	void Awake(){
		camara = GameObject.Find ("Main Camera");
		flecha_bottom = GameObject.Find ("flecha_bottom");
	}
	public void reiniciar(){
		nivel = 0;
		control_nivel = 0;
		Destroy(GameObject.Find ("opcion(0)"));
		Destroy (GameObject.Find ("opcion(1)"));
		Destroy (GameObject.Find ("baraja"));
		GameObject opcion3 = GameObject.Find ("opcion(2)");
		if (opcion3) {
			Destroy (opcion3);
		}
		this.Start ();
	}
	// Use this for initialization
	void crearElementos(){
		fill_array (numeros_aleatorios_nivel_3);
		Destroy(GameObject.Find ("opcion(0)"));
		Destroy (GameObject.Find ("opcion(1)"));
		GameObject opcion3 = GameObject.Find ("opcion(2)");
		if (opcion3) {
			Destroy (opcion3);
		}
		for (int i=0; i < numeros_aleatorios_nivel_3.Length; i++) {
			float x = (-5.23f) + (i*10.24f);
			GameObject opcion;
			if (i == 2) {
				opcion = Instantiate (preOpcion,new Vector3 (0.1f,-3.8f,0),Quaternion.identity) as GameObject;
			} else {
				opcion = Instantiate (preOpcion,new Vector3 (x,0,0),Quaternion.identity) as GameObject;
			}
			opcion.name = "opcion("+i+")";
			GameObject opcion_hijo = Instantiate (opciones_hijos[numeros_aleatorios_nivel_3[i]]);
			opcion_hijo.transform.parent = opcion.transform;// le pasamos la herencia.
			opcion_hijo.transform.position = opcion.transform.position;// Le pasamos la posicion de su padre.
			opcion_hijo.name = "opcion_figura("+i+")";
			opcion_hijo.AddComponent<BoxCollider> ();
			opcion_hijo.AddComponent<flecha> ();
			opcion_hijo.tag = etiquetas_flechas [i];
			opcion.tag = etiquetas [numeros_aleatorios_nivel_3[i]];
			opcion_hijo.GetComponent<SpriteRenderer>().sortingOrder=1;
		}
	}

	void Start () {
		fill_array (numeros_aleatorios_nivel_2);//lenar arreglo de numeros aleatoreos sin que se repitan.
		GameObject baraja = Instantiate (preBaraja,new Vector3 (0f,1f, 0f),Quaternion.identity) as GameObject;
		baraja.name = "baraja";
		baraja.GetComponent<SpriteRenderer>().sortingOrder=1;
		int n = Random.Range(0,2);
		baraja.tag =etiquetas[numeros_aleatorios_nivel_2[n]];
		GameObject figura_hijo = Instantiate (figuras_baraja[numeros_aleatorios_nivel_2[n]]) as GameObject;
		figura_hijo.transform.parent = baraja.transform;
		figura_hijo.transform.position = baraja.transform.position;
		figura_hijo.GetComponent<SpriteRenderer>().sortingOrder=2;
		flecha_bottom.gameObject.SetActive (false);
		for (int i=0; i < numeros_aleatorios_nivel_2.Length; i++) {
			float x = (-5.23f) + (i*10.24f);
			GameObject opcion;
			opcion = Instantiate (preOpcion,new Vector3 (x,0,0),Quaternion.identity) as GameObject;
			opcion.name = "opcion("+i+")";
			GameObject opcion_hijo = Instantiate (opciones_hijos[numeros_aleatorios_nivel_2[i]]);
			opcion_hijo.transform.parent = opcion.transform;// le pasamos la herencia.
			opcion_hijo.transform.position = opcion.transform.position;// Le pasamos la posicion de su padre.
			opcion_hijo.name = "opcion_figura("+i+")";
			opcion_hijo.AddComponent<BoxCollider> ();
			opcion_hijo.AddComponent<flecha> ();
			opcion_hijo.tag = etiquetas_flechas [i];
			opcion.tag = etiquetas [numeros_aleatorios_nivel_2[i]];
			opcion_hijo.GetComponent<SpriteRenderer>().sortingOrder=2;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			index=1;
			Check (index);
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			index = 0;
			Check (index);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			if (nivel > 0) {// si ya se ha superado el nivel 1 significa que el usuario tendra habilitada la tercera opción para poder seleccionarla-
				index = 2;
				Check (index);
			}
		}
	
	}
	void correcto(GameObject baraja_find){
		camara.GetComponent<juego>().setCorrecto ();
		Destroy (baraja_find);
		GameObject baraja = Instantiate (preBaraja, new Vector3 (0f, 1f, -3f), Quaternion.identity) as GameObject;
		//SpriteRenderer color = baraja_find.GetComponentInChildren<SpriteRenderer>();
		//color.color.a = 0f;
		//animacion_baraja.Play ("animacionBaraja");
		//Destroy (baraja_find.GetComponent<Animation> (),1f);
		baraja_find.AddComponent<Animator>();
		Animator animacion = baraja_find.GetComponent<Animator> ();
		animacion.runtimeAnimatorController = desaparecer;
		animacion.enabled = true;
		//mi_lista.insertInit (baraja_find);
		//StartCoroutine ("elimiminar_objetos",1f);
		baraja.name = "baraja";
		baraja.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		int n;
		GameObject figura_hijo;
		control_nivel++;
		if (control_nivel == 6) {
			this.crearElementos ();
			flecha_bottom.SetActive (true);
			nivel = 1;
		} else if (control_nivel > 15) {
			this.crearElementos ();
			nivel = 2;
		}
		if (nivel == 0) {
			n = Random.Range (0, 2);
			baraja.tag = etiquetas [numeros_aleatorios_nivel_2 [n]];
			figura_hijo = Instantiate (figuras_baraja [numeros_aleatorios_nivel_2 [n]]) as GameObject;
		} else {
			n = Random.Range (0, 3);
			baraja.tag = etiquetas [numeros_aleatorios_nivel_3 [n]];
			figura_hijo = Instantiate (figuras_baraja [numeros_aleatorios_nivel_3 [n]]) as GameObject;
		} 
		figura_hijo.transform.parent = baraja.transform;
		figura_hijo.transform.position = baraja.transform.position;
		figura_hijo.GetComponent<SpriteRenderer>().sortingOrder = 2;
		preCorrecto.gameObject.SetActive (true);
		StartCoroutine("desactiv",preCorrecto);
	}
	public void Check(int index){
		GameObject baraja_find = GameObject.Find ("baraja");
		int[] arreglo;
		if (nivel == 0) {
			arreglo = numeros_aleatorios_nivel_2;
		} else {
			arreglo = numeros_aleatorios_nivel_3;
		}
		if (baraja_find.tag == GameObject.FindGameObjectWithTag (etiquetas [arreglo[index]]).tag) {
			this.correcto (baraja_find);
		} else {
			camara.GetComponent<juego>().setError (50);
			Animator animacion;
		    animacion = baraja_find.GetComponent<Animator> ();
			if (!animacion) {
				baraja_find.AddComponent<Animator> ();
				animacion = baraja_find.GetComponent<Animator> ();
				animacion.runtimeAnimatorController = incorrecto;
			} else {
				animacion.enabled = true;
			}

			StartCoroutine ("pausarAnimacion", animacion);
			//	GameObject.Find("Main Camera").GetComponent("juego").setError(50);
		}
	}
	IEnumerator pausarAnimacion(Animator ani){
		yield return new WaitForSeconds (.5f);
		if (ani) {
			ani.enabled = false;
		}
	}
	IEnumerator elimiminar_objetos(float tiempo){
		yield return new WaitForSeconds (tiempo);
		//mi_lista.vaciarLista ();
	}
	IEnumerator desactiv(GameObject objeto){
		yield return new WaitForSeconds (1f);
		objeto.gameObject.SetActive (false);
	}
}
/*
public class Nodo{
	private GameObject objeto;
	private Nodo siguiente;
	public Nodo(GameObject objeto){
		this.objeto= objeto;

	}
	public Nodo (GameObject objeto,Nodo siguiente){
		this.objeto = objeto;
		this.siguiente = siguiente;
	}
	public void setSiguiente(Nodo siguiente){
		this.siguiente = siguiente;
	}
	public GameObject getObject(){
		return this.objeto;
	}
	public Nodo getSiguiente(){
		return this.siguiente;
	}
}
public class lista : MonoBehaviour{
	public Nodo inicio;
	protected int longitud;
	public lista(){
		this.inicio = null;
		this.longitud = 0;
	}
	public void insertInit(GameObject objeto){
		Nodo nuevo = new Nodo (objeto);
		nuevo.setSiguiente (this.inicio);
		this.inicio = nuevo;
		this.longitud++;
	}
	public void insertEnd(GameObject objeto){
		Nodo nuevo =  new Nodo(objeto);
		if(this.inicio==null){
			this.inicio = nuevo;
		}else{
			Nodo auxiliar;
			auxiliar = this.inicio;
			while (auxiliar.getSiguiente() != null) {
				auxiliar = auxiliar.getSiguiente();
			}
			auxiliar.setSiguiente (nuevo);
		}
	}
	public void vaciarLista(){
		if (!isEmpty ()) {
			Nodo auxliar = inicio;
			while (auxliar != null) {
				while (auxliar.getSiguiente() != null) {
					auxliar = auxliar.getSiguiente ();
				}
				Destroy (auxliar.getObject());
				auxliar = null;
				auxliar = inicio;
			}
		}
	}
	public bool isEmpty(){
		if (this.inicio == null)
			return true;
		else
			return false;
	}
}
*/