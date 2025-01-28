using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Range(0.01f, 1.0f)]
    private Vector2 groundCheckSize = new Vector2(0.8f, 0.06f);
    public LayerMask isGroundLayer;
    private Transform groundCheck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //groundCheck init
        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform); //sets the position to the parents position
        newGameObject.transform.localPosition = Vector3.zero;
        newGameObject.name = "GroundCheck";
        groundCheck = newGameObject.transform;
    }
    public bool isGrounded()
    {
        if (!groundCheck) return false;
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, isGroundLayer);
    }
}