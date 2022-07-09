using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STMG_Spawner : MonoBehaviour
{
    [SerializeField] private Collider2D[] spawnArea;

    [SerializeField] private GameObject[] filePrefabs;

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject initialTarget;

    public float minSpawnDelay = 0.5f;
    public float maxSpawnDelay = 2.0f;

    public float minVelocity = 8f;
    public float maxVelocity = 9f;

    public float lifespan = 5.0f;

    private void Awake()
    {
        spawnArea = GetComponents<Collider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);

        while(enabled)
        {
            GameObject filePrefab = filePrefabs[Random.Range(0, filePrefabs.Length)];
            Collider2D spawnLocation = spawnArea[Random.Range(0, spawnArea.Length)];

            Vector2 position = new Vector2();
            position.x = Random.Range(spawnLocation.bounds.min.x, spawnLocation.bounds.max.x);
            position.y = Random.Range(spawnLocation.bounds.min.y, spawnLocation.bounds.max.y);

            Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
            GameObject file = Instantiate(filePrefab, position, rotation);

            Destroy(file, lifespan);

            float speed = Random.Range(minVelocity, maxVelocity);

            // file.GetComponent<Rigidbody2D>().velocity = (0, velocity)

            Collider2D initialTargetArea = initialTarget.GetComponent<Collider2D>();
            Vector3 initialTargetDirection = new Vector3();

            initialTargetDirection.x = Random.Range(initialTargetArea.bounds.min.x, initialTargetArea.bounds.max.x);
            initialTargetDirection.y = Random.Range(initialTargetArea.bounds.min.y, initialTargetArea.bounds.max.y);
            initialTargetDirection.z = 0;
            
            Vector2 direction = initialTargetDirection - file.transform.position;
            Vector2 fileVelocity = direction.normalized * speed;
            
            file.GetComponent<Rigidbody2D>().velocity = fileVelocity;


            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
