using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActions playerControls;
    [SerializeField] private Collider2D deleteToolCollider;
    [SerializeField] private Collider2D dragToolCollider;
    [SerializeField] private string activeGame;
    [SerializeField] public Vector3 stOffsetCord;
    [SerializeField] public Vector3 ntOffsetCord;
    [SerializeField] public Vector3 scOffsetCord;
    [SerializeField] public Vector3 csOffsetCord;
    
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

        stOffsetCord = new Vector3(-50, 0, 0);
        ntOffsetCord = new Vector3(50, 0, 0);
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
                    worldPosition += stOffsetCord;
                    deleteAction(worldPosition);
                }
                else if (activeGame == "nt")
                {
                    worldPosition += ntOffsetCord;
                    beginDrag(worldPosition);
                }
                
            }
        }
    }

    private void deleteAction(Vector2 deleteLocation)
    {
        deleteToolCollider.transform.position = deleteLocation;
        StartCoroutine(enableDeleteTool(0.1f, deleteToolCollider));
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
