using UnityEngine;
using System.Collections;

public class gema : MonoBehaviour {

	public string nivel = "sonia";
		void OnTriggerEnter2D(Collider2D other){
		if(other.transform.tag == "Player")
			Application.LoadLevel (nivel);
	}
}
