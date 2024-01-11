using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLobbyManager : MonoBehaviour, ILoading
{
    [SerializeField] private LoadingScreen loadingScreen;

    public void LoadLobby()
    {
        AudioManager.Stop();
        loadingScreen.LoadScene(this);
    }

    public AsyncOperation LoadFunction()
    {
        return SceneManager.LoadSceneAsync("LOBBY", LoadSceneMode.Single);
    }
}
