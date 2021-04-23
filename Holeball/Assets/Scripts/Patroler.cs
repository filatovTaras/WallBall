using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroler : MonoBehaviour
{
    [SerializeField]
    GameObject patroler = default;
    [SerializeField]
    Transform startPosTrans = default;
    [SerializeField]
    Transform endPosTrans = default;
    [SerializeField]
    float speed = 1;

    Rigidbody _rb;
    bool isToEndPos = true;

    float startTime;
    float journeyLength;
    float journeyLengthStarter;
    float journeyLengthStandart;
    float distCovered;
    float fractionOfJourney;
    float dist;

    void Awake()
    {
        _rb = patroler.GetComponent<Rigidbody>();
        startTime = Time.time;
        journeyLengthStarter = Vector3.Distance(patroler.transform.position, endPosTrans.position);
        journeyLengthStandart = Vector3.Distance(startPosTrans.position, endPosTrans.position);
        journeyLength = journeyLengthStarter;
    }

    void FixedUpdate()
    {
        Patrol();
    }

    void Patrol()
    {
        MoveToEndPos();
        MoveToStartPos();
    }

    void MoveToEndPos()
    {
        if (isToEndPos)
        {
            distCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distCovered / journeyLength;

            _rb.MovePosition(Vector3.Lerp(startPosTrans.position, endPosTrans.position, fractionOfJourney));
            dist = (endPosTrans.position - patroler.transform.position).sqrMagnitude;

            if (dist < 0.01f)
            {
                isToEndPos = false;
                startTime = Time.time;
                journeyLength = journeyLengthStandart;
            }
        }
    }

    void MoveToStartPos()
    {
        if(!isToEndPos)
        {
            distCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distCovered / journeyLength;

            _rb.MovePosition(Vector3.Lerp(endPosTrans.position, startPosTrans.position, fractionOfJourney));
            dist = (startPosTrans.position - patroler.transform.position).sqrMagnitude;

            if (dist < 0.01f)
            {
                isToEndPos = true;
                startTime = Time.time;
            }
        }
    }
}
