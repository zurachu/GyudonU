using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private GameObject canvas;
	private GameObject customerPrefab;

	// Use this for initialization
	void Start()
	{
		canvas = GameObject.Find("Canvas");
		customerPrefab = Resources.Load<GameObject>("Prefabs/Customer");
        for (int i = 0; i < 6; i++)
        {
            AddCustomer(i);
        }
	}

	// Update is called once per frame
	void Update()
	{

	}

    private void AddCustomer(int index)
    {
		var instance = Instantiate(customerPrefab);
        var rect = instance.GetComponent<RectTransform>();
        var position = rect.transform.position;
        var width = rect.sizeDelta.x;
        position.x += width * index;
        rect.transform.position = position;
		instance.transform.SetParent(canvas.transform, false);
	}
}
