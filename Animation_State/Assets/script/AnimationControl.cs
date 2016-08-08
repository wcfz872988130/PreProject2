using UnityEngine;
using System.Collections;

public class AnimationControl : MonoBehaviour {
	private Animator animation1;
	private float m_time = 0.0f;
	// Use this for initialization
	void Start () {
		animation1 = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		m_time += Time.deltaTime;
		AnimatorStateInfo stateInfo = animation1.GetCurrentAnimatorStateInfo (0);
		if (stateInfo.fullPathHash == Animator.StringToHash ("Base Layer.move") && m_time>4.0f)
		{
			animation1.SetBool ("keepSilence",true);
			animation1.SetBool ("StartMove",false);
			m_time = 0.0f;
		}
		if (stateInfo.fullPathHash == Animator.StringToHash ("Base Layer.silence") && m_time>3.0f)
		{
			animation1.SetBool ("keepSilence",false);
			animation1.SetBool ("StartMove",true);
			m_time = 0.0f;
		}
	}
}
