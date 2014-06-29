﻿using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public Transform ball;
	public Sprite[] sprites;
	private bool isQuitting = false;

	void Awake() 
	{
		var renderer = this.GetComponent<SpriteRenderer>();
		renderer.color = new Color(3, 3, 3);
		renderer.sprite = sprites[Random.Range(0, 3)];
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnApplicationQuit() 
	{ 
		isQuitting = true; 
	}
	void OnDestroy() {
		if (!isQuitting)
		{
		    GameObject newBall = Instantiate(ball, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity) as GameObject;
			if( newBall )
			{
				newBall.layer = 8;
			}
		}
	}

}
