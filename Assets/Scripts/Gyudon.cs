using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyudon : MonoBehaviour
{

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

    static public int PriceOf(MoriSize size)
    {
        switch (size)
        {
            case MoriSize.Namimori:
                return 29;
            case MoriSize.Oomori:
                return 39;
            case MoriSize.Tokumori:
                return 49;
        }
        throw new System.ArgumentOutOfRangeException();
    }

    public MoriSize moriSize;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Customer")
        {
            collision.GetComponent<Customer>().Receive(moriSize);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "GyudonArea")
        {
            Destroy(gameObject);
        }
    }
}
