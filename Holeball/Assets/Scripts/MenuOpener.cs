using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    public SceneObjects sceneObjects;
    [SerializeField]
    RectTransform menuPanel = default;
    [SerializeField]
    RectTransform menu = default;
    [SerializeField]
    RectTransform butonImage = default;
    [SerializeField]
    AudioClip clickSound = default;
    AudioSource audioSource;
    //int speed = 200;
    Vector2 closePos;
    Vector2 openPos;
    Vector3 buttonOpenRotate = Vector2.zero;
    Vector3 buttonCloseRotate = new Vector3(0, 0, 180);

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Time.timeScale = 1;
        closePos = Vector2.zero;
        openPos = closePos;
        openPos.y = openPos.y - menu.rect.height;
        menuPanel.anchoredPosition = closePos;
    }

    public void OpenClose()
    {
        /*
        float step = speed * Time.deltaTime;
        Vector2 endPos = menu.transform.position;
        endPos.y = endPos.y - 200;
        menu.transform.position = Vector2.MoveTowards(menu.transform.position, endPos, step);
        */
        if (menuPanel.anchoredPosition == Vector2.zero)
        {
            sceneObjects.playerController.gameObject.SetActive(false);
            menuPanel.anchoredPosition = openPos;
            Time.timeScale = 0;
            butonImage.rotation = Quaternion.Euler(buttonCloseRotate);
        }
        else
        {
            sceneObjects.playerController.gameObject.SetActive(true);
            menuPanel.anchoredPosition = closePos;
            Time.timeScale = 1;
            butonImage.rotation = Quaternion.Euler(buttonOpenRotate);
        }
        audioSource.PlayOneShot(clickSound);
    }
}
