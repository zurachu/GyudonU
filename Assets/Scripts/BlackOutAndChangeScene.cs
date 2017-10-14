using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BlackOutAndChangeScene : MonoBehaviour {

    public string nextScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var image = GetComponent<Image>();
        var color = image.color;
        color.a += Time.deltaTime;
        image.color = color;
        if (color.a >= 1)
        {
            SceneManager.LoadScene(nextScene);
        }
	}
}
