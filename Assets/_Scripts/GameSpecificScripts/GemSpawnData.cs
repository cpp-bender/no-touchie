using UnityEngine;

[CreateAssetMenu(menuName = "No Touchie/Gem Spawn Data", fileName = "Gem Spawn Data")]
public class GemSpawnData : ScriptableObject
{
    [SerializeField, Space(5f)] GameObject gemPrefab;
    [SerializeField, Space(5f)] float spawnRate = 2f;
    [SerializeField, Space(5f)] float moveSpeed = 5f;
    [SerializeField, Space(5f)] float minPosX = -5f;
    [SerializeField, Space(5f)] float maxPosX = 5f;
    [SerializeField, Space(5f)] float minPosZ = 30f;
    [SerializeField, Space(5f)] float maxPosZ = 100f;

    public GameObject GemPrefab { get => gemPrefab; set => gemPrefab = value; }
    public float SpawnRate { get => spawnRate; set => spawnRate = value; }
    public float MinPosX { get => minPosX; set => minPosX = value; }
    public float MaxPosX { get => maxPosX; set => maxPosX = value; }
    public float MinPosZ { get => minPosZ; set => minPosZ = value; }
    public float MaxPosZ { get => maxPosZ; set => maxPosZ = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
}
