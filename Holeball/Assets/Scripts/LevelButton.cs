using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    int sceneIndex = default;
    [SerializeField]
    Sprite closeImg = default;
    Sprite openImg = default;

    void Awake()
    {
        openImg = GetComponent<Image>().sprite;

        if (!PlayerPrefs.HasKey("level" + sceneIndex))
            SetCloseState();
        else if (PlayerPrefs.GetInt("level" + sceneIndex) == 0)
            SetOpenState();
        else if (PlayerPrefs.GetInt("level" + sceneIndex) == 1)
            SetCompleteState();
    }

    void SetCloseState()
    {
        GetComponent<Image>().sprite = closeImg;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    void SetOpenState()
    {
        GetComponent<Image>().sprite = openImg;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    void SetCompleteState()
    {
        GetComponent<Image>().sprite = openImg;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void LoadScene()
    {
        if (PlayerPrefs.HasKey("level" + sceneIndex))
            SceneManager.LoadScene(sceneIndex - 1);
    }
}
