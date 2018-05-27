using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour {

	private SpriteRenderer m_Renderer;
	
	private void Awake() {
		m_Renderer = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "Player"){
			m_Renderer.color = new Color(255, 255, 255, .5f);
		}
	}

	 private void OnTriggerExit2D(Collider2D other) {
		if(other.name == "Player"){
			m_Renderer.color = new Color(255, 255, 255, 1);
		}
	}

}
