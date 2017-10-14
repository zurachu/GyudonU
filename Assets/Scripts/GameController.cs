using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameController : MonoBehaviour
{

    private const int NumChair = 6;

    public GameObject canvas;
    public Clerk clerk;
    public GameObject popularityGauge;
    public GameObject customerPrefab;
    public UnityEngine.UI.Text salesLabel;
    public GameObject blackOutAndChangeScene;

    private GameObject[] customer = new GameObject[NumChair];
    private int sales = 0;
    public float popularityMax;
    public float popularity;
    public float popularityUpRate;
    public float popularityDownRate;

    // Use this for initialization
    void Start()
    {
        UpdateSales();
        Assert.IsTrue(0 < popularity && popularity <= popularityMax);
        SetPopularityGauge(popularity);
    }

    // Update is called once per frame
    void Update()
    {
        if (0 < popularity && Random.Range(0, 2000) < 100)
        {
            int index = Random.Range(0, NumChair);
            var type = Customer.RandomType();
            if (AddCustomer(index, type) && type == Customer.Type.Mai)
            {
                for (int i = 0; i < NumChair; i++)
                {
                    AddCustomer(i, Customer.Type.Gorem);
                }
            }
        }
        UpdatePopularityGauge();
    }

    private bool AddCustomer(int index, Customer.Type type)
    {
        if (!customer[index])
        {
            var instance = Instantiate(customerPrefab);
            var rect = instance.GetComponent<RectTransform>();
            var position = rect.transform.position;
            var width = rect.sizeDelta.x;
            position.x += width * index;
            rect.transform.position = position;
            instance.transform.SetParent(canvas.transform, false);
            var customerScript = instance.GetComponent<Customer>();
            customerScript.SetType(type);
            customerScript.ResultCallback += (diffSales, diffPopularity) =>
            {
                if (0 < popularity)
                {
                    sales += diffSales;
                    UpdateSales();
                    if (diffPopularity > 0)
                    {
                        popularity += diffPopularity * popularityUpRate;
                    }
                    else
                    {
                        popularity += diffPopularity * popularityDownRate;
                    }
                    if (popularity <= 0)
                    {
                        StartCoroutine(OnGameOver());
                    }
                    else if (popularityMax < popularity)
                    {
                        popularity = popularityMax;
                    }
                }
            };
            customer[index] = instance;
            return true;
        }
        return false;
    }

    private IEnumerator OnGameOver()
    {
        popularity = 0;
        clerk.OnGameOver();
        var moriButtons = Object.FindObjectsOfType<MoriButton>();
        foreach (var button in moriButtons)
        {
            button.enabled = false;
        }
        var restGyudons = GameObject.FindGameObjectsWithTag("Gyudon");
        foreach (var gyudon in restGyudons)
        {
            Destroy(gyudon);
        }
        yield return new WaitForSeconds(5);
        blackOutAndChangeScene.SetActive(true);
        blackOutAndChangeScene.transform.SetParent(canvas.transform.parent, false);
        blackOutAndChangeScene.transform.SetParent(canvas.transform, false);
    }

    private void UpdateSales()
    {
        salesLabel.text = sales.ToString() + " G$";
    }

    private void SetPopularityGauge(float value)
    {
        var popularityGaugeScale = new Vector3(value / popularityMax, 1, 1);
        popularityGauge.GetComponent<RectTransform>().localScale = popularityGaugeScale;
        popularityGauge.GetComponent<GaugeMeshEffect>().Refresh();
    }

    private void UpdatePopularityGauge()
    {
        var current = popularityGauge.GetComponent<RectTransform>().localScale.x * popularityMax;
        var next = (current * 3 + popularity) / 4;
        if (Mathf.Abs(next - popularity) < 1)
        {
            next = popularity;
        }
        SetPopularityGauge(next);
    }
}
