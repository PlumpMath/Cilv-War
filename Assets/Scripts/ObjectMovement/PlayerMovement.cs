using UnityEngine;

public class PlayerMovement : ObjectMovement {
	public AudioSource m_MovementAudio; // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
	public AudioClip m_EngineIdling; // Audio to play when the tank isn't moving.
	public AudioClip m_EngineDriving; // Audio to play when the tank is moving.

	public string m_EPlayerZone = "";
	public float m_TurnSpeed = 180f;
	private bool isLeft = false,
	isRight = false,
	isTop = false,
	isBottom = false;


	void FixedUpdate() {
		
		playerTurn();
		EngineAudio();

		playerMovement();
	}
	void Update(){
		banphim ();
	}
	void banphim(){
		if(Input.GetKey(KeyCode.A)){
			isLeft = true;
		}
		else if(Input.GetKey(KeyCode.D)){
			isRight = true;
		}
		else if(Input.GetKey(KeyCode.W)){
			isTop = true;
		}
		else if(Input.GetKey(KeyCode.S)){
			isBottom = true;
		}
		else notAction ();
	}
	public void turnLeft() { isLeft = true;	}

	public void turnRight() { isRight = true;	}

	public void goUp() { isTop = true;	}

	public void goBack() { isBottom = true;  }

	public void notAction() {
		isLeft = false;
		isRight = false;
		isBottom = false;
		isTop = false;
	}

	private void playerMovement() {
		// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
		Vector3 movement = new Vector3(0, 0, 0);
		if (isTop) {
			movement = transform.forward * m_Speed * Time.deltaTime;
		} else if (isBottom) {
			movement = -transform.forward * m_Speed * Time.deltaTime;
		}

		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}

	private void playerTurn() {
		// Determine the number of degrees to be turned based on the input, speed and time between frames.
		float turn = 0f;
		if (isLeft) {
			turn = -m_TurnSpeed * Time.deltaTime;
		} else if (isRight) {
			turn = m_TurnSpeed * Time.deltaTime;
		}

		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

		// Apply this rotation to the rigidbody's rotation.
		m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Powerfull") {
			Destroy(other.gameObject);
			Collider collider = GameObject.Find("Player").GetComponent < Collider > ();
			TankHealth targetHealth = collider.GetComponent < TankHealth > ();
			float _add = -50f;
			targetHealth.TakeDamage(_add, collider);
		}
	}

	public void EngineAudio() {
		if (isLeft || isRight) {
			if (m_MovementAudio.clip == m_EngineDriving) {
				m_MovementAudio.clip = m_EngineIdling;
				m_MovementAudio.Play();
			}
		}
		if (isTop || isBottom) {
			// Otherwise if the tank is moving and if the idling clip is currently playing...
			if (m_MovementAudio.clip == m_EngineIdling) {
				// ... change the clip to driving and play.
				m_MovementAudio.clip = m_EngineDriving;
				//	m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play();
			}
		}
	}
}
