using UnityEngine;

public class TestMonoBehaviour : SingletonMonoBehaviour<TestMonoBehaviour>
{
    public bool canSpawnCubes = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSpawnCubes)
        {
            ReferenceManager.Instance.SpawnFinishFlag();
            canSpawnCubes = false;
        }
    }
}
