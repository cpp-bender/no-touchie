using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public TMP_FontAsset font;
    public string gameName;
    public bool gameHasGems;

    public Color mainColor;
    public Color gemColor;

    public GameObject[] levelCounts;
    public GameObject[] gemCountInLevelTexts;
    public GameObject[] gemCountTotalTexts;
    public GameObject[] gemIcons;
    public GameObject gameNameParentGameObject;
    public GameObject tutorialHand;
    public GameObject tutorialText;
    public GameObject[] panels;
    public GameObject restartButton;
    public GameObject successText;
    public GameObject failText;
    public GameObject nextButtonText;
    public GameObject dodgeSlider;
    public List<GameObject> hearts;

    public Color outline0;

    private bool isLevelStartedForUI;
    private bool isLevelCompletedForUI;
    private bool isLevelFailedForUI;

    private bool letCountGems;
    private float timerCountGems;
    private float timerForCreatingGems;
    private float speedCountGems;
    private int startGemCountInLevel;
    private int endGemCountInLevel;
    private int startGemCountTotal;
    private int endGemCountTotal;
    private int currentLostHeartIndex;

    void Awake()
    {
        CreateInstance();
    }

    void Start()
    {
        PrepareUI();
    }

    void Update()
    {
        LevelStartForUI();

        CountingGems();
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void PrepareUI()
    {
        for (int i = 0; i < levelCounts.Length; i++)
        {
            levelCounts[i].GetComponent<TextMeshProUGUI>().font = font;
            levelCounts[i].GetComponent<TextMeshProUGUI>().color = mainColor;
            levelCounts[i].GetComponent<TextMeshProUGUI>().text = "LEVEL " + GameManager.instance.currentLevel.ToString();
        }

        for (int i = 0; i < gemCountInLevelTexts.Length; i++)
        {
            gemCountInLevelTexts[i].GetComponent<TextMeshProUGUI>().font = font;
            gemCountInLevelTexts[i].GetComponent<TextMeshProUGUI>().color = mainColor;
            gemCountInLevelTexts[i].GetComponent<TextMeshProUGUI>().text = "0";

            if (!gameHasGems)
            {
                gemCountInLevelTexts[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < gemCountTotalTexts.Length; i++)
        {
            gemCountTotalTexts[i].GetComponent<TextMeshProUGUI>().font = font;
            gemCountTotalTexts[i].GetComponent<TextMeshProUGUI>().color = mainColor;
            gemCountTotalTexts[i].GetComponent<TextMeshProUGUI>().text = GameManager.instance.gemCountTotal.ToString();

            if (!gameHasGems)
            {
                gemCountTotalTexts[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < gemIcons.Length; i++)
        {
            gemIcons[i].GetComponent<Image>().color = gemColor;

            if (!gameHasGems)
            {
                gemIcons[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < gameNameParentGameObject.transform.childCount; i++)
        {
            gameNameParentGameObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = gameName;
            gameNameParentGameObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>().font = font;
        }

        gameNameParentGameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = mainColor;

        tutorialText.GetComponent<TextMeshProUGUI>().color = mainColor;
        tutorialText.GetComponent<TextMeshProUGUI>().font = font;

        successText.GetComponent<TextMeshProUGUI>().font = font;
        failText.GetComponent<TextMeshProUGUI>().font = font;
        nextButtonText.GetComponent<TextMeshProUGUI>().font = font;

        restartButton.GetComponent<Image>().color = mainColor;

        if (!gameHasGems)
        {
            gemIcons[0].SetActive(false);
        }

        restartButton.SetActive(false);

        PrepareGameName();
    }
    private void PrepareGameName()
    {
        gameNameParentGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().outlineColor = outline0;
        gameNameParentGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().outlineWidth = 1f;

        gameNameParentGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().outlineWidth = 0.001f;
        gameNameParentGameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().outlineWidth = 0.001f;
        gameNameParentGameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().outlineWidth = 0.001f;
    }
    public void LevelStartForUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isLevelStartedForUI)
            {
                gameNameParentGameObject.SetActive(false);
                tutorialHand.SetActive(false);
                tutorialText.SetActive(false);
                if (gameHasGems)
                {
                    gemIcons[0].SetActive(true);
                }
                restartButton.SetActive(true);

                GameManager.instance.isLevelStarted = true;
                GameManager.instance.isLevelCompleted = false;
                GameManager.instance.isLevelFailed = false;
                isLevelStartedForUI = true;
            }
        }
    }
    public void LevelCompleteForUI(float delay)
    {
        if (!isLevelCompletedForUI)
        {
            StartCoroutine(WaitForLevelComplete(delay));

            isLevelCompletedForUI = true;
        }
    }
    IEnumerator WaitForLevelComplete(float delay)
    {
        yield return new WaitForSeconds(delay);

        panels[0].SetActive(false);
        panels[1].SetActive(true);

        yield return new WaitForSeconds(1f);

        CountGems();
    }
    public void LevelFailForUI(float delay)
    {
        if (!isLevelFailedForUI)
        {
            StartCoroutine(WaitForLevelFail(delay));

            isLevelFailedForUI = true;
        }
    }
    IEnumerator WaitForLevelFail(float delay)
    {
        yield return new WaitForSeconds(delay);

        panels[0].SetActive(false);
        panels[2].SetActive(true);

        yield return new WaitForSeconds(1.25f);

        GameManager.instance.RestartLevel();
    }
    public void RefreshGemCountInLevel()
    {
        for (int i = 0; i < gemCountInLevelTexts.Length; i++)
        {
            gemCountInLevelTexts[i].GetComponent<TextMeshProUGUI>().text = GameManager.instance.gemCountInLevel.ToString();

            if (gemCountInLevelTexts[i].activeInHierarchy)
            {
                gemCountInLevelTexts[i].GetComponent<Animator>().SetTrigger("PopUp");
            }
        }
    }
    private void RefreshGemCountTotal()
    {
        for (int i = 0; i < gemCountTotalTexts.Length; i++)
        {
            gemCountTotalTexts[i].GetComponent<TextMeshProUGUI>().text = GameManager.instance.gemCountTotalTemp.ToString();

            if (gemCountTotalTexts[i].activeSelf)
            {
                gemCountTotalTexts[i].GetComponent<Animator>().SetTrigger("PopUp");
            }
        }
    }
    private void CountGems()
    {
        speedCountGems = 2f;
        letCountGems = true;
        startGemCountInLevel = GameManager.instance.gemCountInLevel;
        endGemCountInLevel = 0;
        startGemCountTotal = GameManager.instance.gemCountTotalTemp;
        endGemCountTotal = GameManager.instance.gemCountTotal;
    }
    private void CountingGems()
    {
        if (letCountGems)
        {
            timerCountGems += Time.deltaTime * speedCountGems;
            timerForCreatingGems += Time.deltaTime * speedCountGems;

            GameManager.instance.gemCountInLevel = (int)Mathf.Lerp(startGemCountInLevel, endGemCountInLevel, timerCountGems);
            GameManager.instance.gemCountTotalTemp = (int)Mathf.Lerp(startGemCountTotal, endGemCountTotal, timerCountGems);

            RefreshGemCountInLevel();
            RefreshGemCountTotal();

            if (timerCountGems >= 1f)
            {
                timerCountGems = 0f;
                letCountGems = false;
            }

            if (timerForCreatingGems >= 0.2f)
            {
                CreateFlyingGems();
                timerForCreatingGems = 0f;
            }
        }
    }
    private void CreateFlyingGems()
    {
        GameObject flyingGem = Instantiate(gemIcons[2], gemIcons[2].transform.position, Quaternion.identity, transform);
        flyingGem.transform.GetChild(0).gameObject.SetActive(false);
        flyingGem.AddComponent<GemIcon>().FlyGem(1, 4f, 0.5f);
    }
    public void NextLevelButton()
    {
        GameManager.instance.NextLevel();
    }
    public void RestartButton()
    {
        GameManager.instance.RestartLevel();
    }
    public void SetTutorialText(string newTutorialText)
    {
        tutorialText.GetComponent<TextMeshProUGUI>().text = newTutorialText;
    }

    private void HandleHeartsActiveness(bool turnOn)
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(turnOn);
        }
    }

    public void StartUI()
    {
        dodgeSlider.gameObject.SetActive(true);
        HandleHeartsActiveness(true);
        currentLostHeartIndex = hearts.Count - 1;
        //ReferenceManager.Instance.PlayTutorialPart();
    }

    public void LoseHealthUI()
    {
        hearts[currentLostHeartIndex].gameObject.SetActive(false);
        currentLostHeartIndex--;
    }
}