using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage_FileBehaviour : MonoBehaviour
{   
    protected Rigidbody2D _rigidBody;
    protected Collider2D _hitbox;
    protected Storage_SubManager _manager;
    protected int _fileInfolder;
    bool childFile = false;

    protected void Awake()
    {
        _rigidBody = transform.GetComponent<Rigidbody2D>();
        _hitbox = GetComponent<Collider2D>();
        _manager = FindObjectOfType<Storage_SubManager>();
        _rigidBody.velocity = _manager.CalculatePrefabVelocity(transform.position,childFile);
        _fileInfolder = _manager._fileInfolder;
        StartCoroutine(ActivateHitbox());
    }
    
    protected IEnumerator ActivateHitbox()
    {
        yield return new WaitForSeconds(0.2f);
        _hitbox.enabled = true;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DeletePrefab();
            return;
        }
        if(other.CompareTag("STMG_Game"))
        {
            Reset();
        }
    }
    
    virtual public void DeletePrefab()
    {
        _manager.HandleFileDeath();
        Destroy(this.gameObject);
    }

    protected void Reset()
    {
        Vector2 position = _manager.CalculatePrefabPoisiton();
        Vector2 velocity = _manager.CalculatePrefabVelocity(position,_manager.InitialTargetArea);
        _rigidBody.velocity = velocity;
        transform.position = position;
    }

    protected void OnDisable()
    {
        StopAllCoroutines();
    }
}
