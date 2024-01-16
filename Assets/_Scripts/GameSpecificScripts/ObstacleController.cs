using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private bool playerAlreadyHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player) && !playerAlreadyHit)
        {
            playerAlreadyHit = true;
            var player = other.gameObject.GetComponent<PlayerController>();
            player.TurnOnRagdoll();
            player.DoRagdollForce();
            GameManager.instance.LevelFail();
        }
    }
}
