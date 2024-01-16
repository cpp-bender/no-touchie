using UnityEngine;

public class DestroyGems : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.LastGem))
        {
            Destroy(other.gameObject);
            GameManager.instance.LevelComplete();
        }
        else if (other.CompareTag(Tags.Gem))
        {
            Destroy(other.gameObject);
        }
    }

    private void CheckForLevelEnd()
    {
        
    }
}
