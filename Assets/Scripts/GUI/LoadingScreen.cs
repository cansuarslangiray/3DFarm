using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loaderUI;
    public Slider loadBar;

    public GameObject canvas;
    public GameObject helpCanvas;
    
    private void Start()
    {
        if (loaderUI == null || loadBar == null)
        {
            Debug.LogError("UI elements are not assigned.");
        }
        
        helpCanvas.SetActive(false);
    }

    public void LoadScene(int index)
    {
        loaderUI.SetActive(true);
        StartCoroutine(LoadSceneCoroutine(index));
    }

    private IEnumerator LoadSceneCoroutine(int index)
    {
        loadBar.value = 0;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;

        float elapsedTime = 0f;
        float totalLoadingTime = 2f; 

        while (elapsedTime < totalLoadingTime)
        {
            float progress = Mathf.Clamp01(elapsedTime / totalLoadingTime);
            loadBar.value = progress;

            //Debug.Log("Loading progress: " + progress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        loadBar.value = 1;
        Debug.Log("Loading complete");
        asyncOperation.allowSceneActivation = true;
        
        yield return new WaitForSeconds(0.5f);
        
        loaderUI.SetActive(false);
    }

   public void HelpButton()
    {
        helpCanvas.SetActive(true);
        canvas.SetActive(false);
    }

    public void CloseHelp()
    {
        helpCanvas.SetActive(false);
        canvas.SetActive(true);
    } 
    
}