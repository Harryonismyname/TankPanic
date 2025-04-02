using System.Collections.Generic;

namespace PolearmStudios.SceneManagement
{
    public class SceneLibrary
    {

        readonly Dictionary<SceneID, string> conversionMap = new()
    {
        {SceneID.BOOTSTRAP, "Bootstrap" },
        {SceneID.MAIN_MENU, "TitleMenu" },
        {SceneID.SAFETY_NET_TESTING, "SafetyNetTesting" },
        {SceneID.SAMPLE_CITY, "SampleCity" }
    };

        #region Singleton Pattern
        public static SceneLibrary Instance
        {
            get
            {
                instance ??= new SceneLibrary();
                return instance;
            }
        }
        private static SceneLibrary instance;
        #endregion

        private SceneLibrary()
        {
        }

        public bool TryGetSceneConversion(SceneID scene, out string conversion)
        {
            if (!conversionMap.ContainsKey(scene))
            {
                conversion = string.Empty;
                return false;
            }
            conversion = conversionMap[scene];
            return true;
        }
    }
}