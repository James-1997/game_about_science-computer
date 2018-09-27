using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoTank : MonoBehaviour {

 /*  CharacterController objetoCharControler;
	float giro = 300.0f;
	Vector3 vetorDirecao = new Vector3(0,0,0);
	float velocidade = 5.0f;
	private float pulo = 5.0f; */

	public GameObject jogador;
	public Animation animacao;	

	CharacterController objetoCharControler;
	float velocidade = 5.0f;
	float giro = 3.0f;
	float frente = 3.0f;
	float pulo = 5.0f; 	
	Vector3 vetorDirecao = new Vector3(0,0,0);
	Vector3 moveCameraFrente; 
	Vector3 moveMove;
	Vector3 normalZeroPiso = new Vector3(0,0,0);
	Transform transformCamera;

	void Start () {
		objetoCharControler = GetComponent<CharacterController>();
		animacao = jogador.GetComponent<Animation>();
		transformCamera = Camera.main.transform;
	}
	
	void Update () {
	/*	Vector3 forward = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * velocidade;
		transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * giro * Time.deltaTime,0));
		objetoCharControler.Move(forward * Time.deltaTime);
		objetoCharControler.SimpleMove(Physics.gravity);

		if (Input.GetButton("Jump")){
			if(objetoCharControler.isGrounded == true) {
				vetorDirecao.y = pulo;
				jogador.GetComponent<Animation>().Play("Pulando");			
			}
		} else {
			if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
				if (!animacao.IsPlaying("Pulando")){
					jogador.GetComponent<Animation>().Play("Andando");
				}
			} else {
				if(objetoCharControler.isGrounded == true) {
					jogador.GetComponent<Animation>().Play("Parado");		
				}
			}
		}

		vetorDirecao.y -= 3.0f * Time.deltaTime;
		objetoCharControler.Move(vetorDirecao * Time.deltaTime); */

		moveCameraFrente = Vector3.Scale(transformCamera.forward, new Vector3(1,0,1)).normalized;
		moveMove = Input.GetAxis("Vertical")*moveCameraFrente + Input.GetAxis ("Horizontal") * transformCamera.right;
		
		vetorDirecao.y -= 3.0f * Time.deltaTime;

		objetoCharControler.Move(vetorDirecao * Time.deltaTime);
		objetoCharControler.Move(moveMove * velocidade * Time.deltaTime);
		
		if (moveMove.magnitude > 1f) moveMove.Normalize();
		moveMove = transform.InverseTransformDirection(moveMove);
		
		moveMove = Vector3.ProjectOnPlane(moveMove, normalZeroPiso);
		giro = Mathf.Atan2(moveMove.x, moveMove.z);
		frente = moveMove.z;
		
		objetoCharControler.SimpleMove(Physics.gravity);
		aplicaRotacao();
		
		if(Input.GetButton("Jump")){
			if (objetoCharControler.isGrounded == true){
				vetorDirecao.y = pulo;
				jogador.GetComponent<Animation>().Play("Pulando");
			}
		}else{
			if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
				if (!animacao.IsPlaying("Pulando")){
					jogador.GetComponent<Animation>().Play("Andando");
				}
			} else {
				if(objetoCharControler.isGrounded == true){
					jogador.GetComponent<Animation>().Play("Parado");
				}
			}
		}
	}

	void aplicaRotacao(){
		float turnSpeed = Mathf.Lerp(180,360,frente);
		transform.Rotate(0, giro * turnSpeed * Time.deltaTime, 0);
	}
}
