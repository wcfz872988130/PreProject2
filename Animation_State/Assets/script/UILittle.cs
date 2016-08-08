using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public class UILittle : MonoBehaviour,IPointerEnterHandler{
	public void OnPointerEnter(PointerEventData data)
	{
		Debug.Log ("On Enter");
	}
}
