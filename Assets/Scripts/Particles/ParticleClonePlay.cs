using UnityEngine;
using System;

public class ParticleClonePlay : MonoBehaviour {
	public ParticleSystem particle;
	void Start () {
		ParticleSystem particle = GetComponent<ParticleSystem> ();
		particle.Play ();
	}


}
