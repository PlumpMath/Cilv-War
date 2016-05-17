using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private bool isGoUp, isGoBack, isTurnLeft, isTurnRight;
	private float degree;
	private AudioSource audioMovement;
	public GameObject targetGoUp, targetGoBack;
	public AudioClip audioMoving;
	private AudioClip audioStanding;

	void Start() { 
		audioMovement = GetComponent<AudioSource> ();
		audioStanding = audioMovement.clip;
		audioMovement.Play ();
		StandMovement ();
	}

	void FixedUpdate() {
		if (isGoUp) transform.position = Vector3.Lerp (transform.position, targetGoUp.transform.position, Time.deltaTime);
		if (isGoBack) transform.position = Vector3.Lerp (transform.position, targetGoBack.transform.position, Time.deltaTime);
		if (isTurnRight) {
			degree += 1.3f;
			Quaternion tempRotation = Quaternion.AngleAxis (degree, Vector3.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, tempRotation, .05f);
		}
		if (isTurnLeft) {
			degree -= 1.3f;
			Quaternion tempRotation = Quaternion.AngleAxis (degree, Vector3.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, tempRotation, .05f);
		}
	}

	public void GoUp () { isGoUp = true; AudioMoving (); }
	public void GoBack () { isGoBack = true; AudioMoving (); }
	public void TurnLeft () { isTurnLeft = true; }
	public void TurnRight () { isTurnRight = true; }
	public void StandMovement () { isGoUp = isGoBack = isTurnLeft = isTurnRight = false; 
		if (audioMovement.clip == audioMoving)
			audioMovement.clip = audioStanding;
		audioMovement.Play ();
	}
	private void AudioMoving () {
		if (audioMovement.clip == audioStanding)
			audioMovement.clip = audioMoving;
		audioMovement.Play ();
	}
}
