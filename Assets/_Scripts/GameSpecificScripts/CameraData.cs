using UnityEngine;

[CreateAssetMenu(menuName = "No Touchie/Camera Data", fileName = "Camera Data")]
public class CameraData : ScriptableObject
{
    [SerializeField] Vector3 followOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] float lerpSpeed = 5f;

    public Vector3 FollowOffset { get => followOffset; set => followOffset = value; }
    public float LerpSpeed { get => lerpSpeed; set => lerpSpeed = value; }
}
