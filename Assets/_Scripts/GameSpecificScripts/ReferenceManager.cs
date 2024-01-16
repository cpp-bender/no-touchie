using Random = UnityEngine.Random;
using DG.Tweening.Core.Enums;
using DG.Tweening;
using UnityEngine;
using System;

public class ReferenceManager : SingletonMonoBehaviour<ReferenceManager>
{
    public GemSpawnData gemData;
    public GemInARowSpawnData spawnData;
    public GemInARowSpawnData levelEndSpawnData;
    public CubeSpawnData cubeSpawnData;
    public TweenParamsData gemJumpTweenData;
    public FlagSpawnData flagSpawnData;

    private CharacterMovement characterMovement;
    private Tween gemJumpTween;
    private float gemNextSpawnTime;
    private float gemJumpTweenDelay;
    private int gemWaveSpawnCount;

    private void Start()
    {
        characterMovement = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<CharacterMovement>();
        levelEndSpawnData.CanSpawn = true;
    }

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
        InitDOTween();
    }

    private void Update()
    {
        if (characterMovement.isGameStarted && Time.time >= gemNextSpawnTime && levelEndSpawnData.CanSpawn)
        {
            SpawnGemsInARow();
        }
    }


    public void InitDOTween()
    {
        //Default All DOTween Global Settings
        DOTween.Init(true, true, LogBehaviour.Default);
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.maxSmoothUnscaledTime = .15f;
        DOTween.nestedTweenFailureBehaviour = NestedTweenFailureBehaviour.TryToPreserveSequence;
        DOTween.showUnityEditorReport = false;
        DOTween.timeScale = 1f;
        DOTween.useSafeMode = true;
        DOTween.useSmoothDeltaTime = false;
        DOTween.SetTweensCapacity(200, 50);

        //Default All DOTween Tween Settings
        DOTween.defaultAutoKill = false;
        DOTween.defaultEaseOvershootOrAmplitude = 1.70158f;
        DOTween.defaultEasePeriod = 0f;
        DOTween.defaultEaseType = Ease.Linear;
        DOTween.defaultLoopType = LoopType.Restart;
        DOTween.defaultRecyclable = false;
        DOTween.defaultTimeScaleIndependent = false;
        DOTween.defaultUpdateType = UpdateType.Normal;
    }

    public Tween GetGemJumpTween(GameObject gem)
    {
        gemJumpTween = gem.transform.DOMoveY(gemJumpTweenData.endValue, gemJumpTweenData.completionTime)
          .SetAs(gemJumpTweenData.TweenParams)
          .SetLink(gem)
          .SetDelay(gemJumpTweenDelay);

        gemJumpTweenDelay += .15f;
        return gemJumpTween;
    }

    public Tween GetGemJumpTweenForLevelEnd(GameObject gem)
    {
        gemJumpTween = gem.transform.DOMoveY(gemJumpTweenData.endValue, gemJumpTweenData.completionTime)
          .SetAs(gemJumpTweenData.TweenParams)
          .SetLink(gem);
        return gemJumpTween;
    }

    public void SpawnFinishFlag()
    {
        Instantiate(flagSpawnData.FinishFlagPrefab, flagSpawnData.InitialWorldPos, flagSpawnData.InitialQuaternion);
    }

    public void SpawnLevelEndFlags()
    {
        float posZ = flagSpawnData.LevelEndFlag[0].startPosZ;
        float posY = flagSpawnData.LevelEndFlag[0].startPosY;

        for (int i = 0; i < flagSpawnData.LevelEndFlags.Length; i++)
        {
            float posX = Random.Range(flagSpawnData.LevelEndFlag[i].minPosX, flagSpawnData.LevelEndFlag[i].maxPosX);
            Vector3 worldPos = new Vector3(posX, posY, posZ);
            Quaternion worldRot = flagSpawnData.InitialQuaternion;
            var flag = Instantiate(flagSpawnData.LevelEndFlags[i], worldPos, worldRot);
            posZ += flagSpawnData.OffsetZ;
        }
    }

    private float Calculate(float number)
    {
        float returnedValue = Mathf.Sign(number) == -1 ? 0f : number;
        return returnedValue;
    }

    private void SpawnGemsInARow()
    {
        if (gemWaveSpawnCount >= 10)
        {
            return;
        }

        gemWaveSpawnCount++;

        gemJumpTweenDelay = 0f;

        float gemPosX = Random.Range(spawnData.MinPosX, spawnData.MaxPosX);
        float gemPosY = 0f;
        float gemPosZ = Random.Range(spawnData.MinPosZ, spawnData.MaxPosZ);

        int spawnDir = spawnData.GetSpawnDir();

        int count = Convert.ToInt32((spawnData.MaxPosX - (gemPosX * (int)spawnData.SpawnDirection)) / spawnData.OffsetX);
        count = Mathf.Clamp(count, 0, spawnData.MaxWaveCount);

        for (int i = 0; i < count; i++)
        {
            Vector3 gemWorlPos = new Vector3(gemPosX, gemPosY, gemPosZ);
            Quaternion gemRotation = Quaternion.Euler(spawnData.InitialRotation);
            Vector3 gemScale = new Vector3(spawnData.InitialScaleValue, spawnData.InitialScaleValue, spawnData.InitialScaleValue);
            var gem = Instantiate(spawnData.GemPrefab, gemWorlPos, gemRotation);
            gem.transform.localScale = gemScale;
            gem.GetComponent<Gem>().SetMoveSpeed(spawnData.MoveSpeed);
            GetGemJumpTween(gem).Play();
            gemPosX += (spawnData.OffsetX * spawnDir);
            gemPosZ -= (spawnData.OffsetZ);
            gemNextSpawnTime = Time.time + spawnData.SpawnRate;
        }
    }

    #region LEGACY CODE
    public void SpawnGemsForLevelEnd()
    {
        float gemPosX = 0f;
        float gemPosY = 0f;
        float gemPosZ = Random.Range(levelEndSpawnData.MinPosZ, levelEndSpawnData.MaxPosZ);

        gemJumpTweenDelay = 0f;

        int count = Convert.ToInt32((levelEndSpawnData.MaxPosX - (gemPosX * (int)levelEndSpawnData.SpawnDirection)) / levelEndSpawnData.OffsetX);
        count = Mathf.Clamp(count, 0, levelEndSpawnData.MaxWaveCount);

        for (int i = 0; i < levelEndSpawnData.MaxWaveCount; i++)
        {
            gemPosX = Random.Range(levelEndSpawnData.MinPosX, levelEndSpawnData.MaxPosX);
            Vector3 gemWorlPos = new Vector3(gemPosX, gemPosY, gemPosZ);
            Quaternion gemRotation = Quaternion.Euler(levelEndSpawnData.InitialRotation);
            Vector3 gemScale = new Vector3(levelEndSpawnData.InitialScaleValue, levelEndSpawnData.InitialScaleValue, levelEndSpawnData.InitialScaleValue);
            var gem = Instantiate(levelEndSpawnData.GemPrefab, gemWorlPos, gemRotation);

            if (i == 0)
            {
                gem.tag = Tags.LastGem;
            }
            else
            {
                gem.tag = Tags.LevelEndGem;
            }

            gem.transform.localScale = gemScale;
            gem.GetComponent<Gem>().SetMoveSpeed(levelEndSpawnData.MoveSpeed);
            GetGemJumpTweenForLevelEnd(gem).Play();
            gemPosZ -= (levelEndSpawnData.OffsetZ);
            gemNextSpawnTime = Time.time + levelEndSpawnData.SpawnRate;
        }
    }

    public void SpawnRandomCubes()
    {
        levelEndSpawnData.CanSpawn = false;

        cubeSpawnData.RoadLength = Mathf.Abs(cubeSpawnData.StartMinPosX) + Mathf.Abs(cubeSpawnData.StartMaxPosX);

        float scaleX = cubeSpawnData.InitialScaleX;
        float scaleY = 1f;
        float scaleZ = 1f;

        float posX = Mathf.Clamp(Random.Range(cubeSpawnData.StartMinPosX, cubeSpawnData.StartMaxPosX), -(Calculate(cubeSpawnData.RoadLength - scaleX)), (Calculate(cubeSpawnData.RoadLength - scaleX)));
        float posY = .6f;
        float posZ = Random.Range(cubeSpawnData.StartMinPosZ, cubeSpawnData.StartMaxPosZ);

        for (int i = 0; i < cubeSpawnData.CubeCount; i++)
        {
            Vector3 worldPos = new Vector3(posX, posY, posZ);
            Vector3 cubeScale = new Vector3(scaleX, scaleY, scaleZ);
            var cube = Instantiate(cubeSpawnData.BaseCubePrefab, worldPos, Quaternion.identity);
            cube.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            cube.GetComponent<BaseCubeController>().SetMoveSpeed(cubeSpawnData.MoveSpeed);
            scaleX += cubeSpawnData.ScaleOffsetX;
            posZ += cubeSpawnData.OffsetZ;
            posX = Mathf.Clamp(Random.Range(cubeSpawnData.StartMinPosX, cubeSpawnData.StartMaxPosX), -(Calculate(cubeSpawnData.RoadLength - scaleX)), (Calculate(cubeSpawnData.RoadLength - scaleX)));
        }
    }

    #endregion
}
