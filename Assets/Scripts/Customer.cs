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
        UnityChan,
    };

    static public Type RandomType()
    {
        Type[] candidates = {
            Type.Cowbell,
            Type.JA,
            Type.Kenta,
            Type.Daioh,
            Type.Mai,
            Type.Chris,
            Type.UnityChan,
        };
        return candidates[Random.Range(0, candidates.Length)];
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

    public GameObject goremParticlePrefab;
    public Sprite namimoriFukidashi;
    public Sprite oomoriFukidashi;
    public Sprite tokumoriFukidashi;
    public Sprite[] customerSprite = new Sprite[System.Enum.GetValues(typeof(Type)).Length];
    public float timeMax;

    private float time;
    private Gyudon.MoriSize moriSize;

    public event System.Action<int, float> ResultCallback = delegate { };

    // Use this for initialization
    void Start()
    {
        fukidashi = transform.Find("Fukidashi").GetComponent<Image>();
        timeGaugeBase = transform.Find("TimeBase").gameObject;
        timeGauge = timeGaugeBase.transform.Find("Time").gameObject;
        happyFaceMark = transform.Find("HappyFaceMark").gameObject;
        angryFaceMark = transform.Find("AngryFaceMark").gameObject;
        particle = transform.Find("Particle System").gameObject;
        time = timeMax;
        moriSize = Gyudon.RandomMoriSize();
        Sprite[] fukidashiSprite = {
            namimoriFukidashi, oomoriFukidashi, tokumoriFukidashi};
        fukidashi.sprite = fukidashiSprite[(int)moriSize];
    }

    // Update is called once per frame
    void Update()
    {
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
                timeGauge.GetComponent<GaugeMeshEffect>().Refresh();
            }
        }
    }

    public void SetType(Type type)
    {
        GetComponent<Image>().sprite = customerSprite[(int)type];
        var timeGaugeMesh = transform.Find("TimeBase").Find("Time").GetComponent<GaugeMeshEffect>();
        if (type == Type.Kenta)
        {
            timeMax /= 1.5f;
            timeGaugeMesh.maxColor = Color.yellow;
        }
        else if (type == Type.Gorem)
        {
            timeMax *= 1.5f;
            timeGaugeMesh.maxColor = Color.cyan;
            Instantiate(goremParticlePrefab).transform.SetParent(transform, false);
        }
        timeGaugeMesh.Refresh();
    }

    public void Receive(Gyudon.MoriSize size)
    {
        StartCoroutine(SetResult((size == moriSize) ? Result.Happy : Result.Angry));
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
        fukidashi.enabled = false;
        ResultCallback(sales, popularity);
        timeGaugeBase.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
