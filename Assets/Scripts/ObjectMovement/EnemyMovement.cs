using UnityEngine;

public class EnemyMovement : ObjectMovement {
	float timeUpdate = 0;

	private float timeDelay;
	public float distanceLookAt = 20f;
	[HideInInspector] public float currentDistance;

	public PlayerMovement player;

	void Start() {
		player = FindObjectOfType(typeof(PlayerMovement))as PlayerMovement;
		timeDelay = Random.Range (6, 9);
		Transform _player = player.transform;
		currentDistance = Vector3.Distance(transform.position, _player.position);
	}

	public bool isInRange () {
		Transform _player = player.transform;
		currentDistance = Vector3.Distance(transform.position, _player.position);
		if (currentDistance < distanceLookAt)
			return true;
		else
			return false;
	}

	void Update() {
		if (!GameController.isPause) {
			Transform _player = player.transform;
			timeUpdate += Time.deltaTime;
			if (timeUpdate > timeDelay) {
				timeDelay = Random.Range (6, 9);
				timeUpdate = 0;
			}
			if (isInRange ())
				this.transform.LookAt (_player);
			else {
				enemyTurn ();
			}
			enemyMove ();
		}
	}

	private void enemyTurn() {
		float turn = 0;
		if (timeUpdate > 0.55f && timeUpdate < 0.95f) {
			turn = (Random.Range(2, 5));
		} else if (timeUpdate > 6 && timeUpdate < 7.5f) {
			turn = (Random.Range(1, 3));
		} else
			turn = 0;
	
		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		// Apply this rotation to the rigidbody's rotation.
		m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

	}

	private void enemyMove() {
		if (timeUpdate > 1.9f && timeUpdate < timeDelay / 2) {
			Vector3 _movement = transform.forward * (Random.value + 1f) / 6;
			m_Rigidbody.MovePosition(m_Rigidbody.position + _movement);
		}
	}
}
