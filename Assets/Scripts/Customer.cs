using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

	private GameObject timeGaugeBase;
	private GameObject timeGauge;

	public float timeMax;
    private float time;

	// Use this for initialization
	void Start () {
		timeGaugeBase = transform.Find("TimeBase").gameObject;
        timeGauge = timeGaugeBase.transform.Find("Time").gameObject;
		time = timeMax;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeGaugeBase.activeSelf)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                StartCoroutine(SetResult());
            }
            else
            {
                var timeGaugeScale = new Vector3(time / timeMax, 1, 1);
                timeGauge.GetComponent<RectTransform>().localScale = timeGaugeScale;
            }
        }
	}

    private IEnumerator SetResult()
    {
		// @todo 吹き出し内容変更
		// @todo 評判と売り上げ反映（GameController にコールバックを想定）
		timeGaugeBase.SetActive(false);
		yield return new WaitForSeconds(1);
		// @todo 席を空ける（GameController にコールバックを想定）
		Destroy(gameObject);
	}
}
