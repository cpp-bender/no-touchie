using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClothesController : MonoBehaviour
{
    public string clothesTypeQueue = "XXX - XXX - XXX";
    public bool randomlySelect = false;

    [Space(5f)]
    public List<GameObject> clotheTypeOne;
    public int clotheTypeOneChosenIndex;

    [Space(5f)]
    public List<GameObject> clotheTypeTwo;
    public int clotheTypeTwoChosenIndex;

    [Space(5f)]
    public List<GameObject> clotheTypeThree;
    public int clotheTypeThreeChosenIndex;

    private void OnValidate()
    {
        SetClothesTypeQueue();
    }

    private void Start()
    {
        SetChosenIndexRandomly();
        LoadAllClothes();
    }

    private void SetChosenIndexRandomly()
    {
        if (randomlySelect)
        {
            clotheTypeOneChosenIndex = Random.Range(0, clotheTypeOne.Count - 1);
            clotheTypeTwoChosenIndex = Random.Range(0, clotheTypeTwo.Count - 1);
            clotheTypeThreeChosenIndex = Random.Range(0, clotheTypeThree.Count - 1);
        }
    }

    protected abstract void SetClothesTypeQueue();

    protected virtual void LoadClothe(List<GameObject> clothesListToLoad, int chosenClotheIndex)
    {
        for (int i = 0; i < clothesListToLoad.Count; i++)
        {
            if (i == (chosenClotheIndex % clothesListToLoad.Count))
            {
                clothesListToLoad[i].SetActive(true);
            }
            else
            {
                clothesListToLoad[i].SetActive(false);
            }
        }
    }

    protected abstract void LoadAllClothes();

}
