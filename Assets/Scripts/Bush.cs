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
			GameManager.singleton.m_HiddenBush = gameObject;
			GameManager.singleton.m_Hidden = true;
			SoundManager.singleton.PlayAudio("EnterBush");
		}
	}

	 private void OnTriggerExit2D(Collider2D other) {
		if(other.name == "Player"){
			m_Renderer.color = new Color(255, 255, 255, 1);
			GameManager.singleton.m_HiddenBush = null;
			GameManager.singleton.m_Hidden = false;
			SoundManager.singleton.PlayAudio("ExitBush");
		}
	}

	public void Deactivate(){
		Destroy(gameObject);
	}
}
