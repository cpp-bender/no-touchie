using System.Collections;
using UnityEngine;

public class FlagController : BaseFlagController
{
    public int factor = 2;

    private bool canHitPlayer;

    private void Awake()
    {
        canHitPlayer = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerRagdollCollider) && canHitPlayer && !GameManager.instance.isLevelCompleted)
        {
            var collectGemCount = (GameManager.instance.gemCountInLevel * factor) - GameManager.instance.gemCountInLevel;
            GameManager.instance.CollectGem(new Vector3(0f, 0f, 0f), collectGemCount);
            GameManager.instance.LevelComplete();
            canHitPlayer = false;
        }
    }
}
