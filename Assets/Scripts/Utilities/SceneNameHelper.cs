using System;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utilities
{
    public static class SceneNameHelper
    {
        public static SceneName GetActiveSceneName()
        {
            return ParseFromUnitySceneName(SceneManager.GetActiveScene().name);
        }

        private static SceneName ParseFromUnitySceneName(string unitySceneName)
        {
            if (!TryParseFromUnitySceneName(unitySceneName, out var sceneName))
                throw new ArgumentException(
                    $"Unknown Unity scene name '{unitySceneName}' for {nameof(SceneName)} enum."
                );

            return sceneName;
        }

        private static bool TryParseFromUnitySceneName(
            string unitySceneName,
            out SceneName sceneName
        )
        {
            if (Enum.TryParse(unitySceneName, ignoreCase: false, out sceneName))
                return true;

            sceneName = default;
            return false;
        }
    }
}
