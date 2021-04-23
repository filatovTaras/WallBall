using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSkiper : MonoBehaviour
{
    public void SkipLevel()
    {
        int crntScene = SceneManager.GetActiveScene().buildIndex + 1;
        int nextScene = crntScene + 1;

        if (PlayerPrefs.GetInt("level" + crntScene) != 1)
            PlayerPrefs.SetInt("level" + crntScene, 0);

        if (PlayerPrefs.GetInt("level" + nextScene) != 1)
            PlayerPrefs.SetInt("level" + nextScene, 0);

        SceneManager.LoadScene(crntScene);
    }
}
