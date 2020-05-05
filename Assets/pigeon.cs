using UnityEngine;
using System.Collections;

public class pigeon : MonoBehaviour {
	
	private float moveSpeed;
	Vector3 position;

	// Use this for initialization
	void Start () {

		position = transform.position;
		moveSpeed = 6f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {



	
		transform.Translate (moveSpeed*Input.GetAxis ("Horizontal") * Time.deltaTime, 0f, (moveSpeed*Input.GetAxis ("Vertical") * Time.deltaTime));
	
	}
}
