using System;
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
    
    [SerializeField] private Storage_SubManager StorageMiniGame;
    [SerializeField] private Network_SubManager NetwrokMiniGame;
    [SerializeField] private Security_SubManager SecurityMiniGame;

        
    public GameObject canvas;
    private GraphicRaycaster uiRaycaster;
    PointerEventData mouseEventData;
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
        mouseEventData = new PointerEventData(EventSystem.current);
        click_results = new List<RaycastResult>();
    }

    private void Update()
    {
        if(playerControls.Normal.MouseClick.triggered)
        {
            getUIElementsClicked(NetwrokMiniGame.checkMaking);
            getUIElementsClicked(StorageMiniGame.movePlayerObject);
        }
        if(playerControls.Normal.MouseClick.inProgress)
        {
            getUIElementsClicked(SecurityMiniGame.movePlayerObject);
            getUIElementsClicked(NetwrokMiniGame.dragWire);
        }
        //if(playerControls.Normal.MouseClick.)
    }

    private void getUIElementsClicked(Action<Vector3> func)
    {
        mouseEventData.position = Mouse.current.position.ReadValue();
        click_results.Clear();
        uiRaycaster.Raycast(mouseEventData, click_results);
        foreach(RaycastResult result in click_results)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseEventData.position);
            if (result.gameObject.tag == "MinigameWindow")
            {
                func(worldPosition);
            }
        }
    }

    // private void AdjustPositionSTM(Vector3 position)
    // {
    //     //update the offset to make sure its correct
    //     Vector3 framePosition = StorageFrame.position;
    //     Vector3 gamePosition = StorageMiniGame.position;
    //     Vector3 stOffsetCord = gamePosition-framePosition;
    //     position += stOffsetCord;
    //     deleteAction(position);
    // }

    // private void AdjustPositionNTM(Vector3 position)
    // {
    //     //update the offset to make sure its correct
    //     Vector3 framePosition = NetwrokFrame.position;
    //     Vector3 gamePosition = NetwrokMiniGame.position;
    //     Vector3 ntOffsetCord = gamePosition-framePosition;
    //     position += ntOffsetCord;
    //     beginDrag(position);
    // }

    // private void deleteAction(Vector2 deleteLocation)
    // {
    //     StoragePlayerObject.transform.position = deleteLocation;
    //     StartCoroutine(enableDeleteTool(0.1f, StoragePlayerObject));
    // }

    // private void beginDrag(Vector2 dragLocation)
    // {
    //     dragToolCollider.transform.position = dragLocation;
    //     StartCoroutine(dragToolClick(0.1f, dragToolCollider));
    // }

    // private IEnumerator enableDeleteTool(float seconds, Collider2D deleteToolCollider)
    // {
    //     deleteToolCollider.enabled = true;
    //     yield return new WaitForSeconds(seconds);
    //     deleteToolCollider.enabled = false;
    // }

    // private IEnumerator dragToolClick(float seconds, Collider2D deleteToolCollider)
    // {
    //     dragToolCollider.enabled = true;
    //     yield return new WaitForSeconds(seconds);
    //     dragToolCollider.enabled = false;
    // }
}
