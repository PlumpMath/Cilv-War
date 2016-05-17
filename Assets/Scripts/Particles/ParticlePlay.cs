using UnityEngine;

public class ParticlePlay : MonoBehaviour {

	void Start () {
		ParticleSystem particle = GetComponent<ParticleSystem> ();
		particle.Play ();
	}
}
