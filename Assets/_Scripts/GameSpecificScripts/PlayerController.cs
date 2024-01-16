using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System;
using RootMotion.FinalIK;

public class PlayerController : MonoBehaviour
{
    public DodgeAnimationParams dodgeAnimation;
    public GameObject tutorialAdam;

    [Header("RAGDOLL VALUES"), Space(5f)]
    public List<Rigidbody> ragdollRigidbodies;
    public List<Collider> ragdollColliders;
    public List<CapsuleCollider> clothColliders;

    [Header("RAGDOLL FORCE VALUES"), Space(5f)]
    public RagdollForceData forceData;
    public bool isAlreadyPushed;

    [Header("DODGE SLIDER")]
    public Slider dodgeSlider;

    public int hitCount;

    private CharacterMovement characterMovement;
    private Rigidbody body;
    private Animator animator;
    private DodgeAnimData dodgeData;
    private float counter;
    private bool canPlayTutorialPart = true;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();

        CacheRagdollColliders();
        CacheRagdollRigidbodies();
        CacheClothColliders();
        TurnOffRagdoll();
    }

    private void Start()
    {
        HandleDodgeAnimInitialState();
    }

    private void Update()
    {
        HandleTutorialPart();
    }

    private void HandleTutorialPart()
    {
        if (GameManager.instance.currentLevel == 1 && tutorialAdam.transform.position.z - transform.position.z <= 7f && canPlayTutorialPart)
        {
            PlayTutorialPart();
            canPlayTutorialPart = false;
        }
    }

    private void PlayTutorialPart()
    {
        float counter = 0f;
        DOTween.To(() => counter, x => counter = x, 1f, .5f)
        .OnStart(delegate
        {
            Time.timeScale = .25f;
            UIManager.instance.tutorialText.SetActive(true);
            UIManager.instance.SetTutorialText("AVOID THE MEN");
        })
        .OnComplete(delegate
        {
            Time.timeScale = 1f;
            UIManager.instance.tutorialText.SetActive(false);
        })
        .Play();
    }

    private void CacheRagdollRigidbodies()
    {
        var rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            if (rigidbody.gameObject != gameObject)
            {
                ragdollRigidbodies.Add(rigidbody);
            }
        }
    }

    private void CacheRagdollColliders()
    {
        var colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                ragdollColliders.Add(collider);
            }
        }
    }

    private void CacheClothColliders()
    {
        var colliders = gameObject.GetComponentsInChildren<CapsuleCollider>();
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                clothColliders.Add(collider);
            }
        }
    }

    public void TurnOnRagdoll()
    {
        animator.enabled = false;
        TurnOnRagdollCollidersAndRigidbodies();
    }

    public void TurnOffRagdoll()
    {
        animator.enabled = true;
        TurnOffRagdollCollidersAndRigidbodies();
    }

    public void DoRagdollForce()
    {
        foreach (var ragdollBody in ragdollRigidbodies)
        {
            ragdollBody.AddForce(forceData.Force * forceData.ForceDir, ForceMode.Impulse);
        }
    }

    private void TurnOffRagdollCollidersAndRigidbodies()
    {
        for (int i = 0; i < ragdollColliders.Count; i++)
        {
            ragdollColliders[i].isTrigger = true;
            ragdollRigidbodies[i].isKinematic = true;
        }
    }

    private void TurnOnRagdollCollidersAndRigidbodies()
    {
        for (int i = 0; i < ragdollColliders.Count; i++)
        {
            ragdollColliders[i].isTrigger = false;
            ragdollRigidbodies[i].isKinematic = false;
        }
    }

    public void HandleDodgeAnimInitialState()
    {
        dodgeAnimation.currentDodgeAnimIndex = (GameManager.instance.currentLevel - 1) % dodgeAnimation.animatorOverrideControllers.Count;
        dodgeData = dodgeAnimation.GetAnimationData();
        animator.runtimeAnimatorController = dodgeAnimation.GetAnimationController();
    }

    public void HandleDodgeSliderInitialValue()
    {
        dodgeData = dodgeAnimation.GetAnimationData();
        dodgeSlider.normalizedValue = dodgeData.InitialNormalizedTime;
    }

    public void PlayDodgeAnim()
    {
        var currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!characterMovement.isGameStarted)
        {
            animator.Play(currentStateInfo.shortNameHash, 0, dodgeData.InitialNormalizedTime);
            return;
        }

        float sign = Sign(Math.Round(dodgeSlider.normalizedValue, 2) - Math.Round(dodgeData.CurrentNormalizedTime, 2));
        dodgeData.CurrentNormalizedTime = currentStateInfo.normalizedTime + (sign * dodgeData.NormalizedTimeMultiplier);
        dodgeData.CurrentNormalizedTime = Mathf.Clamp(dodgeData.CurrentNormalizedTime, dodgeData.MinNormalizedTime, dodgeData.MaxNormalizedTime);
        animator.Play(currentStateInfo.shortNameHash, 0, dodgeData.CurrentNormalizedTime);
    }

    private float Sign(double number)
    {
        float sign = (number < 0) ? -1 : ((number > 0 ? 1 : 0));
        return sign;
    }

    #region LEGACY CODE
    public void StopDodgeAnim()
    {
        var currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //dodgeData.CurrentNormalizedTime = Mathf.Lerp(dodgeData.CurrentNormalizedTime, .5f, Time.deltaTime * dodgeData.NormalizedTimeHandsOffMultiplier);
        //dodgeSlider.value = Mathf.Lerp(dodgeData.CurrentNormalizedTime, .5f, Time.deltaTime * dodgeData.NormalizedTimeHandsOffMultiplier);
        animator.Play(currentStateInfo.shortNameHash, 0, dodgeData.CurrentNormalizedTime);
    }
    public void PlayDodgeAnimLittle(float oldSliderNormalizedValue)
    {
        var currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        dodgeSlider.normalizedValue += Sign((dodgeSlider.normalizedValue - oldSliderNormalizedValue)) * .1f;

        float sign = Sign(Math.Round(dodgeSlider.normalizedValue, 2) - Math.Round(dodgeData.CurrentNormalizedTime, 2));

        dodgeData.CurrentNormalizedTime = currentStateInfo.normalizedTime + (sign * dodgeData.NormalizedTimeMultiplier);
        dodgeData.CurrentNormalizedTime = Mathf.Clamp(dodgeData.CurrentNormalizedTime, dodgeData.MinNormalizedTime, dodgeData.MaxNormalizedTime);
        animator.Play(currentStateInfo.shortNameHash, 0, dodgeData.CurrentNormalizedTime);
    }

    #endregion
}
