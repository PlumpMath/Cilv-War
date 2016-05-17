using UnityEngine;
using System.Collections;

public class Gamemanager : MonoBehaviour {
	public Object canvasControl;
	// Use this for initialization
	void Start () {
		Instantiate (canvasControl);
	}

}
