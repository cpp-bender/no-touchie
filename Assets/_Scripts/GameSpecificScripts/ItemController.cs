using DG.Tweening;
using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour
{
    public bool isThisFlower;
    public GameObject[] GiftItems;

    private void Start()
    {
        GiftItems = GameObject.FindGameObjectsWithTag(Tags.Item);
        //RandomlySpawnItems();
    }

    public void RandomlySpawnItems()
    {
        Debug.Log("Random obj");
        GiftItems[Random.Range(0, GiftItems.Length)].SetActive(false);
    }

    public void ItemFall()
    {
        gameObject.transform.parent = null;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(AddForceAndScaleDown());
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }

    private IEnumerator AddForceAndScaleDown()
    {
        Vector3 forceDirection = new Vector3(1f, 1f, 0f);

        if (!isThisFlower)
        {
            forceDirection.x *= -1f;
        }

        gameObject.GetComponent<Rigidbody>().AddForce(forceDirection.x * Random.Range(75f, 125f), forceDirection.y * Random.Range(-30f, 30f), forceDirection.z * Random.Range(-30f, 30f), ForceMode.Force);
        yield return new WaitForSeconds(3f);
        gameObject.transform.DOScale(0f, 1f).Play();
    }
}
