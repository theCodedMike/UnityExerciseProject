namespace M11D27.UIFramework
{
    [System.Serializable]
    public class PanelInfo
    {
        public PanelType type;
        public string path;
    }

    public enum PanelType
    {
        Login, Main, Setting, Bag
    }
}