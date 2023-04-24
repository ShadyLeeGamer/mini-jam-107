using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private CurrencyContainer currencyContainer;
    [SerializeField] private TextMeshProUGUI currencyText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        currencyContainer.OnChange += UpdateUI;
    }


    void UpdateUI()
    {
        currencyText.text = currencyContainer.GetValue().ToString();
    }
}
