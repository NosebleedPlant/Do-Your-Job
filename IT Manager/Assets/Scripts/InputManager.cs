using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private string activeGame;
    
    [SerializeField] private Storage_SubManager StorageMiniGame;
    [SerializeField] private Network_SubManager NetwrokMiniGame;
    [SerializeField] private Security_SubManager SecurityMiniGame;
    [SerializeField] private GameObject Canvas;

    private bool _held = false;
    private PointerEventData _mouseEventData;
    private GraphicRaycaster _uiRaycaster;
    private List<RaycastResult> _click_results;

    private void Start()
    {
        _mouseEventData = new PointerEventData(EventSystem.current);
        _uiRaycaster = Canvas.GetComponent<GraphicRaycaster>();
        _click_results = new List<RaycastResult>();
    }

    private void Update()
    {
        if(_held)
        {
            GameInputRecived(NetwrokMiniGame.dragWire);
            // Debug.Log("held");
        }

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
        }
        if(Input.GetMouseButton(0))
        {
            Debug.Log("held");
        }
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("released");
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.performed)
        {GameInputRecived(NetwrokMiniGame.checkMaking); _held=true;}
        if(context.canceled)
        {NetwrokMiniGame.ClearConnection(); _held=false;Debug.Log("released");}
    }

    private void GameInputRecived(Action<Vector3> func)
    {
        _mouseEventData.position = Mouse.current.position.ReadValue();
        _click_results.Clear();
        _uiRaycaster.Raycast(_mouseEventData, _click_results);
        foreach(RaycastResult result in _click_results)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(_mouseEventData.position);
            if (result.gameObject.tag == "MinigameWindow")
            {
                func(worldPosition);
            }
        }
    }
}
