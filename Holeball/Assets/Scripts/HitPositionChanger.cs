using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPositionChanger : MonoBehaviour
{
    [SerializeField]
    Transform endMarker = default;

    Rigidbody _rb;

    AudioSource audioSource;
    [SerializeField]
    AudioClip moveWallSound = default;

    Vector3 startPos;
    Vector3 startRot;
    Vector3 endPos;
    Vector3 endRot;

    [SerializeField]
    float speed = 4f;
    float distCovered;
    float startTime;
    float fractionOfJourney;
    float journeyLength;

    enum Positions { start, end};
    Positions crntPos = Positions.start;

    bool isMoving = false;
    bool isMoveToEnd
    {
        get
        {
            return _isMoveToEnd;
        }
        set
        {
            _isMoveToEnd = value;
            if (_isMoveToEnd) startTime = Time.time;
        }
    }
    bool _isMoveToEnd = false;

    bool isMoveToStart
    {
        get
        {
            return _isMoveToStart;
        }
        set
        {
            _isMoveToStart = value;
            if (_isMoveToStart) startTime = Time.time;
        }
    }
    bool _isMoveToStart = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        startRot = transform.rotation.eulerAngles;
        endPos = endMarker.position;
        endRot = endMarker.rotation.eulerAngles;
        journeyLength = Vector3.Distance(startPos, endPos);
    }

    void FixedUpdate()
    {
        MoveToEndPos();
        MoveToStartPos();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") return;

        if (isMoving) return;
        StartCoroutine(MoveToNextPos());
    }

    void MoveToEndPos()
    {
        if (!isMoveToEnd) return;

        distCovered = (Time.time - startTime) * speed;
        fractionOfJourney = distCovered / journeyLength;

        _rb.MovePosition(Vector3.Lerp(startPos, endPos, fractionOfJourney));
        transform.rotation = Quaternion.Euler(Vector3.Lerp(startRot, endRot, fractionOfJourney));
        
        float dist = (endPos - transform.position).sqrMagnitude;
        
        if (dist < 0.01f && transform.rotation == Quaternion.Euler(endRot))
        {
            isMoveToEnd = false;
            isMoving = false;
            crntPos = Positions.end;
        }
    }

    void MoveToStartPos()
    {
        if (!isMoveToStart) return;

        distCovered = (Time.time - startTime) * speed;
        fractionOfJourney = distCovered / journeyLength;

        _rb.MovePosition(Vector3.Lerp(endPos, startPos, fractionOfJourney));
        transform.rotation = Quaternion.Euler(Vector3.Lerp(endRot, startRot, fractionOfJourney));

        float dist = (startPos - transform.position).sqrMagnitude;
        if (dist < 0.01f && transform.rotation == Quaternion.Euler(startRot))
        {
            isMoveToStart = false;
            isMoving = false;
            crntPos = Positions.start;
        }
    }

    IEnumerator MoveToNextPos()
    {
        yield return new WaitForSeconds(0.5f);

        isMoving = true;
        if (crntPos == Positions.start)
            isMoveToEnd = true;
        else
            isMoveToStart = true;

        audioSource.PlayOneShot(moveWallSound);
    }
}
