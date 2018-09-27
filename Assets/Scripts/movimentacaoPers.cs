using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentacaoPers : MonoBehaviour {
 
	private float velocidade = 1.0f;
	private float giro = 180.0f;
	private float gravidade = 3.5f;
	private float pulo = 6.0f;
	private CharacterController objetoCharControler;
	private Vector3 vetorDirecao = new Vector3(0,0,0); 

	float contaPisca = 0;
	bool podePegarEst;
	int numeroObjts;
 
	public GameObject jogador;
	public Animation animacao;
	public GameObject ParticulaFogo;
	public GameObject ParticulaOvo;
	public GameObject ParticulaPena;
	public GameObject ParticulaEstrela;
 
	void Start () { 
		podePegarEst = false;
	    numeroObjts = 0;
		objetoCharControler = GetComponent<CharacterController>(); 
		animacao = jogador.GetComponent<Animation>();
	}
 	
	void Update (){ 
		if (Input.GetKey(KeyCode.LeftShift)) { velocidade = 2.5f; } else{velocidade = 5;}
 		
		Vector3 forward = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * velocidade;
		transform.Rotate(new Vector3(0,Input.GetAxis("Horizontal") * giro *Time.deltaTime,0));
 		
		objetoCharControler.Move(forward * Time.deltaTime);
		objetoCharControler.SimpleMove(Physics.gravity);
 		
		if(Input.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) { vetorDirecao.y = pulo; }
		} 
 
 
 
 
		if(Input.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) {
				vetorDirecao.y = pulo;
				jogador.GetComponent<Animation>().Play("JUMP");
			}
		}else
		{
			if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")  )
			{
				if (!animacao.IsPlaying("JUMP"))
				{	 
					jogador.GetComponent<Animation>().Play("WALK");
				}
 				
 				
 				
			}else
			{
				if (objetoCharControler.isGrounded == true) 
				{	
					jogador.GetComponent<Animation>().Play("IDLE");
				}
			}
		}
 
 
 
 
		vetorDirecao.y -= gravidade * Time.deltaTime;	
		objetoCharControler.Move(vetorDirecao * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "OVO"){
			Instantiate(ParticulaOvo, other.gameObject.transform.position, Quaternion.identity);
			other.gameObject.SetActive(false);
			numeroObjts=numeroObjts+5; 
			verificaPickObjetos();
		}
		if (other.gameObject.tag == "PENA"){
			Instantiate(ParticulaPena, other.gameObject.transform.position, Quaternion.identity);
			other.gameObject.SetActive(false);
			numeroObjts++; 
			verificaPickObjetos();
		}
		if (other.gameObject.tag == "ESTRELA"){
			if(podePegarEst) {
				Instantiate(ParticulaEstrela, other.gameObject.transform.position, Quaternion.identity);
				other.gameObject.SetActive(false);
				Invoke("carregaFase",3);
			}
		}
		if (other.gameObject.tag == "FOGUEIRA"){
			InvokeRepeating("mudaEstadoFelpudo",0,0.1f);
			objetoCharControler.Move(transform.TransformDirection(Vector3.back)*3);
		}
		if (other.gameObject.tag == "BURACO"){
			Application.LoadLevel("CenaAventura");
		}
	}

	void mudaEstadoFelpudo(){
		contaPisca++;
		jogador.SetActive(!jogador.activeInHierarchy);
		if (contaPisca>7){contaPisca=0;jogador.SetActive(true);CancelInvoke();}
	}

	void verificaPickObjetos(){
		if(numeroObjts>10){
			podePegarEst = true;
			Destroy(ParticulaFogo);
		}
	}
	
	void carregaFase(){
		Application.LoadLevel("CenaAventura");
	}

}

