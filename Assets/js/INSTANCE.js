#pragma strict

// VARIABLES GLOBALES DEL JUEGO
    public var preBaraja:GameObject;//Game object para guardar el prefab del diseño de la baraja.
	public var preOpcion:GameObject;//Game object para guardar el prefab del diseño de las opciones de respuestas laterales.
	public var preCorrecto:GameObject;//Game object para guardar el prefab del diseño de la palomita que indica cuando la respuesta es correct.
	public var preIncorrecto:GameObject; //lo mismos que la de arriba pero para indicar cuando es incorrecta.
	public var opciones_hijos:GameObject[]; //Game objects para guardar las opciones del diseño de las figuras ya formadas.
	public var figuras_baraja:GameObject[];//Game objects para guardar el diseño de las operaciones de las figuras.
	private var etiquetas =["Rectangulo","Diamante","Cuadrilatero"];//Etiquetas que serán puestas a los Games objects que nos servirán para referenciar a la respuesta corrrecta.
	private var index:int;
	private var x:int;
	private var y:int;
	private var z:int;
	private var clip:AnimationClip;
	private var animacion:Animation;
function Start () {
		Application.ExternalCall("$juego.game.start",60,false); 
		var baraja = Instantiate (preBaraja,new Vector3 (0.22f,0.45f, 0f),Quaternion.identity) as GameObject;
		baraja.name = "baraja";
		var n = Random.Range (0,3);
		baraja.tag =etiquetas[n];
		var figura_hijo = Instantiate (figuras_baraja [n]) as GameObject;
		figura_hijo.transform.parent = baraja.transform;
		figura_hijo.transform.position = baraja.transform.position;
		figura_hijo.GetComponent(SpriteRenderer).sortingOrder=2;
		for (var i:int = 0; i < 3; i++) {
			var x:float = (-5.23) + (i*5.24);
			var opcion:GameObject;
			if (i == 1) {
				 opcion = Instantiate (preOpcion, new Vector3 (x, -3.8, 0), Quaternion.identity) as GameObject;
			} else {
				 opcion = Instantiate (preOpcion,new Vector3 (x,0,0),Quaternion.identity) as GameObject;
			}
			opcion.name = "opcion("+i+")";
			var opcion_hijo:GameObject = Instantiate (opciones_hijos[i]);
			opcion_hijo.transform.parent = opcion.transform;// le pasamos la herencia.
			opcion_hijo.transform.position = opcion.transform.position;// Le pasamos la posicion de su padre.
			opcion_hijo.name = "opcion_figura("+i+")";
			opcion.tag = etiquetas [i];
			opcion_hijo.GetComponent(SpriteRenderer).sortingOrder=1;
		}
	
	}

	// Update is called once per frame
	function Update () {

		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			index=2;
			Check (index);
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			index = 0;
			Check (index);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			index = 1;
			Check (index);
		}

	}
	function Check(index:int){
	    clip =  AnimationClip();
		var baraja_find = GameObject.Find ("baraja");
		if (baraja_find.tag == GameObject.FindGameObjectWithTag (etiquetas [index]).tag) {
			Application.ExternalCall ("$juego.game.setCorrecto");
			Destroy (baraja_find);
			var baraja = Instantiate (preBaraja, new Vector3 (0.22f, 0.45f, 0f), Quaternion.identity);
			baraja.name = "baraja";
			var n = Random.Range (0, 3);
			baraja.tag = etiquetas [n];
			var figura_hijo = Instantiate (figuras_baraja [n]) as GameObject;
			figura_hijo.transform.parent = baraja.transform;
			figura_hijo.transform.position = baraja.transform.position;
			figura_hijo.GetComponent(SpriteRenderer).sortingOrder = 2;
			var correcto = Instantiate (preCorrecto,  Vector3 (0f, 0f, 0f), transform.rotation);
			correcto.transform.localScale =  Vector3 (0.2f, 0.2f, 0f);
			correcto.GetComponent(SpriteRenderer).sortingOrder = 3;
			correcto.AddComponent(Animation);
			x = correcto.transform.position.x;
			y = correcto.transform.position.y;
			z = correcto.transform.position.z;
			clip.SetCurve ("",typeof(Transform),"localPosition.y", AnimationCurve( Keyframe(0,y), Keyframe(0.7,y+0.6), Keyframe(.4,y), Keyframe(.5,y-0.6),new Keyframe(.8,y)));
			clip.SetCurve ("",typeof(Transform),"localPosition.x",AnimationCurve ( Keyframe (0,-1), Keyframe(0.7,x+0.6), Keyframe(.4,x+1.9), Keyframe(.5,x+0.6),new Keyframe(.8,x)));
			clip.SetCurve ("",typeof(Transform),"localScale.y", AnimationCurve( Keyframe(0,0.2), Keyframe(0.7,0.3), Keyframe(.5,0.4),new Keyframe(.6,0.3),new Keyframe(.4,0.2)));
			clip.SetCurve ("",typeof(Transform),"localScale.x",AnimationCurve (Keyframe (0, 0.2), Keyframe(0.7,0.3), Keyframe(.5,.4), Keyframe(.6,0.3),new Keyframe(.4,0.2)));
		    animacion = correcto.GetComponent (Animation) as Animation;
			clip.legacy = true;
			animacion.AddClip (clip,"clip");
			animacion.Play ("clip");
			Destroy (correcto, 1.5f);

		} else {
			Application.ExternalCall ("$juego.game.setError",50);
			var incorrecto = Instantiate (preIncorrecto,  Vector3 (0f, 0f, 0f), transform.rotation);
			incorrecto.transform.localScale =  Vector3 (0.2f, 0.2f, 0f);
			incorrecto.GetComponent (SpriteRenderer).sortingOrder = 3;
		    incorrecto.AddComponent(Animation);
			x = incorrecto.transform.position.x;
			y = incorrecto.transform.position.y;
			z = incorrecto.transform.position.z;
			clip.SetCurve ("",typeof(Transform),"localScale.y", AnimationCurve(Keyframe(0,0.2f), Keyframe(0.1f,0.4f), Keyframe(.1f,0.3f), Keyframe(.2f,0.4f), Keyframe(.3f,0.2f)));
			clip.SetCurve ("", typeof(Transform), "localScale.x",  AnimationCurve (Keyframe (0, 0.2f), Keyframe(0.1f,0.3f), Keyframe(.1f,.4f), Keyframe(.2f,0.3f), Keyframe(.3f,0.2f)));
		    animacion = incorrecto.GetComponent (Animation) as Animation;
			clip.legacy = true;
			animacion.AddClip (clip,"clip");
			animacion.Play ("clip");
			Destroy (incorrecto, 1f);
		}
	}