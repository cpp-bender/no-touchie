using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamActionAnimations : MonoBehaviour
{
    private Animator adamAnims;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.CloneAdam))
        {
            adamAnims = other.GetComponent<Animator>();

            adamAnims.SetInteger(AdamAnimTriggers.AdamMiaAnimsIndex, Random.Range(0, 10));
            adamAnims.SetTrigger(AdamAnimTriggers.AdamMiaAnims);
        }
    }
}
