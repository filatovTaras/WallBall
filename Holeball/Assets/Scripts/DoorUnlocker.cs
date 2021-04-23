using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{
    [SerializeField]
    Color activeColor = default;
    Color DeactiveColor;
    [SerializeField]
    Door door = default;
    public bool isActive
    {
        get
        {
            return _isActive;
        }
        set
        {
            _isActive = value;
            if (value) Activate();
            if (!value) Deactivate();
        }
    }
    bool _isActive = false;
    [SerializeField]
    bool isResetWithNewBall = false;
    Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        DeactiveColor = rend.material.color;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") return;
        if (_isActive)
            isActive = false;
        else
            isActive = true;
    }

    public void TryResetWithNewBall()
    {
        if (isResetWithNewBall)
            isActive = false;
    }

    void Activate()
    {
        door.TryOpen();
        rend.material.color = activeColor;
    }

    void Deactivate()
    {
        door.Close();
        rend.material.color = DeactiveColor;
    }
}
