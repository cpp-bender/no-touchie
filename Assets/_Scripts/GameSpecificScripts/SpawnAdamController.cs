using Random = UnityEngine.Random;
using UnityEngine;
using DG.Tweening;

public class SpawnAdamController : MonoBehaviour
{
    private Vector3 adamPos;
    private GameObject spawner;
    private GameObject cloneAdam;
    private AdamController adamController;
    private CharacterMovement characterMovement;
    private int randomAnimIndex;
    private TweenCallback finishFlagCallback;
    private TweenCallback levelEndFlagsCallBack;

    public GameObject adam;
    public float posXRange;
    public float spawnTime;
    public float nextSpawnTime;
    public float timeBetweenSpawns;
    private float timeSinceLevelLoad;

    void Start()
    {
        adamController = FindObjectOfType<AdamController>();
        spawner = GameObject.FindGameObjectWithTag(Tags.Spawner);
        characterMovement = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<CharacterMovement>();
        finishFlagCallback = ReferenceManager.Instance.SpawnFinishFlag;
        levelEndFlagsCallBack = ReferenceManager.Instance.SpawnLevelEndFlags;
        timeSinceLevelLoad = 0f;
    }

    private void Update()
    {
        if (characterMovement.isGameStarted && timeSinceLevelLoad >= nextSpawnTime)
        {
            SpawnAdam();
            SpawnAI();
            EndGameOnTime();
        }

        timeSinceLevelLoad += Time.deltaTime;
    }

    private void EndGameOnTime()
    {
        if (nextSpawnTime >= 15)
        {
            DOVirtual.DelayedCall(2f, finishFlagCallback)
                .OnComplete(delegate
                {
                    ReferenceManager.Instance.SpawnLevelEndFlags();
                })
                .Play();

            enabled = false;
        }
    }

    private void SpawnAI()
    {
        timeBetweenSpawns -= 0.15f;

        if (timeBetweenSpawns <= 1.5f)
        {
            timeBetweenSpawns = 1.5f;
        }
    }

    private void SpawnAdam()
    {
        randomAnimIndex = Random.Range(0, 5);
        adamPos = new Vector3(Random.Range(-posXRange, posXRange), 0, spawner.transform.position.z);

        cloneAdam = Instantiate(adam, adamPos, adam.transform.rotation);
        cloneAdam.GetComponent<Transform>().tag = Tags.CloneAdam;

        cloneAdam.GetComponent<AdamController>().RandomizeGiftsAndFlowers();
        AdamRandomRunningAnims();
        SpawnDifferentAdamTypes();

        nextSpawnTime = timeSinceLevelLoad + timeBetweenSpawns;
    }

    private void AdamRandomRunningAnims()
    {
        cloneAdam.GetComponent<Animator>().SetInteger(AdamAnimTriggers.AdamRunAnimsIndex, randomAnimIndex);
        cloneAdam.GetComponent<Animator>().SetTrigger(AdamAnimTriggers.AdamRunAnims);
    }

    private void SpawnDifferentAdamTypes()
    {
        cloneAdam.GetComponent<AdamClothesController>().clotheTypeOneChosenIndex = Random.Range(0, 3);
        cloneAdam.GetComponent<AdamClothesController>().clotheTypeTwoChosenIndex = Random.Range(0, 3);
        cloneAdam.GetComponent<AdamClothesController>().clotheTypeThreeChosenIndex = Random.Range(0, 3);
    }
}
