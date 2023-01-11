using UnityEngine;
using System.Collections;

public class particleDeath : MonoBehaviour {

	private ParticleSystem particleSys;
	public float aliveTime = 3f;

	void Start () {
		particleSys = GetComponent<ParticleSystem>();
		//Invoke ("killParticle", aliveTime);
	}

	void killParticle() {
		Destroy(gameObject);
	}

	void Update () {
		if(particleSys){
			if(!particleSys.IsAlive()){
				Destroy(gameObject);
			}
		}
	}
}
