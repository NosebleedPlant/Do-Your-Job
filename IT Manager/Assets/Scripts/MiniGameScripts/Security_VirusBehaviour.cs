using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_VirusBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Collider2D _hitbox;
    private Security_SubManager _manager;

    private void Awake()
    {
        _manager = FindObjectOfType<Security_SubManager>();
        _rigidBody = transform.GetComponent<Rigidbody2D>();
        _rigidBody.velocity = Vector2.down*6f;
    }

    private void OnDisable() => StopAllCoroutines();

    private void OnCollisionEnter2D(Collision2D collsiion)
    {
        if (collsiion.transform.CompareTag("Player"))
        {
            _rigidBody.velocity = new Vector2(0,Mathf.Abs(3f));
            StartCoroutine(DeleteVirus());
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("SCMG_PlayArea"))
        {
            StartCoroutine(DeleteVirus());
        }
    }

    private IEnumerator DeleteVirus()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
