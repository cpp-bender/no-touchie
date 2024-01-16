using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAdams : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.CloneAdam))
        {
            Destroy(other.gameObject);
        }
    }
}
