using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnEnable()
    {
       GameManager.Instance.OnPlayerSpawned += OnPlayerSpawnedCallback;         
    }
    private void OnDisable()
    {
        GameManager.Instance.OnPlayerSpawned -= OnPlayerSpawnedCallback;
    }
    private void OnPlayerSpawnedCallback(PlayerController controller)
    {
        playerTransform = controller.transform;
        offset = transform.position - playerTransform.position;
    }



    // Update is called once per frame
    void Update()
    {
        if (!playerTransform) return;

        Vector3 position = new Vector3(playerTransform.position.x + offset.x, playerTransform.position.y + offset.y, -10);
        float clampX = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        float clampY = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);

        transform.position = new Vector3(clampX, clampY, -10);
    }
}
