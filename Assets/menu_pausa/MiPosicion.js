class MiPosicion extends MonoBehaviour
{
	static var Usar : MiPosicion;
	function Awake()
	{
		Usar = this;
	}

	var ObjetoGuardar : GameObject;
	var ObjetoCargar : GameObject;
	var CargarAlInicio : boolean = true;
	var Botones : MisBotones;
	var Teclado : MiTeclado;

	private var A1 : int;
	private var B1 : int;
	private var A2 : int;
	private var B2 : int;
	private var A3 : int;
	private var B3 : int;

	class MisBotones
	{
		var MostrarBotones : boolean = true;
		var Posicion : P = P.ArribaDerecha;
		enum P{ArribaIzquierda, ArribaDerecha, AbajoIzquierda, AbajoDerecha}
		var Anchura : int = 100;
		var Altura : int = 50;
		var Distancia : int = 10;
		var TextoGuardar : String = "Guardar";
		var TextoCargar : String = "Cargar";
		var TextoBorrar : String = "Borrar";
	}
	class MiTeclado
	{
		var UsarTeclado : boolean;
		var TeclaGuardar : KeyCode = KeyCode.G;
		var TeclaCargar : KeyCode = KeyCode.C;
		var TeclaBorrar : KeyCode = KeyCode.B;
	}

	function Start()
	{
		if(CargarAlInicio)
		{
			Cargar();
		}
		if(Teclado.UsarTeclado)
		{
			Debug.Log("Pulsa la tecla: " + Teclado.TeclaGuardar + " para guardar tu posicion");
			Debug.Log("Pulsa la tecla: " + Teclado.TeclaCargar + " para cargar tu posicion");
			Debug.Log("Pulsa la tecla: " + Teclado.TeclaBorrar + " para borrar tu posicion");
		}
	}

	function Update()
	{
		if(Botones.MostrarBotones)
		{
			if(Botones.Posicion == Botones.P.ArribaIzquierda)
			{
				A1 = Botones.Distancia;
				B1 = Botones.Distancia;
				A2 = Botones.Distancia;
				B2 = Botones.Distancia*2+Botones.Altura;
				A3 = Botones.Distancia;
				B3 = Botones.Distancia*3+Botones.Altura*2;
			}
			else if(Botones.Posicion == Botones.P.ArribaDerecha)
			{
				A1 = Screen.width-Botones.Anchura-Botones.Distancia;
				B1 = Botones.Distancia;
				A2 = Screen.width-Botones.Anchura-Botones.Distancia;
				B2 = Botones.Distancia*2+Botones.Altura;
				A3 = Screen.width-Botones.Anchura-Botones.Distancia;
				B3 = Botones.Distancia*3+Botones.Altura*2;
			}
			else if(Botones.Posicion == Botones.P.AbajoIzquierda)
			{
				A1 = Botones.Distancia;
				B1 = Screen.height-Botones.Altura-Botones.Distancia;
				A2 = Botones.Distancia;
				B2 = Screen.height-Botones.Altura*2-Botones.Distancia*2;
				A3 = Botones.Distancia;
				B3 = Screen.height-Botones.Altura*3-Botones.Distancia*3;
			}
			else
			{
				A1 = Screen.width-Botones.Anchura-Botones.Distancia;
				B1 = Screen.height-Botones.Altura-Botones.Distancia;
				A2 = Screen.width-Botones.Anchura-Botones.Distancia;
				B2 = Screen.height-Botones.Altura*2-Botones.Distancia*2;
				A3 = Screen.width-Botones.Anchura-Botones.Distancia;
				B3 = Screen.height-Botones.Altura*3-Botones.Distancia*3;
			}
		}
		if(Teclado.UsarTeclado)
		{
			if(Input.GetKeyDown(Teclado.TeclaGuardar))
			{
				Guardar();
			}	
			else if(Input.GetKeyDown(Teclado.TeclaCargar))
			{
				Cargar();
			}	
			else if(Input.GetKeyDown(Teclado.TeclaBorrar))
			{
				Borrar();
			}
		}
	}

	function OnGUI()
	{
		if(Botones.MostrarBotones)
		{
			if(GUI.Button(Rect(A1, B1, Botones.Anchura, Botones.Altura), Botones.TextoGuardar))
			{
				Guardar();
			}
			
			if(PlayerPrefs.HasKey("X")	&&	PlayerPrefs.HasKey("Y")	&&	PlayerPrefs.HasKey("Z"))
			{
				if(GUI.Button(Rect(A2, B2, Botones.Anchura, Botones.Altura), Botones.TextoCargar))
				{
					Cargar();
				}
				if(GUI.Button(Rect(A3, B3, Botones.Anchura, Botones.Altura), Botones.TextoBorrar))
				{
					Borrar();
				}
			}
		}
	}



	function Guardar()
	{
		PlayerPrefs.SetFloat("X", ObjetoGuardar.transform.position.x);
		PlayerPrefs.SetFloat("Y", ObjetoGuardar.transform.position.y);
		PlayerPrefs.SetFloat("Z", ObjetoGuardar.transform.position.z);
		
		Debug.Log("Posicion guardada");
	}
	function Cargar()
	{
		if(PlayerPrefs.HasKey("X")	&&	PlayerPrefs.HasKey("Y")	&&	PlayerPrefs.HasKey("Z"))
		{
			var NuevaPosicion : Vector3;
			NuevaPosicion.x = PlayerPrefs.GetFloat("X");
			NuevaPosicion.y = PlayerPrefs.GetFloat("Y");
			NuevaPosicion.z = PlayerPrefs.GetFloat("Z");
			ObjetoCargar.transform.position = NuevaPosicion;
			
			Debug.Log("Posicion cargada");
		}
		else
		{
			Debug.Log("No puedo Cargar mi posicion por que falta Guardar");
		}
	}
	function Borrar()
	{
		if(PlayerPrefs.HasKey("X")	&&	PlayerPrefs.HasKey("Y")	&&	PlayerPrefs.HasKey("Z"))
		{
			PlayerPrefs.DeleteKey("X");
			PlayerPrefs.DeleteKey("Y");
			PlayerPrefs.DeleteKey("Z");
			
			Debug.Log("Posicion borrada");
		}
		else
		{
			Debug.Log("No puedo Borrar mi posicion por que falta Guardar");
		}
	}
}
