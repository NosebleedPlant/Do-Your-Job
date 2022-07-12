using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTMG_ButtonOnActivate : MonoBehaviour
{
    [SerializeField] private NTMG_Manager manager;

    private void Start()
    {
        manager = GetComponentInParent<NTMG_Manager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            manager.startGame();
            
        }
    }
}
