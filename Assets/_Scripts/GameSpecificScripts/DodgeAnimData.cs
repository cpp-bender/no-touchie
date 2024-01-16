using UnityEngine;

[CreateAssetMenu(menuName = "No Touchie/Dodge Anim Data", fileName = "No Touchie")]
public class DodgeAnimData : ScriptableObject
{
    [SerializeField] float initialNormalizedTime = .5f;
    [SerializeField] float currentNormalizedTime = .5f;
    [SerializeField] float normalizedTimeMultiplier = 0.005f;
    [SerializeField] float minNormalizedTime = 0f;
    [SerializeField] float maxNormalizedTime = .99f;

    public float CurrentNormalizedTime { get => currentNormalizedTime; set => currentNormalizedTime = value; }
    public float NormalizedTimeMultiplier { get => normalizedTimeMultiplier; set => normalizedTimeMultiplier = value; }
    public float MinNormalizedTime { get => minNormalizedTime; set => minNormalizedTime = value; }
    public float MaxNormalizedTime { get => maxNormalizedTime; set => maxNormalizedTime = value; }
    public float InitialNormalizedTime { get => initialNormalizedTime; }
}
