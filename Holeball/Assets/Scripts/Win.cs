using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField]
    AudioClip winSound = default;
    AudioSource audioSource;
    [SerializeField]
    string nextSceneName = default;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        if (nextSceneName == "") return;
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        audioSource.PlayOneShot(winSound);

        yield return new WaitForSeconds(3);

        int crntScene = SceneManager.GetActiveScene().buildIndex + 1;
        int nextScene = crntScene + 1;
        PlayerPrefs.SetInt("level" + crntScene, 1);

        if(!PlayerPrefs.HasKey("level" + nextScene))
            PlayerPrefs.SetInt("level" + nextScene, 0);

        SceneManager.LoadScene(nextSceneName);
    }
}
