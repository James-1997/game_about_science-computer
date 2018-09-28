using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimetaCamera : MonoBehaviour {

	Vector3 offSet;
	public GameObject jogador;
	
	void Start ()
	{
		offSet = transform.position - jogador.transform.position;
	}
	
	void Update ()
	{
		transform.position = jogador.transform.position + offSet;
	}
}
