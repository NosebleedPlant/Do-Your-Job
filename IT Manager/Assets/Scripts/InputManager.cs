using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameManager mainManager;
    [SerializeField] private Storage_SubManager StorageMiniGame;
    [SerializeField] private Network_SubManager NetwrokMiniGame;
    [SerializeField] private Security_SubManager SecurityMiniGame;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private Transform InGameCursor;
    private InputActions playerControls;
    
    private PointerEventData _mouseEventData;
    private GraphicRaycaster _uiRaycaster;
    private List<RaycastResult> _click_results;

    private void Start()
    {
        Cursor.visible = false;
        _mouseEventData = new PointerEventData(EventSystem.current);
        _uiRaycaster = Canvas.GetComponent<GraphicRaycaster>();
        _click_results = new List<RaycastResult>();
    }

    public void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        InGameCursor.position = new Vector3(mousePosition.x,mousePosition.y,0f);
        
        if(Input.GetMouseButtonDown(0))
        {
            GameInputRecived(NetwrokMiniGame.checkMaking);
            GameInputRecived(StorageMiniGame.movePlayerObject);
            GameInputRecived(SecurityMiniGame.activateTrigger);
        }
        if(Input.GetMouseButton(0))
        {
            GameInputRecived(NetwrokMiniGame.dragWire);
        }
        if(Input.GetMouseButtonUp(0))
        {
            NetwrokMiniGame.ClearConnection();
        }
    }

    private void GameInputRecived(Action<Vector3> func)
    {
        _mouseEventData.position = Mouse.current.position.ReadValue();
        _click_results.Clear();
        _uiRaycaster.Raycast(_mouseEventData, _click_results);
        if(_click_results!=null)
        {
            if (_click_results[0].gameObject.CompareTag("MinigameWindow"))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(_mouseEventData.position);
                func(worldPosition);
            }
        }
    }
}
