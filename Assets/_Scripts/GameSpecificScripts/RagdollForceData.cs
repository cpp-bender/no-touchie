using UnityEngine.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "No Touchie/Ragdoll Force Data", fileName = "Ragdoll Force Data")]
public class RagdollForceData : ScriptableObject
{
    [SerializeField, FormerlySerializedAs("Force Value")] float force = 50;
    [SerializeField, FormerlySerializedAs("Force Dir")] Vector3 forceDir = new Vector3(0f, -1f, -1f);

    public float Force { get => force; set => force = value; }
    public Vector3 ForceDir { get => forceDir; set => forceDir = value; }
}
