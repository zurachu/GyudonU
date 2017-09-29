using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private const int NumChair = 6;

	public GameObject canvas;
	public GameObject customerPrefab;

    private GameObject[] customer = new GameObject[NumChair];

	// Use this for initialization
	void Start()
	{
    
    }

	// Update is called once per frame
	void Update()
	{
        if(Random.Range(0, 2000) < 100)
        {
            int index = Random.Range(0, NumChair);
            if (!customer[index])
            {
				AddCustomer(index);
			}
        }
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
        customer[index] = instance;
	}
}
