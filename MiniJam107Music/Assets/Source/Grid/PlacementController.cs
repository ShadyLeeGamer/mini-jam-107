using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlacementController : MonoBehaviour
{
    public static PlacementController Instance;
    private TowerUiComponent towerContainer;
    private GameObject placeHolder;
    [SerializeField] private Shop shop;
    [SerializeField] private CurrencyContainer currencyContainer;
    [SerializeField] private AudioClip placementClip;
    [SerializeField] private AudioClip failedPlacementClip;
    private AudioSource audioSource;
    public Color unoccupied;
    public Color occupied;
    [SerializeField] private GameObject gridPlacmentOverlay;
    public static Grid<GameObject> grid;
    public const int CELL_SIZE = 1;
    //private Store store;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        grid = new Grid<GameObject>(6, 5, CELL_SIZE, new Vector3(-0.25f, -0.25f), (Grid<GameObject> g, int x, int y) => null);
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void OnButtonClick()
    {
        this.towerContainer = EventSystem.current.currentSelectedGameObject.GetComponent<TowerUiComponent>();
        placeHolder = Instantiate(towerContainer.GetPlaceHolderObject());
        placeHolder.SetActive(true);
    }

    public void Update()
    {
        MovePlaceHolder();
        PlaceTowerOnPlaceHolder();
        DestroyPlaceHolder();
    }

    private void PlaceTowerOnPlaceHolder()
    {
        Vector2 snappedPlacementPosition = grid.SnapToGridLocation((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButtonDown(0) && placeHolder != null)
        {
            try
            {
                if (grid.GetGridObject(snappedPlacementPosition) != null)
                {
                    audioSource.PlayOneShot(failedPlacementClip);
                    return;
                }
                GameObject tower = Instantiate(shop.Buy(towerContainer.GetTower().name, currencyContainer));
                tower.transform.position = snappedPlacementPosition;
                grid.SetGridObject(snappedPlacementPosition, tower);
                audioSource.PlayOneShot(placementClip);
                Deselect();
            }
            catch (Exception)
            {
                audioSource.PlayOneShot(failedPlacementClip);
            }
        }
    }

    private void MovePlaceHolder()
    {
        if (towerContainer == null) return;
        try
        {
            Vector2 snappedPlacementPosition = grid.SnapToGridLocation((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
            GameObject gameObject = grid.GetGridObject(snappedPlacementPosition);
            gridPlacmentOverlay.SetActive(true);
            SpriteRenderer spriteRenderer = gridPlacmentOverlay.GetComponent<SpriteRenderer>();
            if(gameObject != null) spriteRenderer.color = occupied;
            else if(currencyContainer.GetValue() < towerContainer.GetTower().price) spriteRenderer.color = occupied;
            else spriteRenderer.color = unoccupied;
            gridPlacmentOverlay.transform.position = snappedPlacementPosition;
        }
        catch (IllegalGridPlacmentException)
        {
            gridPlacmentOverlay.SetActive(false);
        }
        placeHolder.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void DestroyPlaceHolder()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Deselect();
        }
    }

    private void Deselect()
    {
        towerContainer = null;
        gridPlacmentOverlay.SetActive(false);
        Destroy(placeHolder);
    }
}
