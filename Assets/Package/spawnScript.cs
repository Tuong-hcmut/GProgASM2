using UnityEngine;

public class spawnScript : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    public float minSpeed = 5f;
    public float maxSpeed = 10f;
    public float curveAmount = 1f;
    public float TimeBetweenObjects;
    public Vector2 direction = Vector2.up; // Default direction

    void Start()
    {
        InvokeRepeating("SpawnObject", 1f,TimeBetweenObjects); // Spawns an object every 2 seconds
    }

    void SpawnObject()
    {
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

        float speed = Random.Range(minSpeed, maxSpeed);
        Vector2 curvedDirection = direction + new Vector2(0, Random.Range(-curveAmount, curveAmount));

        rb.linearVelocity = curvedDirection.normalized * speed;
    }
}





