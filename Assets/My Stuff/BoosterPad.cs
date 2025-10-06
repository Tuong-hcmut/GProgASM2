using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BoosterPad : MonoBehaviour
{
    [Header("Boost Settings")]
    [SerializeField] private float boostForce = 7f;          // user-defined: strength of boost
    [SerializeField] private Vector2 localBoostDirection = Vector2.up; // user-defined: direction in local space
    [SerializeField] private bool affectOnlyRigidbody = true; // user-defined: only apply to objects with Rigidbody2D

    private void Awake()
    {

        // Ensure the collider is a trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (affectOnlyRigidbody && rb == null) return;

        // Convert local direction to world space
        Vector2 worldDirection = transform.TransformDirection(localBoostDirection.normalized);

        // Apply boost
        if (rb != null)
        {
            rb.AddForce(worldDirection * boostForce, ForceMode2D.Impulse);
            Debug.Log($"BoosterPad boosted {other.name} with force {boostForce} toward {worldDirection}");
        }
    }
}
