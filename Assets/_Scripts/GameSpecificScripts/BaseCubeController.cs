using UnityEngine;

public class BaseCubeController : MonoBehaviour
{
    private float moveSpeed = 5f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.back, Time.deltaTime * moveSpeed);
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
