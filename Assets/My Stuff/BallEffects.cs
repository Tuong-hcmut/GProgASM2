using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class BallEffects : MonoBehaviour
{
    [Header("Bounds")]
    [SerializeField] public Collider2D playAreaCollider;

    [Header("Effects")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private ParticleSystem hitParticles;

    [Header("Settings")]
    [SerializeField] private float minVelocityToTrigger = 0.1f;
    [SerializeField] private float volumeMultiplier = 0.5f;
    [SerializeField] private float propelForce = 10f;
    [SerializeField] private int hitThreshold = 10;
    [SerializeField] private float safeInset = 0.5f;  // How far inward to teleport if out of bounds

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private static int aiHitCount = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        if (hitParticles != null)
            hitParticles.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactStrength = rb.linearVelocity.magnitude;

        if (impactStrength > minVelocityToTrigger)
        {
            // --- Effects ---
            if (hitSound != null)
                audioSource.PlayOneShot(hitSound, Mathf.Clamp01(impactStrength * volumeMultiplier));

            if (hitParticles != null)
            {
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    hitParticles.transform.position = contact.point;
                    hitParticles.transform.rotation = Quaternion.identity;
                    hitParticles.Play();
                }
            }
        }

        // --- AI Hit Tracking ---
        if (collision.collider.CompareTag("AI1") || collision.collider.CompareTag("AI2"))
        {
            aiHitCount++;
            if (aiHitCount >= hitThreshold)
            {
                aiHitCount = 0;
                PropelAllAIs();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Only act when leaving the play area
        if (other == playAreaCollider)
        {
            // Efficiency check: only correct when moving slowly (not in mid-boost)
            if (rb.linearVelocity.magnitude < 10f)
                RepositionInsidePlayArea();
        }
    }

    private void RepositionInsidePlayArea()
    {
        if (playAreaCollider == null)
            return;

        Vector2 center = playAreaCollider.bounds.center;
        Vector2 fromPos = transform.position;
        Vector2 direction = (center - fromPos).normalized;

        // Cast from the ball’s position toward the center to find the play area boundary
        RaycastHit2D hit = Physics2D.Raycast(fromPos, direction, Mathf.Infinity, LayerMask.GetMask("PlayArea"));

        if (hit.collider != null)
        {
            Vector2 target = hit.point + direction * safeInset;
            transform.position = target;
            rb.linearVelocity = Vector2.zero;
            Debug.Log($"Ball repositioned inside play area at {target}");
        }
        else
        {
            // Fallback if no hit detected
            transform.position = center;
            rb.linearVelocity = Vector2.zero;
            Debug.LogWarning("No intersection found — moved ball to play area center.");
        }
    }

    private void PropelAllAIs()
    {
        // Combine both AI1 and AI2 tagged objects
        List<GameObject> aiObjects = new List<GameObject>();
        aiObjects.AddRange(GameObject.FindGameObjectsWithTag("AI1"));
        aiObjects.AddRange(GameObject.FindGameObjectsWithTag("AI2"));

        foreach (GameObject ai in aiObjects)
        {
            Rigidbody2D aiRb = ai.GetComponent<Rigidbody2D>();
            if (aiRb == null) continue;

            Vector2 direction = (ai.transform.position - transform.position).normalized;
            aiRb.AddForce(direction * propelForce, ForceMode2D.Impulse);
        }

        Debug.Log($"Propelled {aiObjects.Count} AI objects (AI1 + AI2) away from the ball!");
    }
}
