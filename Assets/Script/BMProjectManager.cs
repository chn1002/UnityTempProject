using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BMProjectManager : IGameManager {
    #region Default Config
    [Header("Default Config")]
    public string[] scenesToLoad;
    public string activeScene;

    public string fileName = "bmProjectGame";
    public static BMProjectManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    private Scene scene;
    #endregion

    private string dbFileName = "BMProjectManagerManager.db";
    private DataService dataService;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this)        //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
            return;
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        #region Default Code
        InitGame(fileName);
        scene = SceneManager.GetActiveScene();
        activeScene = scene.name;
        #endregion

        InitConfig();

        InitBackground();

        Debug.Log("DB Name " + dbFileName);
        dataService = new DataService(dbFileName);

        var people = dataService.GetDBExample();

        foreach (var person in people)
        {
            Debug.Log("Index :" + person.index + ", " + person.name + ", " + person.date);
        }
    }

    //Update is called every frame.
    void Update()
    {
        #region Default Code
        defultKey();
        #endregion
    }

    // Debug GUI Interface
    private void OnGUI()
    {
        if (getDebugMode() == false)
            return;

        GUIStyle mGUILabelStyle, mGUItoggleStyle, mGUISliderSytle;

        mGUILabelStyle = new GUIStyle(GUI.skin.label);
        mGUILabelStyle.normal.textColor = mMoniterManager.getFontColor();
        mGUILabelStyle.hover.textColor = mMoniterManager.getFontColor(); 

        GUI.Label(mMoniterManager.showGUIMessage(0), "Lable Test", mGUILabelStyle);

        mGUItoggleStyle = new GUIStyle(GUI.skin.toggle);
        mGUItoggleStyle.normal.textColor = mMoniterManager.getFontColor();
        mGUItoggleStyle.hover.textColor = mMoniterManager.getFontColor();

        bool toggleTxt = false;
        GUI.Toggle(mMoniterManager.showGUIMessage(1), toggleTxt, "Toggle");

        mGUISliderSytle = new GUIStyle(GUI.skin.verticalScrollbar);
        mGUISliderSytle.normal.textColor = mMoniterManager.getFontColor();
        mGUISliderSytle.hover.textColor = mMoniterManager.getFontColor();

        float value = 0;
        float topValue=10;
        float bottonValue=1;

        GUI.VerticalSlider(mMoniterManager.showGUIMessage(2), value, topValue, bottonValue);
    }

    // Initinal Config value for game from XML Config file.
    private void InitConfig()
    {
    }

    // Initinal Backgournd GUI or Object
    private void InitBackground()
    {
    }

    // Create Target Object of All
    void CreateAllTargetObject(bool isCreate)
    {
    }

    // Disable Target Object of all 
    void DisableAllTargetObject()
    {
    }

    // Crate Target Object
    void CreateTargetObject()
    {
    }

    // The targetObj set Texture 
    private void setTargetPrefb(GameObject targetObj, Texture[] targetTexture)
    {
    }

    // Application Quit Action
    private void OnApplicationQuit()
    {
    }
}
