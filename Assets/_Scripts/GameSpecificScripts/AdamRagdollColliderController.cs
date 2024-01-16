using NUnit.Framework;
using UnityEngine;

public class AdamRagdollColliderController : MonoBehaviour
{
    private AdamController adamController;
    private PlayerController player;
    private Animator adamAnimator;
    private GameObject hitParticle;

    private const int counterMax = 10;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
        SetParams();
    }

    private void SetParams()
    {
        //Fills Adam Animator and Adam Controlller objects

        int iter = 0;

        var currentParent = transform.parent;

        while (true)
        {
            if (iter == counterMax)
            {
                Debug.Log("Params not found");
            }

            if (currentParent.CompareTag(Tags.CloneAdam))
            {
                adamAnimator = currentParent.GetComponent<Animator>();
                adamController = currentParent.GetComponent<AdamController>();
                break;
            }
            if (currentParent.CompareTag(Tags.TutorialAdam))
            {
                adamAnimator = currentParent.GetComponent<Animator>();
                adamController = currentParent.GetComponent<AdamController>();
                break;
            }
            currentParent = currentParent.parent;
            iter++;
        }
    }

    public void SetHitParticle(GameObject particle)
    {
        hitParticle = particle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerRagdollCollider) && !player.isAlreadyPushed && adamController.canHitPlayer)
        {
            if (player.hitCount < 2)
            {
                Instantiate(hitParticle, other.gameObject.transform.position, Quaternion.identity);
                player.hitCount++;
                adamController.canHitPlayer = false;
                StartCoroutine(adamController.AdamStunAnimation());
                adamController.DropGiftsAndFlowers();
                UIManager.instance.LoseHealthUI();
            }

            else if (player.hitCount >= 2)
            {
                Instantiate(hitParticle, other.gameObject.transform.position, Quaternion.identity);
                adamAnimator.SetTrigger(AdamAnimTriggers.AdamAnimsDance);
                adamController.canMoveForward = false;
                player.isAlreadyPushed = true;
                StartCoroutine(adamController.AdamStunAnimation());
                adamController.DropGiftsAndFlowers();
                player.TurnOnRagdoll();
                player.DoRagdollForce();
                UIManager.instance.LoseHealthUI();
                GameManager.instance.LevelFail();
            }
        }
    }
}
