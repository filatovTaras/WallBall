using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurrentLevel : MonoBehaviour
{
    void Start()
    {
        int crntScene = SceneManager.GetActiveScene().buildIndex + 1;
        GetComponent<Text>().text = crntScene.ToString();
    }
}
