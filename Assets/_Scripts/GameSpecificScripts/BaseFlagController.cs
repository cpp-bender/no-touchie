using UnityEngine;

public class BaseFlagController : MonoBehaviour
{
    public Cloth clothObject;

    protected float moveSpeed = 5f;

    protected PlayerController player;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerController>();
        SetClothColliders();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

    }

    private void SetClothColliders()
    {
        clothObject.capsuleColliders = player.clothColliders.ToArray();
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
