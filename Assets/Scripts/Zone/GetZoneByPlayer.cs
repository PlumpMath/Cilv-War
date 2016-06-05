using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetZoneByPlayer : MonoBehaviour {
	const int maxEnemy = 1;
	public GameObject m_Player;
	public ParticleSystem m_Particle;

	private GameController c_GameController;
	private EnemyManager c_EnemyManager;

	void Start() {
		c_GameController = FindObjectOfType < GameController > ();
		c_EnemyManager = FindObjectOfType < EnemyManager > ();
		m_Particle.Play();
	}

	void Update() {
		if (c_GameController.getScoreInZone() >= maxEnemy)
			if (Vector3.Distance(transform.position, m_Player.transform.position) < 5) {
				m_Particle.startColor = Color.blue;
				c_EnemyManager.init(c_EnemyManager.getEmemyGlobal());
				c_GameController.setScoreInZone(0);
				c_GameController.countStar++;

				PlayerPrefs.SetInt("StarZone", c_GameController.countStar);

				c_GameController.setOccupy (true);
			}

		if (GameController.m_IsPause) {
			c_GameController.countStar = 0;
		}
	}
}
