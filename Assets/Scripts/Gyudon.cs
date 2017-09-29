using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyudon : MonoBehaviour {

	public enum MoriSize
	{
		Namimori,
		Oomori,
		Tokumori,
	};

    static public MoriSize RandomMoriSize()
    {
        var numMoriSize = System.Enum.GetValues(typeof(MoriSize)).Length;
        return (MoriSize)System.Enum.ToObject(typeof(MoriSize), Random.Range(0, numMoriSize));
    }

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
