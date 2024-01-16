using UnityEngine.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "Flag Spawn Data", menuName = "No Touchie/Flag Spawn Data")]
public class FlagSpawnData : ScriptableObject
{
    [Header("Finish Flag Params"), Space(5f)]
    [SerializeField] GameObject finishFlagPrefab;
    [SerializeField] Vector3 initialWorldPos = new Vector3(0f, 1f, 50f);
    [SerializeField] Quaternion initialQuaternion = Quaternion.Euler(0f, -180f, 0f);

    [Header("Level End Flags Params"), Space(5f)]
    [SerializeField] GameObject[] levelEndFlags;
    [SerializeField, FormerlySerializedAs("Custom Class For Each Level End Flag")] LevelEndFlag[] levelEndFlag;
    [SerializeField] float offsetZ = 10f;

    public GameObject FinishFlagPrefab { get => finishFlagPrefab; set => finishFlagPrefab = value; }
    public Vector3 InitialWorldPos { get => initialWorldPos; set => initialWorldPos = value; }
    public Quaternion InitialQuaternion { get => initialQuaternion; set => initialQuaternion = value; }
    public GameObject[] LevelEndFlags { get => levelEndFlags; set => levelEndFlags = value; }
    public LevelEndFlag[] LevelEndFlag { get => levelEndFlag; set => levelEndFlag = value; }
    public float OffsetZ { get => offsetZ; set => offsetZ = value; }
}
