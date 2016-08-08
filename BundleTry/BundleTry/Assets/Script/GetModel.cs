using UnityEngine;
using System.Collections;

public class GetModel : MonoBehaviour {
	private NetBundleManager Instance = null;
	private Transform m_transform;
	// Use this for initialization
	void Start () {
		Instance = NetBundleManager.GetNetManager ();
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(200,250,100,20),"Rotation"))
		{
			Instance.GetMyModel ("cube.ab");
		}
		if(GUI.Button(new Rect(200,50,100,20),"1"))
		{
			Instance.GetMyModel ("plane.ab");
		}
	}
}
