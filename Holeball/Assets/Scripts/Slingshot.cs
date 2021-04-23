using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField]
    LineRenderer pillar1Line = default;
    [SerializeField]
    LineRenderer pillar2Line = default;
    [SerializeField]
    AudioClip[] elasticStartAudio = new AudioClip[3];
    [SerializeField]
    AudioClip[] pushBallAudio = new AudioClip[4];
    public PushSetting pushSetting;
    [SerializeField]
    float xMin, xMax, zMin, zMax;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        pushSetting.slingshot = transform;
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        SetMaxMinDistanceToBall();
        StandartElasticPos();
    }

    public void SetMaxMinDistanceToBall()
    {
        xMin = transform.position.x - 3;
        xMax = transform.position.x + 3;
        zMin = transform.position.z - 3;
        zMax = transform.position.z + 3;

        pushSetting.xMinSlingDist = xMin;
        pushSetting.xMaxSlingDist = xMax;
        pushSetting.zMinSlingDist = zMin;
        pushSetting.zMaxSlingDist = zMax;
    }

    public void StartMoveElasticSound()
    {
        audioSource.PlayOneShot(elasticStartAudio[Random.Range(0, elasticStartAudio.Length)], 0.3f);
    }

    public void PlayFireSound()
    {
        audioSource.PlayOneShot(pushBallAudio[Random.Range(0, pushBallAudio.Length)], 1f);
    }

    public void MoveElastic()
    {
        pillar1Line.SetPosition(1, pushSetting.crntBall.transform.position);
        pillar2Line.SetPosition(1, pushSetting.crntBall.transform.position);
    }

    public void StandartElasticPos()
    {
        pillar1Line.SetPosition(0, pillar1Line.transform.position);
        pillar1Line.SetPosition(1, pillar2Line.transform.position);
        pillar2Line.SetPosition(0, pillar2Line.transform.position);
        pillar2Line.SetPosition(1, pillar1Line.transform.position);
    }
}
