using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActions playerControls;
    [SerializeField] private string activeGame;
    
    [SerializeField] private Transform StorageFrame;
    [SerializeField] private Transform StorageMiniGame;
    [SerializeField] private Collider2D StoragePlayerObject;

    [SerializeField] private Transform NetwrokFrame;
    [SerializeField] private Transform NetwrokMiniGame;
    [SerializeField] private Collider2D dragToolCollider;

    [SerializeField] private Transform SecurityFrame;
    [SerializeField] private Transform SecurityMiniGame;
    [SerializeField] private Transform SecurityPlayerObject;

        
    public GameObject canvas;
    private GraphicRaycaster uiRaycaster;
    PointerEventData click_data;
    List<RaycastResult> click_results;

    private void Awake()
    {
        playerControls = new InputActions();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        StopAllCoroutines();
    }

    private void Start()
    {
        uiRaycaster = canvas.GetComponent<GraphicRaycaster>();
        click_data = new PointerEventData(EventSystem.current);
        click_results = new List<RaycastResult>();
    }

    private void Update()
    {
        if (playerControls.Normal.MouseClick.triggered)
        {
            getUiElementsClicked();
        }

    }

    private void getUiElementsClicked()
    {
        click_data.position = Mouse.current.position.ReadValue();
        click_results.Clear();
        
        uiRaycaster.Raycast(click_data, click_results);

        foreach(RaycastResult result in click_results)
        {
            if (result.gameObject.tag == "MinigameWindow")
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(click_data.position);
                if (activeGame == "st")
                {
                    //update the offset to make sure its correct
                    Vector3 framePosition = StorageFrame.position;
                    Vector3 gamePosition = StorageMiniGame.position;
                    Vector3 stOffsetCord = gamePosition-framePosition;
                    worldPosition += stOffsetCord;
                    deleteAction(worldPosition);
                }
                else if (activeGame == "nt")
                {
                    Vector3 framePosition = NetwrokFrame.position;
                    Vector3 gamePosition = NetwrokMiniGame.position;
                    Vector3 ntOffsetCord = gamePosition-framePosition;
                    worldPosition += ntOffsetCord;
                    beginDrag(worldPosition);
                }
                
            }
        }
    }

    private void deleteAction(Vector2 deleteLocation)
    {
        StoragePlayerObject.transform.position = deleteLocation;
        StartCoroutine(enableDeleteTool(0.1f, StoragePlayerObject));
    }

    private void beginDrag(Vector2 dragLocation)
    {
        dragToolCollider.transform.position = dragLocation;
        StartCoroutine(dragToolClick(0.1f, dragToolCollider));
    }

    private IEnumerator enableDeleteTool(float seconds, Collider2D deleteToolCollider)
    {
        deleteToolCollider.enabled = true;
        yield return new WaitForSeconds(seconds);
        deleteToolCollider.enabled = false;
    }

    private IEnumerator dragToolClick(float seconds, Collider2D deleteToolCollider)
    {
        dragToolCollider.enabled = true;
        yield return new WaitForSeconds(seconds);
        dragToolCollider.enabled = false;
    }
}
