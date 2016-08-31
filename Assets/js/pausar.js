#pragma strict
var ObjPausa : GameObject;

function start(){
	Debug.Log("Me haz tocado");
}

function OnMouseDown(){
	print("Me haz tocado.");
	Debug.Log("Me haz tocado");
	//ObjPausa.SetActive(true);
	//Time.timeScale = 0;

}
