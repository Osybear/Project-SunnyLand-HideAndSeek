using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D m_RigidBody;
	private SpriteRenderer m_Renderer;
	private Animator m_Animator;

	public float m_MovementSpeed = 0;
	public float m_JumpForce = 0;
	public bool m_isGrounded = true;

	private void Awake() {
		m_RigidBody = GetComponent<Rigidbody2D>();
		m_Renderer = GetComponent<SpriteRenderer>();
		m_Animator = GetComponent<Animator>();
	}

	private void Update() {
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		if(GameManager.singleton.m_isHiding == true)
			moveHorizontal = 0;
		Vector2 movement = new Vector2(moveHorizontal * m_MovementSpeed, m_RigidBody.velocity.y);
		m_RigidBody.velocity = movement;

		if(Input.GetKeyDown(KeyCode.Space) && m_isGrounded == true && GameManager.singleton.m_isHiding == false)
			m_RigidBody.velocity = new Vector2(movement.x, m_JumpForce);

		if(moveHorizontal < 0)
			m_Renderer.flipX = true;
		if(moveHorizontal > 0)
			m_Renderer.flipX = false;

		m_Animator.SetFloat("YVelocity", m_RigidBody.velocity.y);
		m_Animator.SetInteger("Run", Mathf.Abs((int)moveHorizontal));
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "Collidable"){
			m_Animator.SetBool("Grounded", true);
			m_isGrounded = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.name == "Collidable"){
			m_Animator.SetBool("Grounded", false);
			m_isGrounded = false;
		}
	}
}
