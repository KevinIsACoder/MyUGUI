using UnityEngine;

namespace xasset.samples
{
    public class LoadingScreen : MonoBehaviour
    {
        public const string Filename = "Assets/xasset/Samples/Prefabs/LoadingScreen.prefab";
        public LoadingBar loadingBar;
        public static LoadingScreen Instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void SetProgress(string message, float progress)
        {
            loadingBar.SetProgress(message, progress);
        }
    }
}