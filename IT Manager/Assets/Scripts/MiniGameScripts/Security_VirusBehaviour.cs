using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_VirusBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Collider2D _hitbox;
    private Security_SubManager _manager;
    private int counter = 0;

    private void Awake()
    {
        _rigidBody = transform.GetComponent<Rigidbody2D>();
        _manager = FindObjectOfType<Security_SubManager>();
        _rigidBody.velocity = Vector2.down*6f;
    }

    private void Update()
    {
        counter++;
    }

    private void OnDisable() => StopAllCoroutines();

    private void OnCollisionEnter2D(Collision2D collsiion)
    {
        if (collsiion.transform.CompareTag("Player"))
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x,-(_rigidBody.velocity.y));
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("SCMG_LossArea"))
        {
            _manager._elapsedTime=0;
        }
        
        if(other.CompareTag("SCMG_PlayArea"))
        {   
            Destroy(this.gameObject);
        }
    }
}
