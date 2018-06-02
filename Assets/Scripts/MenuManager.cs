using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Text m_PressEnter;

	public GameObject m_Title;
	public GameObject m_Info;

	public float m_TwinkleRate;
	public int m_EnterCount;

	private void Awake() {
		m_Info.SetActive(false);
	}

	private void Start() {
		StartCoroutine(TextTwinkle());
	}

	public IEnumerator TextTwinkle(){
		while(true){
			yield return new WaitForSeconds(m_TwinkleRate);
			m_PressEnter.color = new Color(m_PressEnter.color.r, m_PressEnter.color.g, m_PressEnter.color.b, 0);
			yield return new WaitForSeconds(m_TwinkleRate);
			m_PressEnter.color = new Color(m_PressEnter.color.r, m_PressEnter.color.g, m_PressEnter.color.b, 1);
		}
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Return)){
			if(m_EnterCount == 0){
				m_Title.SetActive(false);
				m_Info.SetActive(true);
			}
			if(m_EnterCount == 1){
				SceneManager.LoadScene("HideAndSeek");
			}
			m_EnterCount++;
		}
	}
}
