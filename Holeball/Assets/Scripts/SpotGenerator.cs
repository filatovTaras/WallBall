using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotGenerator : MonoBehaviour
{
    [SerializeField]
    AudioClip[] hitBallSpotAudio = new AudioClip[4];
    public SceneObjects sceneObjects;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            PaintOnWall(collision.GetContact(0).point);
            PaintOnFloor();

            audioSource.PlayOneShot(hitBallSpotAudio[Random.Range(0, hitBallSpotAudio.Length)], 1f);
        }
        else if (collision.gameObject.tag == "Patrol")
        {
            PaintOnFloor();
            audioSource.PlayOneShot(hitBallSpotAudio[Random.Range(0, hitBallSpotAudio.Length)], 1f);
        }
    }

    void PaintOnWall(Vector3 pos)
    {
        sceneObjects.spotPull.GetChild(0).transform.position = pos;
        sceneObjects.spotPull.GetChild(0).transform.LookAt(transform.position);
        sceneObjects.spotPull.GetChild(0).SetAsLastSibling();
    }

    void PaintOnFloor()
    {
        Vector3 pos = transform.position;
        pos.y = transform.position.y - transform.position.y + 0.1f;

        sceneObjects.spotPull.GetChild(0).transform.position = pos;
        sceneObjects.spotPull.GetChild(0).transform.rotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
        sceneObjects.spotPull.GetChild(0).SetAsLastSibling();
    }
}
