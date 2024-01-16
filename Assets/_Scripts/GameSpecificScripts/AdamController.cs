using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using System;
using System.Linq;
using System.Collections;

public class AdamController : MonoBehaviour
{
    public GameObject hitParticle;
    public bool canMoveForward;
    public float forwardSpeed = 4f;
    public bool canHitPlayer = true;

    public float randomPosX;
    private float clampValue;
    private float startPointZ;
    private GameObject target;

    private SpawnAdamController spawnAdamController;
    //private ItemController itemController;
    private new CapsuleCollider collider;
    private Animator adamAnimator;
    private Vector3 targetPos;

    [Header("RAGDOLL PARTS"), Space(5f)]
    public List<Rigidbody> adamRagdollRigidbodies;
    public List<Collider> adamRagdollColliders;


    public List<ItemController> giftsAndFlowers;
    public bool isThisStaticAdam;

    private void Start()
    {
        //itemController = FindObjectOfType<ItemController>();
        target = GameObject.FindGameObjectWithTag(Tags.Player);
        adamAnimator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        spawnAdamController = FindObjectOfType<SpawnAdamController>();
        startPointZ = transform.position.z;

        //itemController.RandomlySpawnItems();
        ClaimRandomXPos();
        CacheRagdollColliders();
        CacheRagdollRigidbodies();
        AddComponentToRagdollObjects(typeof(AdamRagdollColliderController));
        SetBipedIKTargets();
        TurnOffRagdoll();

        if (isThisStaticAdam)
        {
            RandomizeGiftsAndFlowers();
        }
    }

    private void ClaimRandomXPos()
    {
        randomPosX = Random.Range(-0.15f, 0.15f);
    }

    private void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (!canMoveForward)
        {
            return;
        }

        clampValue = Mathf.Lerp(spawnAdamController.posXRange, 3.5f, (startPointZ - transform.position.z) / startPointZ);
        var posXValue = Mathf.Clamp(transform.position.x - randomPosX, -clampValue, clampValue);
        transform.position = Vector3.Lerp(transform.position, new Vector3(posXValue, transform.position.y, transform.position.z - 1f), Time.deltaTime * forwardSpeed);
        transform.LookAt(new Vector3(posXValue, transform.position.y, transform.position.z - 1f));
    }

    private void SetBipedIKTargets()
    {
        gameObject.GetComponent<BipedIK>().solvers.leftHand.target = GameObject.FindGameObjectWithTag(Tags.AdamIKTarget).transform;
        gameObject.GetComponent<BipedIK>().solvers.rightHand.target = GameObject.FindGameObjectWithTag(Tags.AdamIKTarget).transform;
        gameObject.GetComponent<BipedIK>().solvers.lookAt.target = GameObject.FindGameObjectWithTag(Tags.AdamIKTarget).transform;
    }

    private void CacheRagdollColliders()
    {
        var colliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                adamRagdollColliders.Add(collider);
            }
        }
    }

    private void CacheRagdollRigidbodies()
    {
        var rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();

        foreach (var rigidbody in rigidbodies)
        {
            if (rigidbody.gameObject != gameObject)
            {
                adamRagdollRigidbodies.Add(rigidbody);
            }
        }
    }

    private void AddComponentToRagdollObjects(Type component)
    {
        foreach (var ragdollCollider in adamRagdollColliders)
        {
            var go = ragdollCollider.gameObject;
            go.AddComponent(component);
            go.GetComponent<AdamRagdollColliderController>().SetHitParticle(hitParticle);
        }
    }

    private void TurnOnRagdoll()
    {
        adamAnimator.enabled = false;
        collider.enabled = false;
        TurnOnRagdollCollidersAndRigidbodies();
    }

    private void TurnOffRagdoll()
    {
        adamAnimator.enabled = true;
        collider.enabled = true;
        TurnOffRagdollCollidersAndRigidbodies();
    }

    public void TurnOffRagdollCollidersAndRigidbodies()
    {
        for (int i = 0; i < adamRagdollColliders.Count; i++)
        {
            adamRagdollColliders[i].isTrigger = true;
            adamRagdollRigidbodies[i].isKinematic = true;
        }
    }

    private void TurnOnRagdollCollidersAndRigidbodies()
    {
        for (int i = 0; i < adamRagdollColliders.Count; i++)
        {
            adamRagdollColliders[i].isTrigger = false;
            adamRagdollRigidbodies[i].isKinematic = false;
        }
    }

    public void RandomizeGiftsAndFlowers()
    {
        giftsAndFlowers = new List<ItemController>();
        giftsAndFlowers = GetComponentsInChildren<ItemController>().ToList();

        int index = Random.Range(0, giftsAndFlowers.Count);

        giftsAndFlowers[index].gameObject.SetActive(false);

        if (giftsAndFlowers[index].isThisFlower)
        {
            gameObject.GetComponent<BipedIK>().solvers.leftHand.IKPositionWeight = 0f;
        }
        else
        {
            gameObject.GetComponent<BipedIK>().solvers.rightHand.IKPositionWeight = 0f;
        }
    }

    public void DropGiftsAndFlowers()
    {
        if (gameObject.CompareTag(Tags.TutorialAdam))
        {
            Time.timeScale = 1f;
        }
        for (int i = 0; i < giftsAndFlowers.Count; i++)
        {
            if (giftsAndFlowers[i].gameObject.activeSelf)
            {
                giftsAndFlowers[i].ItemFall();
            }
        }
    }

    public IEnumerator AdamStunAnimation()
    {
        forwardSpeed = 0f;
        adamAnimator.SetTrigger(AdamAnimTriggers.AdamAnimsStun);
        yield return new WaitForSeconds(0.8f);
        forwardSpeed = 4f;
        gameObject.GetComponent<BipedIK>().solvers.leftHand.IKPositionWeight = 0f;
        gameObject.GetComponent<BipedIK>().solvers.rightHand.IKPositionWeight = 0f;
    }
}
