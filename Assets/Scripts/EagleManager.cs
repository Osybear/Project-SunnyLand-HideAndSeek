using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleManager : MonoBehaviour {

	public List<Transform> m_Spots;
	public List<GameObject> m_Bushes;
	public GameObject m_HiddenBush;
	public Transform m_SpotOffScreen;
	public Transform m_Eagle;
	public SpriteRenderer m_Renderer;
	public Animator m_Animator;

	public float m_Speed;
	public int m_RandomIndex;

	private void Start() {
		//StartCoroutine(Patrol());
	}

	public IEnumerator Patrol(){
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

		while(m_Eagle.position != m_Bushes[m_RandomIndex].transform.position){
			float step = m_Speed * 2 * Time.deltaTime;
			m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, m_Bushes[m_RandomIndex].transform.position, step);
			yield return new WaitForEndOfFrame();
		}

		m_Animator.SetBool("Diving", false);

		yield return new WaitForSeconds(1f);

		while(m_Eagle.position != m_Spots[m_RandomIndex].position){
			float step = m_Speed * Time.deltaTime *.5f;
			m_Eagle.position = Vector3.MoveTowards(m_Eagle.position, m_Spots[m_RandomIndex].position, step);
			yield return new WaitForEndOfFrame();
		}

		if(GameManager.singleton.m_isHiding == false)
		yield return null;
	}
}
