using System.IO;
using UnityEngine;

public class IGameManager : MonoBehaviour {
    // Config
    [Header("XML Data")]
    xmlDataProcess gXMLProcess;
    private xmlDataConfig gXMLConfig;

    private string configFileName = "/Resources/mConfig.xml";
    private const string configFileExtention = ".xml";

    public INetworkManager mNetworkManager;
    public ISystemManager mSystemManager;
    public IMoniterManager mMoniterManager;

    /*
     * setting Config File Name  
     */
    protected void setConfigFileName(string fileName)
    {
        configFileName = "/Resources/" + fileName + configFileExtention;
    }

    //Initializes the game for each level.
    protected void InitGame(string fileName)
    {
        mNetworkManager = new INetworkManager();
        mSystemManager = new ISystemManager();
        mMoniterManager = new IMoniterManager();

        setConfigFileName(fileName);

        if (xmlConfigStart(fileName) == false)
        {
            createXMLConfig(gXMLConfig);
        }

        // Moniter Setting
        mMoniterManager.setFontSize(gXMLConfig.mSystemConfig.fontSize);
        mMoniterManager.setFontColor(gXMLConfig.mSystemConfig.fontColor);

        mSystemManager.setResolutions(gXMLConfig.mSystemConfig.resolutionWidth, 
            gXMLConfig.mSystemConfig.resolutionHeight, 
            gXMLConfig.mSystemConfig.fullScreenMode);

        setDebugMode(gXMLConfig.debugMode);
    }

    protected bool xmlConfigStart(string TitileName)
    {
        if(gXMLProcess == null)
        {
            gXMLProcess = new xmlDataProcess();
        }

        gXMLConfig = new xmlDataConfig { programName = TitileName};

        string fileName = Application.dataPath + configFileName;
        FileInfo configFile = new System.IO.FileInfo(fileName);

        if (configFile.Exists)
        {
            Debug.Log("File Check ");

           gXMLConfig = gXMLProcess.XMLDeserialize(fileName, gXMLConfig);
            return true;
        }
        else
        {
            Debug.Log("File is not exist " + fileName);
            return false; ;
        }
    }

    protected void createXMLConfig(xmlDataConfig xmlDataConf)
    {
        string fileName = Application.dataPath + configFileName;
        FileInfo configFile = new System.IO.FileInfo(fileName);

        Debug.Log("Create Config file : " + configFile.FullName);

        gXMLProcess.XMLCreate(xmlDataConf, fileName);
    }

    protected void setDebugMode(bool enable)
    {
        if(enable)
        {
            Cursor.visible = true;    //Mouse Cursor Visible
        }
        else
        {
            Cursor.visible = false;    //Mouse Cursor inVisible
            Cursor.lockState = CursorLockMode.Locked;   // Mouse Cursor Locking

            mSystemManager.SetWindowPosAlways();
        }
    }

    protected void defultKey()
    {

        if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Combination: Control + Q");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
        }
    }

    protected void close()
    {
        mNetworkManager.NetworkStop();
    }

    public bool getDebugMode()
    {
        if (gXMLConfig == null)
            return false;

        return gXMLConfig.debugMode;
    }

    protected void setXMLConfig(xmlDataConfig xmlData)
    {
        gXMLConfig = xmlData;
    }

    public xmlDataConfig getConfig()
    {
        return gXMLConfig;
    }
}
