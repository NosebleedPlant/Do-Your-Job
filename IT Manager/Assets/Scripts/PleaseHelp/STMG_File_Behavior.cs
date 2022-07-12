using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STMG_File_Behavior : MonoBehaviour
{   
    [SerializeField] private Collider2D hitbox;
    [SerializeField] private GameObject[] filePrefab;
    [SerializeField] public GameObject secondaryTarget;
    [SerializeField] private int minFiles;
    [SerializeField] private int maxFiles;
    [SerializeField] private float lifespan;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float minVelocity;
    
    private void Start()
    {
        hitbox = GetComponent<Collider2D>();
        StartCoroutine(activateHitbox());
    }

    private IEnumerator activateHitbox()
    {
        yield return new WaitForSeconds(0.2f);
        hitbox.enabled = true;
    }
    
    private void deleteFile()
    {
        // this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject);
    }
    private void deleteFolder()
    {
        StartCoroutine(spawnFiles());
        // this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
    }
    private IEnumerator spawnFiles()
    {
        yield return new WaitForSeconds(0);

        secondaryTarget = GameObject.Find("STMG_Secondary_Target");

        Vector2 spawnLocation = this.gameObject.transform.position;
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        
        Collider2D secondaryTargetArea = secondaryTarget.GetComponent<Collider2D>();

        for (int i = 0; i < Random.Range(minFiles, maxFiles); i++)
        {
            GameObject file = Instantiate(filePrefab[Random.Range(0, 3)], spawnLocation, rotation);
            Destroy(file, lifespan);

            Vector3 secondaryTargetDirection = new Vector3();
            secondaryTargetDirection.x = Random.Range(secondaryTargetArea.bounds.min.x, secondaryTargetArea.bounds.max.x);
            secondaryTargetDirection.y = Random.Range(secondaryTargetArea.bounds.min.y, secondaryTargetArea.bounds.max.y);
            secondaryTargetDirection.z = 0;

            float speed = Random.Range(minVelocity, maxVelocity);
            Vector2 direction = secondaryTargetDirection - file.transform.position;
            Vector2 fileVelocity = direction.normalized * speed;

            file.GetComponent<Rigidbody2D>().velocity = fileVelocity;
        }
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && this.CompareTag("STMG_File"))
        {
            deleteFile();
        }
        else if (other.CompareTag("Player") && this.CompareTag("STMG_Folder"))
        {
            deleteFolder();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
