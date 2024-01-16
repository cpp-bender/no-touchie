using RootMotion.FinalIK;
using UnityEngine;

public class StaticAdamController : MonoBehaviour
{
    private CharacterMovement characterMovement;

    private void Start()
    {
        SetStaticAdamPosition();
        characterMovement = FindObjectOfType<CharacterMovement>();    
    }

    private void Update()
    {
        if (characterMovement.isGameStarted)
        {
            StaticAdamRunning();
        }
    }

    private void SetStaticAdamPosition()
    {
        gameObject.transform.position = new Vector3(Random.Range(-6f, 6f), gameObject.transform.position.y, Random.Range(25f, 100f));
    }

    public void StaticAdamRunning()
    {
        var adamController = gameObject.GetComponent<AdamController>();

        if (adamController.canMoveForward == false)
        {
            adamController.canMoveForward = true;
            gameObject.GetComponent<BipedIK>().enabled = true;
            gameObject.GetComponent<Animator>().SetTrigger(AdamAnimTriggers.AdamRunAnims);
        }
    }
}
