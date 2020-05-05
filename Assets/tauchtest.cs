using UnityEngine;
using System.Collections;

public class tauchtest : MonoBehaviour {
	private float moveSpeed;
	private float movejump;
	private float z;

	void Start () {

		movejump = 0.03f;
		moveSpeed = 0.2f;

	}
	void FixedUpdate () {
	
	
	
	if (Input.touchCount == 1) {
			transform.Translate(Input.touches[0].deltaPosition.x*.1f*moveSpeed,
			                    0f,
			                    Input.touches[0].deltaPosition.y*.1f*moveSpeed);
				}




	}
}
