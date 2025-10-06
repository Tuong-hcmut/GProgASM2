using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

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
    private Dictionary<GameObject, Vector3> aiStartPositions = new Dictionary<GameObject, Vector3>();

    void Start()
    {
        ballStartPos = ball.position;
        player1StartPos = player1.position;
        player2StartPos = player2.position;


        GameObject[] allAIs = GameObject.FindGameObjectsWithTag("AI");
        foreach (var ai in allAIs)
        {
            aiStartPositions[ai] = ai.transform.position;
        }

        if (goalText != null)
            goalText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.Instance?.AddScore(scoringPlayerNumber);
            ShowGoalText();
            StartCoroutine(ResetPositionsAfterGoal());
        }
    }

    IEnumerator ResetPositionsAfterGoal()
    {
        ResetPositions();


        GameObject[] allAIs = GameObject.FindGameObjectsWithTag("AI");
        foreach (var ai in allAIs)
        {
            AIController aiCtrl = ai.GetComponent<AIController>();
            if (aiCtrl != null)
                aiCtrl.enabled = false;
        }

        yield return new WaitForSeconds(2f); // đợi 2 giây


        foreach (var ai in allAIs)
        {
            AIController aiCtrl = ai.GetComponent<AIController>();
            if (aiCtrl != null)
                aiCtrl.enabled = true;
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
        ball.position = ballStartPos;
        Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
        if (rbBall != null)
        {
            rbBall.linearVelocity = Vector2.zero;
            rbBall.angularVelocity = 0f;
        }

        ResetObject(player1, player1StartPos);
        ResetObject(player2, player2StartPos);


        foreach (var kvp in aiStartPositions)
        {
            if (kvp.Key != null)
                ResetObject(kvp.Key.transform, kvp.Value);
        }
    }

    void ResetObject(Transform obj, Vector3 startPos)
    {
        obj.position = startPos;
        obj.rotation = Quaternion.identity;

        var rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
