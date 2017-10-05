using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public enum Type
    {
        Cowbell,
        JA,
        Kenta,
        Daioh,
        Mai,
        Gorem,
        Chris,
    };

	static public Type RandomType()
	{
		var numTypes = System.Enum.GetValues(typeof(Type)).Length;
		return (Type)System.Enum.ToObject(typeof(Type), Random.Range(0, numTypes));
	}

	private enum Result
    {
        Happy,
        Angry,
    };

    private Image fukidashi;
    private GameObject timeGaugeBase;
    private GameObject timeGauge;
	private GameObject happyFaceMark;
	private GameObject angryFaceMark;
    private GameObject particle;

    public Sprite namimoriFukidashi;
    public Sprite oomoriFukidashi;
    public Sprite tokumoriFukidashi;
    public Sprite[] customerSprite = new Sprite[System.Enum.GetValues(typeof(Type)).Length];
    public float timeMax;

    private float time;
    private Gyudon.MoriSize moriSize;

	public event System.Action<int, float> ResultCallback = delegate { };

	// Use this for initialization
	void Start () {
        fukidashi = transform.Find("Fukidashi").GetComponent<Image>();
		timeGaugeBase = transform.Find("TimeBase").gameObject;
        timeGauge = timeGaugeBase.transform.Find("Time").gameObject;
		happyFaceMark = transform.Find("HappyFaceMark").gameObject;
		angryFaceMark = transform.Find("AngryFaceMark").gameObject;
        particle = transform.Find("Particle System").gameObject;
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

    public void SetType(Type type)
    {
        GetComponent<Image>().sprite = customerSprite[(int)type];
    }

    public void Receive(Gyudon.MoriSize size)
    {
        StartCoroutine(SetResult((size == moriSize) ? Result.Happy : Result.Angry));
        GetComponent<Collider2D>().enabled = false;
    }

    private IEnumerator SetResult(Result result)
    {
        int sales = 0;
        float popularity = 0;
        if (result == Result.Happy)
        {
            happyFaceMark.SetActive(true);
            particle.SetActive(true);
			sales = Gyudon.PriceOf(moriSize);
			popularity = 1;
		}
        else
		{
			angryFaceMark.SetActive(true);
			popularity = -1;
		}
        ResultCallback(sales, popularity);
		timeGaugeBase.SetActive(false);
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
}
