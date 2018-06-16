using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager singleton;
	public EagleManager m_EagleManager;

	public int m_PlayerGems = 0;
	public int m_Rounds = 0;
	public float m_TwinkleRate;

	public bool m_Killed = false;
	public bool m_DisableControls = false;
	public bool m_Hidden = false;
	public bool m_GameStarted = false;

	public Text m_GemCount;
	public Text m_PressEnter;
	public GameObject m_Player;
	public GameObject m_HiddenBush;

	private Coroutine m_TextTwinkle;

	private void Awake() {
		if(singleton != null)
             GameObject.Destroy(singleton);
         else
             singleton = this;
		
		m_PressEnter.text = "Press Enter" +"\nTo Start";
	}

	private void Start() {
		m_TextTwinkle = StartCoroutine(TextTwinkle());
	}

	private void Update() {
		if(m_GameStarted == false && Input.GetKeyDown(KeyCode.Return)){
			m_GameStarted = true;
			SoundManager.singleton.PlayAudio("MusicLoop");
			StopCoroutine(m_TextTwinkle);
			StartCoroutine(Hide());
		}

		if(m_Killed == true && Input.GetKeyDown(KeyCode.Return)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void AddGem(){
		m_PlayerGems++;
		if(m_PlayerGems < 10)
			m_GemCount.text = "00" + m_PlayerGems;
		else if(m_PlayerGems >= 10 && m_PlayerGems < 100)
			m_GemCount.text = "0" + m_PlayerGems;
		else if(m_PlayerGems >= 100)
			m_GemCount.text = "" + m_PlayerGems;
	}

	public IEnumerator TextTwinkle(){
		string CurrentText = m_PressEnter.text;

		while(true){
			yield return new WaitForSeconds(m_TwinkleRate);
			m_PressEnter.text = null;
			yield return new WaitForSeconds(m_TwinkleRate);
			m_PressEnter.text = CurrentText;
		}
	}

	public IEnumerator Hide(){
		m_DisableControls = false;
		m_PressEnter.text = "Hide!" + "\n3";
		yield return new WaitForSeconds(1);
		m_PressEnter.text = "Hide!" + "\n2";
		yield return new WaitForSeconds(1);
		m_PressEnter.text = "Hide!" + "\n1";
		yield return new WaitForSeconds(1);
		m_PressEnter.text = "";
		m_DisableControls = true;
		
		if(m_HiddenBush != null)
			m_Player.transform.position = m_HiddenBush.transform.position;

		if(m_Hidden == false){
			StartCoroutine(m_EagleManager.GetUnhiddenPlayer());
		}else
			StartCoroutine(m_EagleManager.ChooseRandom());
	}

	public void StopGame(){
		if(m_Rounds == 1)
			m_PressEnter.text = "You Survived " + m_Rounds + " Round\nPress Enter To Restart";
		else
			m_PressEnter.text = "You Survived " + m_Rounds + " Rounds\nPress Enter To Restart";

		SoundManager.singleton.StopAudio("MusicLoop");
	}
}
