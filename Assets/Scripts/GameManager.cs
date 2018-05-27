using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager singleton;

	public int m_PlayerGems = 0;
	public float m_TwinkleRate;

	public bool m_isDead = false;
	public bool m_isHiding = false;
	public bool m_hasStarted = false;

	public Text m_GemCount;
	public Text m_PressEnter;
	public GameObject m_Player;

	private Coroutine m_TextTwinkle;

	public List<GameObject> Bushes;

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
		if(m_hasStarted == false && Input.GetKeyDown(KeyCode.Return)){
			m_hasStarted = true;
			StopCoroutine(m_TextTwinkle);
			m_PressEnter.gameObject.SetActive(true);
			StartCoroutine(HideText());
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
		while(true){
			yield return new WaitForSeconds(m_TwinkleRate);
			m_PressEnter.gameObject.SetActive(false);
			yield return new WaitForSeconds(m_TwinkleRate);
			m_PressEnter.gameObject.SetActive(true);
		}
	}

	private IEnumerator HideText(){
		m_PressEnter.text = "Hide!" + "\n3";
		yield return new WaitForSeconds(1);
		m_PressEnter.text = "Hide!" + "\n2";
		yield return new WaitForSeconds(1);
		m_PressEnter.text = "Hide!" + "\n1";
		yield return new WaitForSeconds(1);
		m_PressEnter.text = "";
		m_isHiding = true;
	}
}
