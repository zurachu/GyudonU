using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyudon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "GyudonArea")
        {
            Destroy(gameObject);
        }
    }
}
