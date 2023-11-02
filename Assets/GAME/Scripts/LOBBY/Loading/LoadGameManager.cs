using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameManager : MonoBehaviour, ILoading
{
    [SerializeField] private GameObject menu;
    [SerializeField] private LoadingScreen loadingScreen;

    public void LoadGame()
    {
        menu.SetActive(false);
        loadingScreen.LoadScene(this);
    }

    public AsyncOperation LoadFunction()
    {
        return SceneManager.LoadSceneAsync("PLAY", LoadSceneMode.Single);
    }
}
