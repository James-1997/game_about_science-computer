using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killParticulaPena : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating("killParticula", 1, 3.0f);
	}
	
	// Update is called once per frame
	void killParticula () {
		Destroy(this.gameObject);
	}
}
