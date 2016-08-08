using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	int num=0;
	void Start()
	{
		StartCoroutine (Calculate());
	}

	IEnumerator Calculate()
	{
		for(int i=0;i<12;i++)
		{
			Debug.Log (i);
			yield return new WaitForSeconds (1.0f);
		}	
	}
}
