using UnityEngine;
using TMPro;

public class Goal_Zone : MonoBehaviour
{
    public Transform ball;
    public Transform player1;
    public Transform player2;

    public Vector3 ballStartPos;
    public Vector3 player1StartPos;
    public Vector3 player2StartPos;

    public TextMeshProUGUI goalText; // Drag UI Text here in Inspector

    [Header("Who Scores?")]
    public int scoringPlayerNumber = 1; // 1 or 2 depending on which goal this is

    void Start()
    {
        ballStartPos = ball.position;
        player1StartPos = player1.position;
        player2StartPos = player2.position;

        if (goalText != null)
            goalText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            // Call GameManager to add score
            GameManager.Instance?.AddScore(scoringPlayerNumber);

            ShowGoalText();   // Show "GOAL" UI
            ResetPositions();
        }
    }

    void ShowGoalText()
    {
        if (goalText != null)
        {
            goalText.text = "GOALLL!!!";
            goalText.gameObject.SetActive(true);

            // Hide after 1.3 seconds
            Invoke(nameof(HideGoalText), 1.3f);
        }
    }

    void HideGoalText()
    {
        if (goalText != null)
            goalText.gameObject.SetActive(false);
    }

    void ResetPositions()
    {
        // Reset ball
        ball.position = ballStartPos;
        Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
        if (rbBall != null)
        {
            rbBall.linearVelocity = Vector2.zero;
            rbBall.angularVelocity = 0f;
        }

        // Reset player 1
        player1.position = player1StartPos;
        Rigidbody2D rb1 = player1.GetComponent<Rigidbody2D>();
        if (rb1 != null)
        {
            rb1.linearVelocity = Vector2.zero;
            rb1.angularVelocity = 0f;
        }

        // Reset player 2
        player2.position = player2StartPos;
        Rigidbody2D rb2 = player2.GetComponent<Rigidbody2D>();
        if (rb2 != null)
        {
            rb2.linearVelocity = Vector2.zero;
            rb2.angularVelocity = 0f;
        }
    }
}