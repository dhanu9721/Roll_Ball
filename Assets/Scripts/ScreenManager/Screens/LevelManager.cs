using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelManager : AbstractScreen
{
    //public LevelData[] levels;
    private List<LevelData> levelsData = new List<LevelData>();
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Transform levelContainer;
    public int levelNumber = 1;
    public string levelsFolderPath = "/Scripts/Levels";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void onShow(object data)
    {
        levelsData.Clear();
        //string folderPath = Path.Combine(Application.dataPath + levelsFolderPath);
        //string folderPath = Path.Combine(Application.streamingAssetsPath + levelsFolderPath);
        TextAsset[] levelFiles = Resources.LoadAll<TextAsset>("Levels");
        //string[] levelFiles = Directory.GetFiles(folderPath, "*.json");
        foreach (Transform child in levelContainer)
        {
            Destroy(child.gameObject);
        }

        //string filePath = Path.Combine(folderPath, "Level_2.json");
        foreach (TextAsset levelJson in levelFiles)
        {
            //string json = File.ReadAllText(filePath);
            string json = levelJson.text; ;
            Debug.Log(json);
            var ldata = JsonUtility.FromJson<LevelData>(json);

            GameObject instance = Instantiate(levelPrefab, levelContainer);
            //instance.transform.parent = levelContainer;
            LevelPrefab levelComponent = instance.GetComponent<LevelPrefab>();
            levelsData.Add(ldata);
            levelComponent.init(ldata);
            Debug.Log("on show of level manager");
        }

        // Deserialize the JSON into PrefabData object

        /* foreach (var level in levels)
         {
             GameObject instance = Instantiate(levelPrefab, levelContainer);
             //instance.transform.parent = levelContainer;
             LevelPrefab levelComponent = instance.GetComponent<LevelPrefab>();
             levelComponent.init(level);
         }*/
    }

    public override void onHide()
    {
        Debug.Log("on hide of level manager");
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
