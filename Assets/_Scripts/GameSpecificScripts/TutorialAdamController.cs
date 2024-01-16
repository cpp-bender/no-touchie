using RootMotion.FinalIK;
using UnityEngine;

public class TutorialAdamController : MonoBehaviour
{
    private PlayerController player;
    private AdamController adamController;
    private Animator animator;
    private BipedIK bipedIK;
    private float moveSpeed = 5f;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        bipedIK = GetComponent<BipedIK>();
        adamController = GetComponent<AdamController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        bipedIK.enabled = false;
        adamController.canMoveForward = false;
    }


    public void Initialize()
    {
        bipedIK.enabled = false;
        adamController.canMoveForward = true;
        animator.SetTrigger(AdamAnimTriggers.AdamRunAnims);
        adamController.randomPosX = 0f;
    }
}
