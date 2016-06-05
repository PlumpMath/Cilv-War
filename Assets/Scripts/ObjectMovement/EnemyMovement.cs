using UnityEngine;

public class EnemyMovement : ObjectMovement {
	private float _timeUpdate = 0;

	private float _timeDelay;
	private PlayerMovement _player;

	public float m_DistanceLookAt = 20f;
	[HideInInspector] public float currentDistance;

	void Start() {
		_player = FindObjectOfType<PlayerMovement>();
		_timeDelay = Random.Range (6, 9);
		Transform transform = _player.transform;
		currentDistance = Vector3.Distance(transform.position, transform.position);
	}

	public bool isInRange () {
		Transform transform = _player.transform;
		currentDistance = Vector3.Distance(transform.position, transform.position);
		if (currentDistance < m_DistanceLookAt)
			return true;
		else
			return false;
	}

	void Update() {
		if (!GameController.m_IsPause) {
			Transform transform = _player.transform;
			_timeUpdate += Time.deltaTime;
			if (_timeUpdate > _timeDelay) {
				_timeDelay = Random.Range (6, 9);
				_timeUpdate = 0;
			}
			if (isInRange ())
				this.transform.LookAt (transform);
			else {
				turnEnemy ();
			}
			moveEnemy ();
		}
	}

	private void turnEnemy() {
		float turn = 0;
		if (_timeUpdate > 0.55f && _timeUpdate < 0.95f) {
			turn = (Random.Range(2, 5));
		} else if (_timeUpdate > 6 && _timeUpdate < 7.5f) {
			turn = (Random.Range(1, 3));
		} else
			turn = 0;
	
		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		// Apply this rotation to the rigidbody's rotation.
		m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

	}

	private void moveEnemy() {
		if (_timeUpdate > 1.9f && _timeUpdate < _timeDelay / 2) {
			Vector3 _movement = transform.forward * (Random.value + 1f) / 6;
			m_Rigidbody.MovePosition(m_Rigidbody.position + _movement);
		}
	}
}
