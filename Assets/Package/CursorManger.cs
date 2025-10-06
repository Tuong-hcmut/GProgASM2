using UnityEngine;

public class CursorManger : MonoBehaviour
{
    [SerializeField] private Texture2D[] ArraycursorTex;
    private int currentFrame;
    [SerializeField] private int frameCount;
    private float frameTimer;
    [SerializeField] private float frameRate;
    private void Start()
    {
        Cursor.SetCursor(ArraycursorTex[0], new Vector2(0,0),CursorMode.Auto);
    }

    // Update is called once per frame
    private void Update()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            frameTimer += frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(ArraycursorTex[currentFrame], new Vector2(0, 0), CursorMode.Auto);
        }
    }
}
