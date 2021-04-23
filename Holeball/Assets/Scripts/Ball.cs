using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public PushSetting pushSetting;

    void Awake()
    {
        pushSetting.crntBall = transform;
        gameObject.SetActive(false);
    }
}
