using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SceneObjects sceneObjects;
    public PushSetting pushSetting;
    BallMover mover;
    Slingshot slingshot;
    int layerMask = 1 << 9;

    void Start()
    {
        sceneObjects.playerController = this;
        mover = pushSetting.crntBall.GetComponent<BallMover>();
        slingshot = pushSetting.slingshot.GetComponent<Slingshot>();
    }

    void Update()
    {
        SetBallToStartPosition();
        SetBallPosition();
        PushBall();
    }

    void SetBallToStartPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(castPoint, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                if(sceneObjects.door != null)
                    sceneObjects.door.TryResetUnlockers();
                mover.SetBallStartPos(pushSetting.slingshot.position);
                pushSetting.crntBall.gameObject.SetActive(true);
                Vector3 mousePos = new Vector3(hit.point.x, 0.5f, hit.point.z);
                pushSetting.mouseBallDistance = pushSetting.slingshot.position - mousePos;
                pushSetting.aim.gameObject.SetActive(true);
                slingshot.StartMoveElasticSound();
            }
        }
    }

    void SetBallPosition() {
        if (Input.GetMouseButton(0))
        {
            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(castPoint, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                pushSetting.aimBallPos = pushSetting.mouseBallDistance + hit.point;
                mover.Move();
                slingshot.MoveElastic();
                pushSetting.aim.DrawAimLine(pushSetting.crntBall.position, pushSetting.crntBall.forward);
            }
        }
    }

    void PushBall(){
        if (Input.GetMouseButtonUp(0))
        {
            mover.Push();
            slingshot.StandartElasticPos();
            slingshot.PlayFireSound();
            pushSetting.aim.gameObject.SetActive(false);
        }
    }
}
