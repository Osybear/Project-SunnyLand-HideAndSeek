using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager singleton;

	public AudioSource m_BirdAttackCaw;
	public AudioSource m_BirdFlapLoop;
	public AudioSource m_BirdHitPlayer;
	public AudioSource m_BirdSwoop;
	public AudioSource m_EnterBush;
	public AudioSource m_ExitBush;
	public AudioSource m_MusicLoop;

	private void Awake() {
		if(singleton != null)
             GameObject.Destroy(singleton);
         else
             singleton = this;
	}

	public void PlayAudio(string name){
		switch(name){
			case "BirdAttackCaw":
				m_BirdAttackCaw.Play();
				break;
			case "BirdFlapLoop":
				m_BirdFlapLoop.Play();
				break;
			case "BirdHitPlayer":
				m_BirdHitPlayer.Play();
				break;
			case "BirdSwoop":
				m_BirdSwoop.Play();
				break;
			case "EnterBush":
				m_EnterBush.Play();
				break;
			case "ExitBush":
				m_ExitBush.Play();
				break;
			case "MusicLoop":
				m_MusicLoop.Play();
				break;
			default:
				Debug.LogError("no sound found");
				break;
		}
	}

	public void StopAudio(string name){
		switch(name){
			case "BirdFlapLoop":
				m_BirdFlapLoop.Stop();
				break;
			case "MusicLoop":
				m_MusicLoop.Stop();
				break;
			default:
				Debug.LogError("no sound found");
				break;
		}
	}
}
