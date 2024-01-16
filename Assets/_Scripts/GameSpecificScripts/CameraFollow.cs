using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CameraData camData;
    public Transform target;

    private PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        var followPos = new Vector3(target.position.x, 0f, 0f) + camData.FollowOffset;
        transform.position = Vector3.Lerp(transform.position, followPos, Time.deltaTime * camData.LerpSpeed);
    }
}
