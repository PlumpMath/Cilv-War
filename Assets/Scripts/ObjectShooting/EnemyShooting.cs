using UnityEngine;

public class EnemyShooting : TankShooting {
	private EnemyMovement EMovement;
	private float timeOut = 0;

	private void Start() {
		EMovement = this.GetComponent<EnemyMovement>();
	}

	void Update() {
		if (!GameController.m_IsPause) {
			timeOut += Time.deltaTime;

			if (EMovement.isInRange () && timeOut > 1.7f) {
				Fire ();
				timeOut = 0;
			}
		}
	}
}
