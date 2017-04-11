using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundController_SP : MonoBehaviour {

	public enum actions
	{
		Normal,
		Fall,
		Bounce,
		Hurt,
		Move,
		Mirage
	}
	public actions action;
	public float fall = 0.5f;
	public int bounce = 3;
	public int hurt = 2;
	public int move = 2;
	public bool used = false;
	public int i;
	public int j;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals ("Player")) {
			if (action == actions.Fall) {
				GetComponent<Rigidbody> ().isKinematic = false;
				GetComponent<Rigidbody> ().useGravity = true;
			}
			if (action == actions.Bounce) {
				collision.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 7, 2), ForceMode.Impulse);

			}
			if (action == actions.Move) {
				if (!used) {
					used = true;
					gameObject.transform.Translate (0, 0, move);
					collision.gameObject.transform.Translate (0, 0, move);
				}
			}
			if (action == actions.Mirage) {
				Destroy (gameObject);
			}
		}
	}
	void OnCollisionStay(Collision collision){
		if (collision.gameObject.tag.Equals ("Player")) {
			if (action == actions.Hurt) {
				collision.gameObject.GetComponent<Health_SP> ().TakeDamage (hurt);
			}
		}
	}
}
