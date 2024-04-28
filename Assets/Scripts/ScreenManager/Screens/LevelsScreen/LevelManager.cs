using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;

public class LevelManager : AbstractScreen
{
    //public LevelData[] levels;
    private List<LevelData> levelsData = new List<LevelData>();
    private Dictionary<string,LevelPrefab> LevelComponents = new Dictionary<string,LevelPrefab>();
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private GameObject levelContainerPrefab;
    //[SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform LevelsPageContainer;
    [SerializeField] private RectTransform LevelsPageViewPort;
    [SerializeField] private GameObject RightButton;
    [SerializeField] private GameObject LeftButton;

    public List<Transform> levelPages = new List<Transform>();
    public float scrollSpeed = 100f;
    private int currentPageIndex = 0;
    private int previousPageIndex = 0;
    private int levelsPerPage = 1;
    private bool isScrolling = false;
    public float dragThreshold = 50f;
    private bool isDragged = false;
    private Vector2 startDragPosition;

    public int levelNumber = 1;
    public string levelsFolderPath = "/Scripts/Levels";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void onShow(object data)
    {
        levelsData.Clear();
        LevelComponents.Clear();
        levelPages.Clear();
        int totalCompletedLevels  = LocalDataManager.GetPlayerProgressIntType(LOCAL_DATA_KEYS.COMPLETED_LEVELS);
        DataManager.GetInstance().setCurrentLevel(totalCompletedLevels+1);
        TextAsset[] levelFiles = Resources.LoadAll<TextAsset>("Levels");
        foreach (Transform child in LevelsPageContainer)
        {
            Destroy(child.gameObject);
        }

        int levelsCounter = 0;
        List<TextAsset> levelsListPerPage = new List<TextAsset>();
        for (int i = 0;i < levelFiles.Length;i++)
        {
            TextAsset levelJson = levelFiles[i];
            levelsCounter++;
            levelsListPerPage.Add(levelJson);
            if(levelsCounter% levelsPerPage == 0)
            {
                GenerateLevelsPerPage(levelsListPerPage);
                levelsCounter = 0;
                levelsListPerPage.Clear();
            }
            else if(levelsCounter < levelsPerPage && i == levelFiles.Length-1)
            {
                GenerateLevelsPerPage(levelsListPerPage);
                levelsCounter = 0;
                levelsListPerPage.Clear();
            }
        }  
    }

    public void GenerateLevelsPerPage(List<TextAsset> levelsListPerPage)
    {
        GameObject levelContainer = Instantiate(levelContainerPrefab, LevelsPageContainer);
        foreach (TextAsset levelJson in levelsListPerPage)
        {
            string json = levelJson.text;
            Debug.Log(json);
            var ldata = JsonUtility.FromJson<LevelData>(json);

            GameObject instance = Instantiate(levelPrefab, levelContainer.transform);
            //instance.transform.parent = levelContainer;
            LevelPrefab levelComponent = instance.GetComponent<LevelPrefab>();
            SetPlayerProgressInitially(levelComponent, ldata);
        }
        levelPages.Add(levelContainer.transform);
        SetLevelPageInitially();
    }

    public void SetPlayerProgressInitially(LevelPrefab levelComponent, LevelData levelData)
    {   
        string levelKey = LOCAL_DATA_KEYS.LEVEL + levelData.levelNumber;
        CreatePlayerPrefsProgressData completedLevelData = JsonUtility.FromJson<CreatePlayerPrefsProgressData>(LocalDataManager.GetPlayerProgressStringType(levelKey));
            Debug.Log("level Data "+ completedLevelData);
        if (completedLevelData != null)
        {
            levelData.isCompleted = true;
            levelData.starCounts = completedLevelData._collectedStars;
                // TODO Set level prefab data here (complete, uncomplete, star counts,etc)
        }

        levelsData.Add(levelData);
        levelComponent.init(levelData);
        LevelComponents.Add(LOCAL_DATA_KEYS.LEVEL + levelData.levelNumber, levelComponent);

    } 
    
    void SetLevelPageInitially()
    {
        int completedLevels = LocalDataManager.GetPlayerProgressIntType(LOCAL_DATA_KEYS.COMPLETED_LEVELS);
        Debug.Log("compdjf : " + completedLevels);
        currentPageIndex = completedLevels / levelsPerPage;
        Debug.Log("pageNumber : " + currentPageIndex);
        //Vector2 targetPosition = new Vector2(-(currentPageIndex * scrollRect.viewport.rect.width), scrollRect.content.anchoredPosition.y);
        Vector2 targetPosition = new Vector2(-(currentPageIndex * LevelsPageViewPort.rect.width + (LevelsPageViewPort.rect.width / 2)), LevelsPageContainer.anchoredPosition.y);
        LevelsPageContainer.anchoredPosition = targetPosition;
        HandleButtonsVisibility(currentPageIndex);
        previousPageIndex = currentPageIndex;
        //ScrollToPage(currentPageIndex);
    }
    public void NextPage()
    {
        //Debug.Log("current page : "+ currentPageIndex+ "  " +levelPages.Count);
        if (currentPageIndex < levelPages.Count - 1 && !isScrolling)
        {
            currentPageIndex++;
            ScrollToPage(currentPageIndex);
        }
        else
        {
            isDragged = false;
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0 && !isScrolling)
        {
            currentPageIndex--;
            ScrollToPage(currentPageIndex);
        }
        else
        {
            isDragged = false;
        }
    }

    void ScrollToPage(int pageIndex)
    {
        isScrolling = true;
        HandleButtonsVisibility(pageIndex);
        float targetHorizontalPosition = ((pageIndex * LevelsPageViewPort.rect.width)+ (LevelsPageViewPort.rect.width/2));
        float previousHorizontalPosition = ((previousPageIndex * LevelsPageViewPort.rect.width) + (LevelsPageViewPort.rect.width / 2));
        //Debug.Log("targetHorizontalpos : " + targetHorizontalPosition);
        //float step = Mathf.Abs(targetHorizontalPosition - scrollRect.content.anchoredPosition.x) / scrollSpeed;
        float step = Mathf.Abs(targetHorizontalPosition - previousHorizontalPosition) / scrollSpeed;
        //Debug.Log("step : " + step);
        Vector2 targetPosition = new Vector2(-targetHorizontalPosition, LevelsPageContainer.anchoredPosition.y);
        //Debug.Log("target pos : " + targetPosition);

        StartCoroutine(ScrollSmoothly(targetPosition, step));
        previousPageIndex = pageIndex;
       
    }

    void HandleButtonsVisibility(int currentPageIndex)
    {
        if (currentPageIndex <= 0)
        {
            LeftButton.SetActive(false);
            RightButton.SetActive(true);
        }
        else if(currentPageIndex >= levelPages.Count-1) {
            //Debug.Log("pagv : " + currentPageIndex);
            //Debug.Log("levelPages.Count-1 : " + levelPages.Count);
            LeftButton.SetActive(true);
            RightButton.SetActive(false);
        }
        else {
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
        }
    }

    IEnumerator ScrollSmoothly(Vector2 targetPosition, float step)
    {
        while (Vector2.Distance(LevelsPageContainer.anchoredPosition, targetPosition) > 0.1f)
        {
            LevelsPageContainer.anchoredPosition = Vector2.MoveTowards(LevelsPageContainer.anchoredPosition, targetPosition, step * Time.deltaTime*300);
            yield return true;
        }
        isScrolling = false;
        isDragged = false;
        LevelsPageContainer.anchoredPosition = targetPosition;
    }

    public void OnClickExit()
    {
        GameModel.screenManager.showScreen(ScreenEnum.MainMenuScreen, null);
    }

    public LevelData GetCurrentLevelData()
    {
        return levelsData[DataManager.GetInstance().getCurrentLevel() - 1];
    }
    public LevelData GetNextLevelData()
    {
        DataManager.GetInstance().setCurrentLevel(DataManager.GetInstance().getCurrentLevel() + 1);
        return levelsData[DataManager.GetInstance().getCurrentLevel()-1];
    }

    public override void onHide()
    {
        Debug.Log("on hide of level manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startDragPosition = Input.mousePosition;
        }

        else if (!isScrolling && Input.GetMouseButtonUp(0) && !isDragged)
        {
            Vector2 dragDelta = (Vector2)Input.mousePosition - startDragPosition;
            if (Mathf.Abs(dragDelta.x) >= dragThreshold && !isDragged)
            {
                isDragged = true;
                if (dragDelta.x > 0)
                {
                    PreviousPage();
                }
                else if (dragDelta.x < 0)
                {
                    NextPage();
                }
            }
        }
    }
}
