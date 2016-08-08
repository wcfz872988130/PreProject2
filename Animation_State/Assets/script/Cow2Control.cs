using UnityEngine;
using System.Collections;

public class Cow2Control : MonoBehaviour {
	private Animator animation1;
	private float m_time = 0.0f;
	private bool direction = false;
	// Use this for initialization
	void Start () {
		animation1 = this.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		m_time += Time.deltaTime;
		AnimatorStateInfo stateInfo = animation1.GetCurrentAnimatorStateInfo (0);
		if (stateInfo.fullPathHash == Animator.StringToHash ("Base Layer.anima2@run") && m_time>1.0f)
		{
			animation1.SetBool ("keepSilence",true);
			animation1.SetBool ("StartMove",false);
			m_time = 0.0f;
		}
		if (stateInfo.fullPathHash == Animator.StringToHash ("Base Layer.silence") && m_time>3.0f && direction==true)
		{
			animation1.SetBool ("StartTranslate",true);
			animation1.SetBool ("keepSilence",false);
			m_time = 0.0f;
		}

		if (stateInfo.fullPathHash == Animator.StringToHash ("Base Layer.silence") && m_time>3.0f)
		{
			animation1.SetBool ("StartMove",true);
			animation1.SetBool ("keepSilence",false);
			m_time = 0.0f;
		}

		if (stateInfo.fullPathHash == Animator.StringToHash ("Base Layer.anima2@run2") && m_time>1.0f)
		{
			animation1.SetBool ("StartTranslate",false);
			animation1.SetBool ("keepSilence",true);
			m_time = 0.0f;
			direction = false;
		}
	}
}
