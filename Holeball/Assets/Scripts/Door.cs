using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public SceneObjects sceneObjects;
    [SerializeField]
    AudioClip closeDoorSound = default;
    [SerializeField]
    List<DoorUnlocker> doorUnlockers = new List<DoorUnlocker>();
    [SerializeField]
    bool isResetWithNewBall = false;

    BoxCollider collid;
    AudioSource audioSource;
    float closePosY;
    float openPosY;
    bool isMove = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        collid = GetComponent<BoxCollider>();
        closePosY = transform.position.y;
        openPosY = transform.position.y - 2;
    }

    void Start()
    {
        if(isResetWithNewBall)
            sceneObjects.door = this;
    }

    void Update()
    {
        Move();
    }

    public void TryOpen()
    {
        for(int i = 0; i < doorUnlockers.Count; i++)
        {
            if (!doorUnlockers[i].isActive) return;
        }
        collid.enabled = false;
        isMove = true;
        audioSource.PlayOneShot(closeDoorSound);
    }

    public void Close()
    {
        isMove = false;
        transform.position = new Vector3(transform.position.x, closePosY, transform.position.z);
        collid.enabled = true;
    }

    void Move()
    {
        if (isMove)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
            if (transform.position.y < openPosY || transform.position.y > closePosY)
                isMove = false;
        }
    }

    public void TryResetUnlockers()
    {
        for (int i = 0; i < doorUnlockers.Count; i++)
        {
            doorUnlockers[i].TryResetWithNewBall();
        }
    }
}
