using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject particleEffect; // Assign your particle effect prefab here
    void OnTriggerEnter2D(Collider2D other)
    {
         //Check if the object colliding with the trigger has a specific tag
        if (other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }    
    }

    void OnMouseDown()
    {
        Instantiate(particleEffect, transform.position, Quaternion.EulerRotation(1,0f,0f));
        Destroy(gameObject);
    }

}


