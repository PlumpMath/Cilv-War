using UnityEngine;
using System;

public class ParticleClonePlay : MonoBehaviour {
	public ParticleSystem m_Particle;

	void Start () {
		ParticleSystem particle = GetComponent<ParticleSystem> ();
		particle.Play ();
	}


}
