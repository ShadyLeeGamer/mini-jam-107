using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button retryButton;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        text.text = Stats.Instance.alienKillContainer.GetValue().ToString();
        retryButton.onClick.AddListener(OnRetryClick);
    }

    void OnRetryClick()
    {
        gameManager.Play();
    }
}
