using UnityEngine;

public class RotateY : MonoBehaviour {
	public float m_Speed = 6;

	void Update () {
		this.transform.Rotate (0, this.transform.position.y * m_Speed, 0);
	}
}
