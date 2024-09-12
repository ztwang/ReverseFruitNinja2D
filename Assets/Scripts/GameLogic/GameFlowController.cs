using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour
{
    public GameObject loadingPanel;

    AsyncOperation loadingAsync;
    // Start is called before the first frame update
    void Start()
    {
        if (loadingPanel)
        {
            loadingPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnQuitGame() => Application.Quit();

    public void GoToScene(string name)
    {
        loadingAsync = SceneManager.LoadSceneAsync(sceneName: name, mode: LoadSceneMode.Single);
        StartCoroutine("WaitForLoading");
    }

    IEnumerator WaitForLoading()
    {
        if (loadingPanel != null && loadingAsync != null)
        {
            loadingPanel.SetActive(true);
            while (!loadingAsync.isDone)
            {
                yield return true;
            }
            if (loadingPanel.activeSelf)
            {
                loadingPanel.SetActive(false);
            }
            loadingAsync = null;
        }
    }
}
