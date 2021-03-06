﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	public GameObject paddle;
	public AudioClip PaddleHitSound;
	public AudioClip BrickBreakSound;
	public AudioClip BallLostSound;
	/// <summary>
	/// Check this if this is the first ball in the level
	/// </summary>
	public bool initialBall = false;

	private Rigidbody2D m_rb;
	private bool justSpawned;
	private bool launched = false;

	// Use this for initialization
	void Start ()
	{
		float speed = 5.0f;
		m_rb = GetComponent< Rigidbody2D > ();
		if (m_rb)
		{
			if (!initialBall) {
				float randomAngle = Random.Range (0.0f, 360.0f);
				m_rb.velocity = new Vector2 (speed * Mathf.Cos (Mathf.Deg2Rad * randomAngle), speed * Mathf.Sin (Mathf.Deg2Rad * randomAngle));
				justSpawned = true;
				this.GetComponent<SpriteRenderer>().material.color = new Color(1f,1f,1f,0.5f);
			}
			else
			{
				this.GetComponent<SpriteRenderer>().material.color = new Color(1f,1f,1f,1f);
				m_rb.velocity = Vector3.zero;
				m_rb.angularVelocity = 0.0f;
			}
		}

	}

	public void SetInitialBall()
	{
		initialBall = true;
		justSpawned = false;
		launched = false;
		
		this.GetComponent<SpriteRenderer>().material.color = new Color(1f,1f,1f,1f);
		this.gameObject.layer = 8;
	}

	// Update is called once per frame
	void Update ()
	{
		//transform.position += m_velocity * Time.deltaTime;
		if ( !launched && paddle != null ) 
		{
			Vector3 newPosition = new Vector3( paddle.transform.position.x, transform.position.y, transform.position.z );
			transform.position = newPosition;
			m_rb.velocity = Vector3.zero;
			m_rb.angularVelocity = 0.0f;
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		//If collided with a brick, destroy it
		if (collision.gameObject.CompareTag( "Brick"))
		{
			if(!justSpawned)
				Destroy(collision.gameObject);
			
			this.GetComponent<AudioSource>().clip = BrickBreakSound;
			this.GetComponent<AudioSource>().Play ();
		}
		//If collided withe the paddle, change the angle of the ball based on the direction
		if (collision.gameObject.name == "Paddle")
		{
			if (this.justSpawned) {
				this.justSpawned = false;
				this.GetComponent<SpriteRenderer>().material.color = new Color(1f,1f,1f,1f);
				this.gameObject.layer = 8;
			}
		    Vector3 directionToBall = transform.position - collision.gameObject.transform.position;
			float ballSpeed = 7.0f;
		    
		    Vector3 newBallVelocity = directionToBall.normalized * ballSpeed;
			newBallVelocity.x *= 2;
			if( m_rb )
			{
				m_rb.velocity = newBallVelocity;
			}

			
			this.GetComponent<AudioSource>().clip = PaddleHitSound;
			this.GetComponent<AudioSource>().Play ();
		}

		if( collision.gameObject.name == "Bottom Wall" )
		{
			this.GetComponent<AudioSource>().clip = BallLostSound;
			this.GetComponent<AudioSource>().Play ();
		}
	}

	//Launches the first ball
	public void LaunchBall()
	{
		if ( !launched )
		{
			float speed = 5.0f;
			m_rb.velocity = new Vector2( speed * Mathf.Cos ( Mathf.Deg2Rad * 45.0f ), speed * Mathf.Sin ( Mathf.Deg2Rad * 45.0f ) );
			launched = true;
		}
	}

}
