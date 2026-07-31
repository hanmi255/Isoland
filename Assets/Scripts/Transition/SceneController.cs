using System.Collections;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 定义场景切换控制器。
/// </summary>
/// <remarks>
/// 依赖：场景事件中心、存档系统、Unity 场景管理和淡入淡出 UI。
/// 使用场景：负责前后台场景切换、淡入淡出效果以及场景切换前后的数据存取。
/// </remarks>
namespace Assets.Scripts.Transition
{
    /// <summary>
    /// 管理场景切换流程和过渡动画。
    /// </summary>
    /// <remarks>
    /// 职责：串联淡出、存档、卸载、加载、恢复和淡入等完整场景切换步骤。
    /// </remarks>
    public class SceneController : SingletonMonoBehaviour<SceneController>
    {
        #region Fields

        /// <summary>
        /// 控制淡入淡出遮罩透明度与输入拦截的画布组。
        /// </summary>
        [SerializeField]
        private CanvasGroup _fadeCanvasGroup = null;

        /// <summary>
        /// 用于显示黑幕的遮罩图像。
        /// </summary>
        [SerializeField]
        private Image _fadeImage = null;

        /// <summary>
        /// 单次淡入或淡出持续时间。
        /// </summary>
        [SerializeField]
        private float _fadeDuration = 1f;

        /// <summary>
        /// 标记当前是否正在执行淡入淡出流程。
        /// </summary>
        private bool _isFading;

        #endregion

        #region Lifecycle Methods

        /// <summary>
        /// 初始化淡入淡出遮罩并执行开场淡出。
        /// </summary>
        private void Start()
        {
            _fadeImage.color = new Color(0, 0, 0, 1);
            _fadeCanvasGroup.alpha = 1;

            StartCoroutine(Fade(0f));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 以淡入淡出过渡方式加载指定场景。
        /// </summary>
        /// <param name="sceneName">要加载的场景名称</param>
        public void FadeAndLoadScene(SceneName sceneName)
        {
            if (_isFading)
                return;

            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 执行淡入、存档、场景切换、恢复和淡出的一整套流程。
        /// </summary>
        /// <param name="sceneName">要切换到的场景名称</param>
        private IEnumerator FadeAndSwitchScenes(SceneName sceneName)
        {

            yield return StartCoroutine(Fade(1f));

            if (SceneNameHelper.GetActiveSceneName() != SceneName.PersistentScene)
            {
                yield return SceneManager.UnloadSceneAsync(
                    SceneManager.GetActiveScene().buildIndex
                );
            }

            yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

            yield return StartCoroutine(Fade(0f));
        }

        /// <summary>
        /// 以附加模式加载场景并将其设为当前激活场景。
        /// </summary>
        /// <param name="sceneName">要加载的场景名称</param>
        private IEnumerator LoadSceneAndSetActive(SceneName sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Additive);

            var newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            SceneManager.SetActiveScene(newlyLoadedScene);
        }

        /// <summary>
        /// 执行画布遮罩的淡入或淡出动画。
        /// </summary>
        /// <param name="finalAlpha">淡入淡出的目标透明度</param>
        private IEnumerator Fade(float finalAlpha)
        {
            _isFading = true;

            _fadeCanvasGroup.blocksRaycasts = true;

            float fadeSpeed = Mathf.Abs(_fadeCanvasGroup.alpha - finalAlpha) / _fadeDuration;

            while (!Mathf.Approximately(_fadeCanvasGroup.alpha, finalAlpha))
            {
                _fadeCanvasGroup.alpha = Mathf.MoveTowards(
                    _fadeCanvasGroup.alpha,
                    finalAlpha,
                    fadeSpeed * Time.deltaTime
                );
                yield return null;
            }

            _isFading = false;

            _fadeCanvasGroup.blocksRaycasts = false;
        }

        #endregion
    }
}
