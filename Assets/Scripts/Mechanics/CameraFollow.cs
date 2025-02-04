using JetBrains.Annotations;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -10);
        float clampX = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        float clampY = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);

        transform.position = new Vector3(clampX, clampY, -10);
    }
}
