using UnityEngine;

[CreateAssetMenu(fileName = "Cube Spawn Data", menuName = "No Touchie/Cube Spawn Data")]
public class CubeSpawnData : ScriptableObject
{
    [SerializeField, Space(5f)] GameObject baseCubePrefab;

    [SerializeField, Space(5f)] float startMinPosZ = 50f;
    [SerializeField, Space(5f)] float startMaxPosZ = 100f;
    [SerializeField, Space(5f)] float offsetZ = 5f;
    [SerializeField, Space(5f)] float initialScaleX = 10f;
    [SerializeField, Space(5f)] float scaleOffsetX = 1f;

    [SerializeField, Space(5f)] float startMinPosX = -5f;
    [SerializeField, Space(5f)] float startMaxPosX = 5f;

    [SerializeField, Space(5f)] int cubeCount = 10;
    [SerializeField, Space(5f)] float moveSpeed = 10f;

    private float roadLength = 10f;

    public GameObject BaseCubePrefab { get => baseCubePrefab; set => baseCubePrefab = value; }
    public float StartMinPosZ { get => startMinPosZ; set => startMinPosZ = value; }
    public float StartMaxPosZ { get => startMaxPosZ; set => startMaxPosZ = value; }
    public int CubeCount { get => cubeCount; set => cubeCount = value; }
    public float OffsetZ { get => offsetZ; set => offsetZ = value; }
    public float StartMinPosX { get => startMinPosX; set => startMinPosX = value; }
    public float StartMaxPosX { get => startMaxPosX; set => startMaxPosX = value; }
    public float RoadLength { get => roadLength; set => roadLength = value; }
    public float ScaleOffsetX { get => scaleOffsetX; set => scaleOffsetX = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float InitialScaleX { get => initialScaleX; set => initialScaleX = value; }
}
