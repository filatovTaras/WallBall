using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PushSetting : ScriptableObject
{
    public Transform slingshot;
    public float xMinSlingDist;
    public float xMaxSlingDist;
    public float zMinSlingDist;
    public float zMaxSlingDist;
    public Transform crntBall;
    public Aim aim;
    public Vector3 aimBallPos;
    public Vector3 mouseBallDistance;
    public int ballForce
    {
        get
        {
            return _ballForce;
        }
        set
        {
            _ballForce = value;
            Raise();
        }
    }
    int _ballForce;

    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(GameEventListener listener)
    { listeners.Remove(listener); }
}
