using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Charge : MonoBehaviour
{
    public int chargePoint;
    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCountText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = chargePoint.ToString();
        }
    }


    public void AddChargePoint()
    {
        chargePoint = chargePoint + 1;
        UpdateCountText();
    }
}
