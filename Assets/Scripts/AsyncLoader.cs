using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader: MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider Slider;
    public Text Text;

    public void LoadLevel(string Scene)
    {
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(Scene));
    }

    IEnumerator LoadLevelASync(string Scene)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(Scene);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            Slider.value = progressValue;
            Text.text = progressValue.ToString() + "%";
            yield return null;
        }
    }
}
