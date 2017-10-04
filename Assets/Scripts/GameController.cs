using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameController : MonoBehaviour {

    private const int NumChair = 6;

	public GameObject canvas;
    public GameObject popularityGauge;
	public GameObject customerPrefab;
    public UnityEngine.UI.Text salesLabel;

    private GameObject[] customer = new GameObject[NumChair];
    private int sales = 0;
    public float popularityMax;
    public float popularity;

	// Use this for initialization
	void Start()
	{
        UpdateSales();
        Assert.IsTrue(0 < popularity && popularity <= popularityMax);
        UpdatePopularity();
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
        var customerScript = instance.GetComponent<Customer>();
        customerScript.SetType(Customer.RandomType());
        customerScript.ResultCallback += (diffSales, diffPopularity) =>
        {
            sales += diffSales;
            UpdateSales();
            popularity += diffPopularity;
            if (popularity <= 0)
            {
                popularity = 0;
                // @todo ゲームオーバー処理
            }
            else if (popularityMax < popularity)
            {
                popularity = popularityMax;
            }
            UpdatePopularity();
        };
		customer[index] = instance;
	}

    private void UpdateSales()
    {
		salesLabel.text = sales.ToString() + " G$";
	}

    private void UpdatePopularity()
    {
        var popularityGaugeScale = new Vector3(popularity / popularityMax, 1, 1);
        popularityGauge.GetComponent<RectTransform>().localScale = popularityGaugeScale;
    }
}
