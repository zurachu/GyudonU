using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clerk : MonoBehaviour {

    public Sprite normal;
    public Sprite sad;

	// Use this for initialization
	void Start () {
        GetComponent<UnityEngine.UI.Image>().sprite = normal;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGameOver()
    {
		GetComponent<UnityEngine.UI.Image>().sprite = sad;
	}
}
