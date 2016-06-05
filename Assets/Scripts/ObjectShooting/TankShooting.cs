using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour {
	public Rigidbody m_Shell;
	public Transform m_FireTransformPrefab;

	public float m_RangeFire = 30f;

	protected void Fire() {
		if (!GameController.m_IsPause) {
			Rigidbody shellInstance =
				Instantiate(m_Shell, m_FireTransformPrefab.position, m_FireTransformPrefab.rotation)as Rigidbody;

			// Set the shell's velocity to the launch force in the fire position's forward direction.
			shellInstance.velocity = m_RangeFire * m_FireTransformPrefab.forward;

		}
	}
}
