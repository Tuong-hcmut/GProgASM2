using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public ArrowManager arrowManager;   // Drag your UIManager (with ArrowManager) here
    public Transform player1Tank;       // Assign in Inspector
    public Transform player2Tank;       // Assign in Inspector

    void Start()
    {
        if (arrowManager == null)
            arrowManager = FindFirstObjectByType<ArrowManager>();

        if (arrowManager != null)
        {
            if (player1Tank != null)
                arrowManager.RegisterTank(player1Tank, Color.blue);

            if (player2Tank != null)
                arrowManager.RegisterTank(player2Tank, Color.red);
        }
        else
        {
            Debug.LogError("ArrowManager not found in scene!");
        }
    }
}
