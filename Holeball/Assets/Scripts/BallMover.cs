using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    public PushSetting pushSetting;
    public float posY = 0.5f;
    Rigidbody _rigidbody;
    public int koef = 10;
    [SerializeField]
    int maxForce = 50;
    TrailRenderer _trailRenderer;
    Vector3 startBallPos;

    public float force = 0;

    void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetBallStartPos(Vector3 pos)
    {
        if (_trailRenderer == null) _trailRenderer = GetComponent<TrailRenderer>();
        _trailRenderer.emitting = false;
        if (_rigidbody != null)
            _rigidbody.velocity = Vector3.zero;
        transform.position = pos;
    }

    public void Move()
    {
        Vector3 pos = pushSetting.aimBallPos;
        pos.y = posY;
        pos.x = Mathf.Clamp(pos.x, pushSetting.xMinSlingDist, pushSetting.xMaxSlingDist);
        pos.z = Mathf.Clamp(pos.z, pushSetting.zMinSlingDist, pushSetting.zMaxSlingDist);
        _rigidbody.MovePosition(pos);
        transform.LookAt(pushSetting.slingshot.position);

        startBallPos = pushSetting.slingshot.position;
        force = Vector3.Distance(startBallPos, pushSetting.aimBallPos);
        force = Mathf.Clamp(force * koef, 0, maxForce);
        pushSetting.ballForce = (int)force;
    }

    public void Push()
    {
        _rigidbody.velocity = Vector3.zero;
        pushSetting.aimBallPos.y = posY;
        transform.LookAt(startBallPos);
        _rigidbody.AddForce(_rigidbody.transform.forward * force, ForceMode.Impulse);
        _trailRenderer.emitting = true;
    }
}
