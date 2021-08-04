using System.Globalization;
using UnityEngine;
using TMPro;

public class Time : MonoBehaviour
{
    private bool start;

    private TextMeshProUGUI time;

    // Start is called before the first frame update
    void Start()
    {
        start = true;
        time = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ":";
            nfi.NumberDecimalDigits = 2;
            time.text = GameManager.Instance.TimePassed.ToString(nfi);
            start = false;
        }
    }
}
