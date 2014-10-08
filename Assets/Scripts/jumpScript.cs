﻿using UnityEngine;
using System.Collections;

public class jumpScript : MonoBehaviour {
	public int jumpForce=200; //Esta es l afuerza de salto
	public int moveForce=100; //Fuerza horizontal al mover
	public int maxSpeed=50; //Velocidad maxima horizontal
	public AudioClip sonidoVolar;
	public AudioClip sonidoHerido;
	public AudioClip sonidoCurar;
	private bool herido = false;
	Animator animation; //esto es necesario para poder manejar las animaciones

	void Start (){ //Al iniciarse cargamos las variables de las animaciones
				animation = GetComponent <Animator> ();
		}
	void Update () {
				if (Input.GetButtonDown ("Jump")) //Cuando pulsamos espacio salta
						saltar (); //mas abajo esta la funcion explicada
				
	
				if (Input.GetKey ("a")) { //cuando se pulsa a se mueva a la izquierda
						mover (moveForce * -1); //multiplicamos negativo para ir a la izquierda
						transform.localScale = new Vector3 (-1, 1, 1);

				}

				if (Input.GetKey ("d")) { //si se pulsa d se mueve a la derecha
						mover (moveForce);
						transform.localScale = new Vector3 (1, 1, 1);
						/*Funcion saltar
		 * esta funcion aplica una fuerza hacia arriba definida por jumpForce
		 * TODO: Falta animacion de salto y sonido
		 */
				
				}

		}

		void saltar (){
		//aplicamos una fuerza con rigidbody2d.Addforce
		if (!herido)
		rigidbody2D.AddForce (new Vector2 (0, jumpForce));
		animation.SetBool ("fly", true);
		// New Vector2(0,jumpforce) es un vector con la x a cero
		AudioSource.PlayClipAtPoint (sonidoVolar, transform.position);
		}

	void damage () {
		herido = true;
				animation.SetBool ("Damage", true);
		AudioSource.PlayClipAtPoint (sonidoHerido, transform.position);
		}
	/*Funcion mover
	 * Parametros: fuerza -> Fuerza que le vamos a aplicar para mover
	 * Aplicamos una fuerza horizontal teniendo en cuenta
	 * no subrepasar la velocidad maxima
	 */ 
	void mover(int fuerza) {
				// creamos una variable para guardar la velocidad actual
				float velocity = rigidbody2D.velocity.x;
				//Mathf.abs nos devuelve el valor absoluto de una variable
				//Si hacemos Mathf.Abs (-10) nos devuelve 10 / -30=30
				if ((fuerza > 0 & velocity < 0) || (fuerza < 0 & velocity > 0)) {
						//si la velocidad absoluta es menos de maxSpeed
						//aplicamos una fuerza horizontal
						rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
						//tambien funciona escribiendo rigidbody2D.AddForce(Vector2.right*fuerza);
				}
				if (Mathf.Abs (velocity) < maxSpeed) {
						rigidbody2D.AddForce (new Vector2 (fuerza, 0));

				}
		}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("Choque");
		animation.SetBool ("fly", false);
		if (coll.gameObject.tag == "enemy")
			damage ();
		if (coll.gameObject.tag == "sombrero")
			curar ();

	}
		

	void curar () {
		herido = false;
		animation.SetBool ("Damage", false);
		AudioSource.PlayClipAtPoint (sonidoCurar, transform.position);

		}
}