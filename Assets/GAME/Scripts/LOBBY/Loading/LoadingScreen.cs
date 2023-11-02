using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : BarUI
{
    [SerializeField] private GameObject screenObject;

    float progressValue = 0f;
    protected override float Amount => progressValue;
    protected override float MaxAmount => 1f;

    public void LoadScene(ILoading load)
    {
        screenObject.SetActive(true);

        progressValue = 0f;
        Refresh();
        
        StartCoroutine(Loading(load));
    }

    IEnumerator Loading(ILoading load)
    {
        yield return new WaitUntil(() => Time.timeScale == 1f);
        
        AsyncOperation loading = load.LoadFunction();

        while(!loading.isDone)
        {
            progressValue = Mathf.Clamp01(loading.progress * 0.9f);
            Refresh();

            yield return null;
        }
    }
}
