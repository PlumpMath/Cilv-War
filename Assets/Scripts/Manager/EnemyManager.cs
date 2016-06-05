using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public float m_MaxInZone = 44f;
	public string m_EnemyZone;

	private bool _isMove = false;
	private string _enemyGlobal = "EnemyGlobal";
	private PlayerMovement _playerMovement;
	private GameController _gameController;

	void Start() {
		_playerMovement = FindObjectOfType < PlayerMovement > ();
		_gameController = FindObjectOfType<GameController> ();
	}

	void Update() {
		if (isGlobal() && !_isMove) {
			init(_enemyGlobal);
			_isMove = true;
			_gameController.setScoreInZone (0);
			_playerMovement.m_EPlayerZone = "";
			_gameController.setOccupy (false);
		}
		if (Vector3.Distance(transform.position, _playerMovement.transform.position) < m_MaxInZone && _isMove) {
			init(m_EnemyZone);
			_isMove = false;
			_playerMovement.m_EPlayerZone = m_EnemyZone;
		}
	}

	public void init(string tag) {
		GameObject[]_enemy = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[]_point = GameObject.FindGameObjectsWithTag(tag);
		for (int i = 0; i < _point.Length; i++) {
			Vector3 _temp = _point[i].transform.position;
			_enemy[i].transform.position = _temp;
		}
	}

	private bool isGlobal() {
		GameObject[]objs = GameObject.FindGameObjectsWithTag(m_EnemyZone);
		for (int i = 0; i < objs.Length; i++)
			if (Vector3.Distance(objs[i].transform.position, _playerMovement.transform.position) < m_MaxInZone) {
				return false;
			}
		return true;
	}

	public string getEmemyGlobal() {
		return _enemyGlobal;
	}
}
