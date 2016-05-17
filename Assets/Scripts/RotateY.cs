using UnityEngine;

public class RotateY : MonoBehaviour {
	public float speed = 6;
	void Update () {
		this.transform.Rotate (0, this.transform.position.y * speed, 0);
	}
}
