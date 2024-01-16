using UnityEngine;

public class FinishFlagController : BaseFlagController
{
    private bool canSpawnLevelEndFlags = true;

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

    }
}
