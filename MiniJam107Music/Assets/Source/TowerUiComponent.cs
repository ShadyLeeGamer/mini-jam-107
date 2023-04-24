using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TowerUiComponent : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private string towerName;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI2;
    private Tower tower;
    private GameObject placeHolderObject;
    private void Awake()
    {
        Image image = GetComponent<Image>();
        this.placeHolderObject = new GameObject();
        this.placeHolderObject.SetActive(false);
        SpriteRenderer spriterenderer = placeHolderObject.AddComponent<SpriteRenderer>();
        spriterenderer.sortingOrder = 100;
        this.tower = shop.Get(towerName);
        Sprite sprite = tower.GetComponent<SpriteRenderer>().sprite;
        textMeshProUGUI.text = towerName;
        textMeshProUGUI2.text = this.tower.price.ToString();
        spriterenderer.sprite = sprite;
        Color originalColor = spriterenderer.color;
        Color color = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a-0.2f);
        spriterenderer.color = color;
        image.sprite = sprite;
    }

    public Tower GetTower()
    {
        return this.tower;
    }

    public GameObject GetPlaceHolderObject()
    {
        return placeHolderObject;
    }
}
