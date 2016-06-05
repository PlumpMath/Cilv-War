using UnityEngine;

	public class ObjectMovement : MonoBehaviour {
		public float m_Speed = 7f;
		public float m_PitchRange = 0.2f;
		protected Rigidbody m_Rigidbody;

		private void Awake() {
			m_Rigidbody = GetComponent < Rigidbody > ();
		}

		private void OnEnable() {
			// When the tank is turned on, make sure it's not kinematic.
			if (m_Rigidbody != null)
				m_Rigidbody.isKinematic = false;
		}

		private void OnDisable() {
			// When the tank is turned off, set it to kinematic so it stops moving.
			if (m_Rigidbody != null)
				m_Rigidbody.isKinematic = true;
		}
	}
