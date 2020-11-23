using System.Xml.Serialization;

[XmlRoot("chnProjectConfig")]
public class xmlDataConfig
{
    [XmlAttribute]
    public string programName;

    [XmlAttribute]
    public bool debugMode;

    public SystemConfig mSystemConfig;
    public COMPortConfig mCOMPortConfig;

    public xmlDataConfig()
    {
        debugMode = true;
        mSystemConfig = new SystemConfig();
        mCOMPortConfig = new COMPortConfig();
    }

    public class SystemConfig
    {
        public SystemConfig()
        {
            resolutionWidth = 1920;
            resolutionHeight = 1080;

            fullScreenMode = true;
            alwayOnTopEnable = false;
            usingKey = true;

            screenSaverTimer = 1800;
            screenSaverEnable = 2;

            fontSize = 1;
            fontColor = "black";

            COMPortUse = false;
        }

        [XmlElement("resolutionWidth")]
        public int resolutionWidth { get; set; }

        [XmlElement("resolutionHeight")]
        public int resolutionHeight { get; set; }

        [XmlElement("fullScreenMode")]
        public bool fullScreenMode { get; set; }

        [XmlElement("alwayOnTopEnable")]
        public bool alwayOnTopEnable { get; set; }

        [XmlElement("usingKey")]
        public bool usingKey { get; set; }

        [XmlElement("screenSaverTimer")]
        public float screenSaverTimer { get; set; }

        [XmlElement("screenSaverEnable")]
        public float screenSaverEnable { get; set; }

        [XmlElement("fontSize")]
        public int fontSize { get; set; }

        [XmlElement("fontColor")]
        public string fontColor { get; set; }

        [XmlElement("COMPortUse")]
        public bool COMPortUse { get; set; }
    }

    public class COMPortConfig
    {
        public COMPortConfig()
        {
            COMPortName = "COM7";
            COMPortSpeed = 9600;
        }

        [XmlElement("COMPortName")]
        public string COMPortName { get; set; }

        [XmlElement("COMPortSpeed")]
        public int COMPortSpeed { get; set; }
    }
}
