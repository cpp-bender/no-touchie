using UnityEngine;

[CreateAssetMenu(menuName = "No Touchie/GemInARowSpawnData", fileName = "Gem Spawn Data - In A Row")]
public class GemInARowSpawnData : ScriptableObject
{
    [SerializeField, Space(5f)] GameObject gemPrefab;

    [SerializeField, Space(5f)] float initialScaleValue = .5f;
    [SerializeField, Space(5f)] Vector3 initialRotation = Vector3.right * -90f;
    [SerializeField, Space(5f)] float spawnRate = 10f;
    [SerializeField, Space(5f)] int maxWaveCount = 10;
    [SerializeField, Space(5f)] float moveSpeed = 5f;
    [SerializeField, Space(5f)] GemSpawnDirection spawnDirection = GemSpawnDirection.Left;

    [SerializeField, Space(5f)] float minPosX = -5f;
    [SerializeField, Space(5f)] float maxPosX = 5f;
    [SerializeField, Space(5f)] float offsetX = .5f;

    [SerializeField, Space(5f)] float minPosZ = 30f;
    [SerializeField, Space(5f)] float maxPosZ = 100f;
    [SerializeField, Space(5f)] float offsetZ = 5f;

    private GemSpawnDirection currentDir = GemSpawnDirection.Left;
    private bool canSpawn = true;

    public GameObject GemPrefab { get => gemPrefab; set => gemPrefab = value; }
    public float InitialScaleValue { get => initialScaleValue; set => initialScaleValue = value; }
    public float SpawnRate { get => spawnRate; set => spawnRate = value; }
    public int MaxWaveCount { get => maxWaveCount; set => maxWaveCount = value; }
    public float MinPosX { get => minPosX; set => minPosX = value; }
    public float MaxPosX { get => maxPosX; set => maxPosX = value; }
    public float OffsetX { get => offsetX; set => offsetX = value; }
    public float MinPosZ { get => minPosZ; set => minPosZ = value; }
    public float MaxPosZ { get => maxPosZ; set => maxPosZ = value; }
    public float OffsetZ { get => offsetZ; set => offsetZ = value; }
    public Vector3 InitialRotation { get => initialRotation; set => initialRotation = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public GemSpawnDirection SpawnDirection { get => spawnDirection; set => spawnDirection = value; }
    public bool CanSpawn { get => canSpawn; set => canSpawn = value; }

    public int GetSpawnDir()
    {
        if (currentDir == GemSpawnDirection.Left)
        {
            currentDir = GemSpawnDirection.Right;
            return (int)GemSpawnDirection.Right;
        }
        else
        {
            currentDir = GemSpawnDirection.Left;
            return (int)GemSpawnDirection.Left;
        }
    }
}

public enum GemSpawnDirection { Left = 1, Right = -1 }
