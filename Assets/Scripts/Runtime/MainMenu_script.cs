using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_script : MonoBehaviour
{

    public string teleporter;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText, progressTextCp;

    IEnumerator LoadAsync (string teleporter)
    {
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(teleporter);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = (progress * 100f) + "%";
            progressTextCp.text = progressText.text;

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            StartCoroutine(LoadAsync(teleporter));
        }

    }
}