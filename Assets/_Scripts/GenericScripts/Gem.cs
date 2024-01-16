using UnityEngine;

public class Gem : MonoBehaviour
{
    private PlayerController player;
    private float moveSpeed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.PlayerRagdollCollider) && !player.isAlreadyPushed)
        {
            Destroy(gameObject);
            GameManager.instance.CollectGem(transform.position, 1);
            if (gameObject.tag == Tags.LastGem)
            {
                GameManager.instance.LevelComplete();
            }
        }
    }
}
