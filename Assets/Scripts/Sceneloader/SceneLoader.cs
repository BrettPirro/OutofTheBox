using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    
    bool loading = false;

    public static SceneLoader current;

    private void Start()
    {
        current = this;
    }



    public static void loadNextScene(float delay) 
    {
        if (current.loading==false) { current.StartCoroutine(current.LoadNextScene(delay)); }
        
    }

    
    

    public static void loadScenebyName(string name)
    {
        if (current.loading == false) { current.StartCoroutine(current.LoadSceneByName(0.5f, name)); }
        
    }


    public static void QuitGame() 
    {
        Application.Quit();
    }



    IEnumerator FadeSceneIn(float seconds) 
    {
    
        while (canvasGroup.alpha < 1) 
        {
            canvasGroup.alpha += Time.deltaTime/seconds;
            yield return null;
        }
    }

    IEnumerator FadeSceneOut(float seconds)
    {
      
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime/seconds;
            yield return null;
        }
    }

    IEnumerator LoadNextScene(float delay) 
    {

        loading = true;
        yield return FadeSceneIn(delay);
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        yield return FadeSceneOut(delay);
        loading = false;
    }

    IEnumerator LoadSceneByName(float delay,string name)
    {
        loading = true;
        yield return FadeSceneIn(delay);
        yield return SceneManager.LoadSceneAsync(name);
        yield return FadeSceneOut(delay);
        loading = false;
    }


}
