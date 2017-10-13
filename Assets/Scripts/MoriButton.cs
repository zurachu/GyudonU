using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoriButton : MonoBehaviour
    , IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public Gyudon.MoriSize moriSize;

    private GameObject canvas;
    private GameObject gyudonPrefab;
    private GameObject gyudonInstance;

    private const float flickSpeedRate = 10;

    // Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        gyudonPrefab = Resources.Load<GameObject>("Prefabs/Gyudon");
    }

    // Update is called once per frame
    void Update()
    {

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
        gyudonInstance.GetComponent<Gyudon>().moriSize = moriSize;
        SetGyudonPosition(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gyudonInstance)
        {
            var rigidBody = gyudonInstance.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(eventData.delta.normalized * flickSpeedRate, ForceMode2D.Impulse);
            if (rigidBody.velocity.magnitude < 1)
            {
                Destroy(gyudonInstance);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gyudonInstance)
        {
            SetGyudonPosition(eventData);
        }
    }
}
