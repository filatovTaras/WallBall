using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPull : MonoBehaviour
{
    public SceneObjects sceneObjects;

    void Awake()
    {
        sceneObjects.spotPull = transform;
    }
}
