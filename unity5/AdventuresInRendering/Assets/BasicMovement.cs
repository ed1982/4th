using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.UpArrow))
		{
			transform.position = transform.position + transform.rotation * transform.forward * 0.1f;
		}
		if (Input.GetKey (KeyCode.DownArrow))
		{
			transform.position = transform.position + transform.rotation * transform.forward * -0.1f;
		}
		if (Input.GetKey (KeyCode.LeftArrow))
		{
			transform.position = transform.position + transform.rotation * transform.right * -0.1f;
		}
		if (Input.GetKey (KeyCode.RightArrow))
		{
			transform.position = transform.position + transform.rotation * transform.right * 0.1f;
		}
		if (Input.GetKey (KeyCode.A))
		{
			transform.position = transform.position + transform.rotation * transform.up * 0.1f;
		}
		if (Input.GetKey (KeyCode.Z))
		{
			transform.position = transform.position + transform.rotation * transform.up * -0.1f;
		}
		
	}
}
