using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public PushSetting pushSetting;
    LineRenderer _lineRenderer;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        pushSetting.aim = this;
        transform.position = new Vector3(0, 0.3f, 0);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DrawAimLine(Vector3 startPos, Vector3 direction)
    {
        if (Physics.Raycast(startPos, direction, out RaycastHit hit, Mathf.Infinity))
        {
            _lineRenderer.SetPosition(0, pushSetting.crntBall.transform.position);
            _lineRenderer.SetPosition(1, hit.point);
            pushSetting.slingshot.LookAt(hit.point);
        }
    }
}
