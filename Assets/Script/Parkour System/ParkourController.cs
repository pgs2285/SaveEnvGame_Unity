using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    [SerializeField] List<ParkourAction> parkourActions;
    [SerializeField] ParkourAction jumpDownAction;
    
    EnvironmentScanner environmentScanner;
    Animator animator;
    PlayerController playerController;

    private void Awake()
    {
        environmentScanner = GetComponent<EnvironmentScanner>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        var hitData = environmentScanner.ObstacleCheck();
        if (Input.GetButton("Jump") && !playerController.InAction)
        {
            
            if (hitData.forwardHitFound) // ���� �߰ߵȰ� �ִٸ�
            {
                foreach(ParkourAction action in parkourActions)
                {
                    if(action.CheckIfPossible(hitData, gameObject.transform)) // ������ �����ȿ� �ֳ� üũ
                    {
                        StartCoroutine(DoParkourAction(action));
                        break; 
                    }
                }

            }
        }

        if(playerController.IsOnLedge && !playerController.InAction && !hitData.forwardHitFound)
        {
            bool shouldJump = true;
            if (playerController.LedgeData.height > 1 && !Input.GetButton("Jump"))
                shouldJump = false;
            if(shouldJump && playerController.LedgeData.angle <= 50)
            {
                playerController.IsOnLedge = false;
                StartCoroutine(DoParkourAction(jumpDownAction));
            }

        }
    }
    IEnumerator DoParkourAction(ParkourAction action)
    {
        playerController.SetControl(false); //�߷� �� �ݸ��� ������ ����� �ö��� ���ϹǷ�, �ϴ� �̰��� ��Ȱ��ȭ ���ִ� �ڵ�
        MatchTargetParams matchParams = null;
        if (action.EnableTargetMatching)
        {
            matchParams = new MatchTargetParams()
            {
                pos = action.MatchPos,
                bodyPart = action.MatchBodyPart,
                posWeight = action.MatchPosWeight,
                startTime = action.MatchStartTime,
                targetTime = action.MatchTargetTime
            };
        }

        yield return playerController.DoAction(action.AnimName, matchParams, action.TargetRotation, action.RotateToObstacle,
            action.PostActionDelay, action.Mirror);
        playerController.SetControl(true); // �߷� �� collisionȰ��ȭ
    }

    void MatchTarget(ParkourAction action)
    {
        if (animator.isMatchingTarget) return;

        animator.MatchTarget(action.MatchPos, transform.rotation, action.MatchBodyPart,
            new MatchTargetWeightMask(action.MatchPosWeight, 0),// vector�� xyz�� 1�ΰ͸� ��ġ�� match��Ų��. rotation�� match�Ƚ�ų�Ŵ� 0
            action.MatchStartTime, action.MatchTargetTime);
    }
}
