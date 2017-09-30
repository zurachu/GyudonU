using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    private enum Result
    {
        Happy,
        Angry,
    };

    private Image fukidashi;
    private GameObject timeGaugeBase;
    private GameObject timeGauge;

    public Sprite namimoriFukidashi;
    public Sprite oomoriFukidashi;
    public Sprite tokumoriFukidashi;
    public float timeMax;

    private float time;
    private Gyudon.MoriSize moriSize;

	public event System.Action<int, float> ResultCallback = delegate { };

	// Use this for initialization
	void Start () {
        fukidashi = transform.Find("Fukidashi").GetComponent<Image>();
		timeGaugeBase = transform.Find("TimeBase").gameObject;
        timeGauge = timeGaugeBase.transform.Find("Time").gameObject;
		time = timeMax;
        moriSize = Gyudon.RandomMoriSize();
        Sprite[] fukidashiSprite = new Sprite[] {
            namimoriFukidashi, oomoriFukidashi, tokumoriFukidashi};
        fukidashi.sprite = fukidashiSprite[(int)moriSize];
    }
	
	// Update is called once per frame
	void Update () {
        if (timeGaugeBase.activeSelf)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                StartCoroutine(SetResult(Result.Angry));
            }
            else
            {
                var timeGaugeScale = new Vector3(time / timeMax, 1, 1);
                timeGauge.GetComponent<RectTransform>().localScale = timeGaugeScale;
            }
        }
	}

    private IEnumerator SetResult(Result result)
    {
        // @todo 吹き出し内容変更
        int sales = (result == Result.Happy) ? 29 : 0;
        float popularity = (result == Result.Happy) ? 1 : -1;
        ResultCallback(sales, popularity);
		timeGaugeBase.SetActive(false);
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
}
