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
    [SerializeField] private string activeGame;
    
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
                    worldPosition.x -= 50;
                }
                deleteAction(worldPosition);
            }
        }
    }

    private void deleteAction(Vector2 deleteLocation)
    {
        deleteToolCollider.transform.position = deleteLocation;
        StartCoroutine(enableDeleteTool(0.1f, deleteToolCollider));
    }

    private IEnumerator enableDeleteTool(float seconds, Collider2D deleteToolCollider)
    {
        deleteToolCollider.enabled = true;
        yield return new WaitForSeconds(seconds);
        deleteToolCollider.enabled = false;
    }
}
