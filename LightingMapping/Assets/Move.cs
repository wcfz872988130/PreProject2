using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
		{
			gameObject.transform.Translate (new Vector3(Time.deltaTime*5,0,0));
		}
		if(Input.GetKeyDown(KeyCode.S))
		{
			gameObject.transform.Translate (new Vector3(-Time.deltaTime*5,0,0));
		}
	}
}
