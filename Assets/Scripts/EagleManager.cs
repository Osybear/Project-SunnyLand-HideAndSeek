using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleManager : MonoBehaviour {

	public List<Transform> m_Spots;
	public List<GameObject> m_Bushes;
	public Transform m_SpotOffScreen;
	public Transform m_Eagle;
	public SpriteRenderer m_Renderer;
	public Animator m_Animator;
	public GameObject m_Player;

	public float m_Speed;
	public int m_RandomIndex;

	public IEnumerator ChooseRandom(){

		m_RandomIndex = Random.Range(0,5);
		for(int i = 0; i < 2; i++){
			while(m_Eagle.position != m_Spots[0].position){
				float step = m_Speed * Time.deltaTime;
				m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, m_Spots[0].position, step);
				yield return new WaitForEndOfFrame();
			}
			m_Renderer.flipX = true;

			while(m_Eagle.position != m_Spots[4].position){
				float step = m_Speed * Time.deltaTime;
				m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, m_Spots[4].position, step);
				yield return new WaitForEndOfFrame();
			}
			m_Renderer.flipX = false;
		}

		while(m_Eagle.position != m_Spots[m_RandomIndex].position){
			float step = m_Speed * Time.deltaTime;
			m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, m_Spots[m_RandomIndex].position, step);
			yield return new WaitForEndOfFrame();
		}

		m_Renderer.flipX = true;
		yield return new WaitForSeconds(.5f);
		m_Renderer.flipX = false;
		yield return new WaitForSeconds(.5f);
		m_Renderer.flipX = true;
		yield return new WaitForSeconds(.5f);
		m_Renderer.flipX = false;

		m_Animator.SetBool("Diving", true);

		Vector3 offset = new Vector3(0, .88f, 0);
		while(m_Eagle.position != m_Bushes[m_RandomIndex].transform.position + offset){
			float step = m_Speed * 2 * Time.deltaTime;
			m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, m_Bushes[m_RandomIndex].transform.position + offset, step);
			yield return new WaitForEndOfFrame();
		}

		m_Animator.SetBool("Diving", false);

		if(GameManager.singleton.m_HiddenBush == m_Bushes[m_RandomIndex])
		{
			m_Player.transform.SetParent(m_Eagle.transform, true);
			m_Player.GetComponent<Rigidbody2D>().gravityScale = 0;
			m_Player.GetComponent<Animator>().SetBool("Killed" , true);
			GameManager.singleton.m_Killed = true;

			while(m_Eagle.position != m_Spots[m_RandomIndex].position){
				float step = m_Speed * Time.deltaTime *.5f;
				m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, m_Spots[m_RandomIndex].position, step);
				yield return new WaitForEndOfFrame();
			}
			yield break;
		}

		yield return new WaitForSeconds(1f);

		while(m_Eagle.position != m_Spots[m_RandomIndex].position){
			float step = m_Speed * Time.deltaTime *.5f;
			m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, m_Spots[m_RandomIndex].position, step);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(1f);

		m_Renderer.flipX = true;
		while(m_Eagle.position != m_SpotOffScreen.position){
			float step = m_Speed * Time.deltaTime *.5f;
			m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, m_SpotOffScreen.position, step);
			yield return new WaitForEndOfFrame();
		}
		m_Renderer.flipX = false;
		StartCoroutine(GameManager.singleton.Hide());
		yield return null;
	}

	public IEnumerator GetUnhiddenPlayer(){
		m_Player.GetComponent<Animator>().SetBool("Killed" , true);
		Vector3 PlayerPos = m_Player.transform.position;
		Vector3 EaglePos = m_Eagle.transform.position;
		Vector3 PosAbovePlayer = new Vector3(PlayerPos.x, EaglePos.y, 0);

		while(m_Eagle.position != PosAbovePlayer){
			float step = m_Speed * Time.deltaTime;
			m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, PosAbovePlayer, step);
			yield return new WaitForEndOfFrame();
		}

		m_Animator.SetBool("Diving", true);

		Vector3 offset = new Vector3(0, .88f, 0);
		while(m_Eagle.position != PlayerPos + offset){
			float step = m_Speed * 2 * Time.deltaTime;
			m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, PlayerPos + offset, step);
			yield return new WaitForEndOfFrame();
		}

		m_Animator.SetBool("Diving", false);

		m_Player.transform.SetParent(m_Eagle.transform, true);
		m_Player.GetComponent<Rigidbody2D>().gravityScale = 0;
		GameManager.singleton.m_Killed = true;

		while(m_Eagle.position != PosAbovePlayer){
			float step = m_Speed * Time.deltaTime;
			m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, PosAbovePlayer, step);
			yield return new WaitForEndOfFrame();
		}

		yield return null;
	}
}
