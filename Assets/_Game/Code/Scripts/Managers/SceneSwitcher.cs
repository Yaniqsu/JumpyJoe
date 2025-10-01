using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    private struct SceneLoadRequest
    {
        public int sceneIndex;
        public string sceneName;

        public bool useID;
        public bool useName;

        public SceneLoadRequest(int sceneID)
        {
            sceneIndex = sceneID;
            sceneName = string.Empty;
            
            useID = true;
            useName = false;
        }
        
        public SceneLoadRequest(string sceneName)
        {
            sceneIndex = -1;
            this.sceneName = sceneName;
            
            useID = false;
            useName = true;
        }
    }
    
    [SerializeField] private Image _blackout;
    [SerializeField] private GameObject _animation;

    private static SceneSwitcher _instance;
    private bool _switching;
    
    private const float FADE_IN_DURATION = 0.5f;
    private const float FADE_OUT_DURATION = 0.5f;
    
    public static SceneSwitcher Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<SceneSwitcher>("SceneSwitcher"));
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    private static void SwitchScene(SceneLoadRequest request)
    {
        if (Instance == null || Instance._switching)
            return;

        Instance._switching = true;
        
        Instance._blackout.DOFade(1, FADE_IN_DURATION)
            .SetUpdate(true)
            .onComplete = () => Instance.StartCoroutine(HandleSceneTransition(request));
    }
    
    public static void SwitchScene(string sceneName) => 
        SwitchScene(new SceneLoadRequest(sceneName));
    
    public static void SwitchScene(int sceneID) => 
        SwitchScene(new SceneLoadRequest(sceneID));

    private static IEnumerator HandleSceneTransition(SceneLoadRequest request)
    {
        Instance._animation.SetActive(true);
        
        var operation = request.useID ? SceneManager.LoadSceneAsync(request.sceneIndex) : SceneManager.LoadSceneAsync(request.sceneName);

        if (operation == null)
            yield break;
        
        yield return new WaitUntil(() => operation.isDone);
        
        Instance._animation.gameObject.SetActive(false);
        Time.timeScale = 1;
        Instance._switching = false;
        
        Instance._blackout.DOFade(0, FADE_OUT_DURATION)
            .SetUpdate(UpdateType.Fixed);
    }
}
