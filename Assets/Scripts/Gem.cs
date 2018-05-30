using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	private void Update() {
		if(GameManager.singleton.m_Killed){
			GetComponent<Collider2D>().enabled = false;
			GetComponent<Animator>().SetTrigger("Collided");
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			GetComponent<Collider2D>().enabled = false;
			GetComponent<Animator>().SetTrigger("Collect");
			GameManager.singleton.AddGem();
		}

		if(other.name == "Collidable"){
			GetComponent<Collider2D>().enabled = false;
			GetComponent<Animator>().SetTrigger("Collided");
		}
	}
	
	public void Deactivate(){
		Destroy(gameObject);
	}
}
