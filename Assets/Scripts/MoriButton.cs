using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoriButton : MonoBehaviour
	, IPointerDownHandler
	, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public enum MoriSize
	{
		Namimori,
		Oomori,
		Tokumori,
	};
	public MoriSize moriSize;

	private GameObject canvas;
	private GameObject gyudonPrefab;
	private GameObject gyudonInstance;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Canvas");
		gyudonPrefab = Resources.Load<GameObject>("Prefabs/Gyudon");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SetGyudonPosition(PointerEventData eventData)
	{
		gyudonInstance.transform.SetParent(canvas.transform.parent, false);
		Vector2 localPosition = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			canvas.GetComponent<RectTransform>(), eventData.position, Camera.main, out localPosition);
		gyudonInstance.GetComponent<RectTransform>().position = localPosition;
		gyudonInstance.transform.SetParent(canvas.transform, false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		gyudonInstance = Instantiate(gyudonPrefab);
		SetGyudonPosition(eventData);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log(eventData.position);
	}

	public void OnDrag(PointerEventData eventData)
	{
		SetGyudonPosition(eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
        gyudonInstance.GetComponent<Rigidbody2D>().AddForce(eventData.delta, ForceMode2D.Impulse);
	}
}
