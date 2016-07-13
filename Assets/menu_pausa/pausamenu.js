var ObjPausa : GameObject;

function Start()
{
	ObjPausa.SetActive(false);
}

function Update()
{
	if(Input.GetKeyDown(KeyCode.P))
	{
		Cambio();
	}
}

function Cambio()
{
	if(Time.timeScale == 1)
		Pausear();
	
	else if(Time.timeScale == 0)
		Continuar();
}

function Pausear()
{
	ObjPausa.SetActive(true);
	Time.timeScale = 0;
}

public function Continuar()
{
	ObjPausa.SetActive(false);
	Time.timeScale = 1;
}

function Menu(i : String)
{
	Debug.Log("hola presione el boton de menu principal...");
}