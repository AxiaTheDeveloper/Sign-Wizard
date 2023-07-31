using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance{get;private set;}
    [SerializeField]private Slider slideLoader;
    private void Awake() {
        Instance = this;
        slideLoader.value = 0f;
    }
    public void LoadScene(string sceneName){
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        while(!loadOperation.isDone)
        {   
            float progress = Mathf.Clamp01(loadOperation.progress / .9f);
            slideLoader.value = progress;
            yield return null;
        }
    }
}
