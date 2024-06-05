using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;

    
    
    bool isGrounded;
    bool hasControl = true;
    
    public bool InAction { get; private set; } 

    Vector3 desiredMoveDir;
    Vector3 moveDir;
    Vector3 velocity;

    public bool IsOnLedge { get; set; }
    public LedgeData LedgeData { get; set; }

    float ySpeed;
    Quaternion targetRotation;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;
    EnvironmentScanner environmentScanner;
    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        environmentScanner = GetComponent<EnvironmentScanner>();
    }


    private void Update()
    {

       // DetectInteract();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        
        Vector3 moveInput = new Vector3(h, 0, v).normalized;

        desiredMoveDir = cameraController.PlanarRotation * moveInput;
        moveDir = desiredMoveDir;   
        if (!hasControl) return;

        velocity = Vector3.zero;

        GroundCheck();
        animator.SetBool("IsGrounded", isGrounded);
        if(isGrounded)
        {
            velocity = desiredMoveDir * moveSpeed;
            ySpeed = -0.5f;
            IsOnLedge = environmentScanner.LedgeCheck(desiredMoveDir, out LedgeData ledgeData);
            if(IsOnLedge)
            {
                LedgeData = ledgeData;
                LedgeMovement();
            }
            animator.SetFloat("moveAmount", velocity.magnitude / moveSpeed, 0.2f, Time.deltaTime);
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;

            velocity = transform.forward * moveSpeed / 2;
        }

        velocity.y = ySpeed;
        characterController.Move(velocity* Time.deltaTime);

        if (moveAmount > 0 && moveDir.magnitude > 0.2f) // ledge�� ������ 90�� �̻��� �ƴϸ� �������� �ʰ� �ߴµ�, zero�� �Ǹ� �÷��̾ ȸ���ϴ� �����ϱ� ���� magnitude��
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);



    }
    [Header("Interact")]
    public float interactDistance = 2f;
    public LayerMask interactLayer;
    public GameObject interactUI;
    public void DetectInteract()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit,interactDistance, interactLayer))
        {
            IInteractable i = hit.transform.GetComponent<IInteractable>();
            i.LookAt(hit.transform);
            if (Input.GetKeyDown(KeyCode.F))
            {
                i.Interact();
            }
        }
        
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    void LedgeMovement()
    {
        float angle = Vector3.Angle(LedgeData.surfaceHit.normal, desiredMoveDir);
        if(Vector3.Angle(desiredMoveDir, transform.forward) >= 80)
        {
            velocity = Vector3.zero;
            return;
        }
        if(angle < 90)
        {
            velocity = Vector3.zero;
            moveDir = Vector3.zero;
        }
    }
    
    public IEnumerator DoAction(string animName, MatchTargetParams matchParams, Quaternion targetRotation, bool rotate=false, float postDelay = 0f, bool mirror = false)
    {
        InAction = true;

        //�߷� �� �ݸ��� ������ ����� �ö��� ���ϹǷ�, �ϴ� �̰��� ��Ȱ��ȭ ���ִ� �ڵ�


        animator.SetBool("mirrorAction", mirror);
        animator.CrossFade(animName, 0.2f); // cross fade�� �ִϸ��̼��� �ް��ϰ� �ٲ�� ������� �ʰ� ���� �Լ��� ���� �ڿ����� �������, �ι��� �μ��� fade out �ð�
        yield return null; //  �� �������� �ѱ����ν� ��ȯ

        var animState = animator.GetNextAnimatorStateInfo(0); // 0�� ���̾��� ��ȯ������ ������.
        if(animState.IsName(animName))
        {
            Debug.LogError("�ִϸ��̼��� �������� �ʴ´�.");
        }

        float timer = 0f;
        while(timer <= animState.length)
        { // �ִϸ��̼ǵ���
            timer += Time.deltaTime;
            if(rotate)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }//�ִϸ��̼� ���ൿ�� ������Ʈ

            if (matchParams!=null)
                MatchTarget(matchParams);

            if(animator.IsInTransition(0) && timer> 1.0f) // Vault�������� �پ�Ѱ� ���߿� �����ϴ� ��ȯ���϶� �߷��� �������� ����. ���� ��ȯ���� break�ϸ� �ȵǴ� 0.5���� �������� ��������
                break;

            yield return null;
        }

        yield return new WaitForSeconds(postDelay); // �ִϸ��̼��� 2���� ����� ��� ��Ʈ�ѷ� �ѱ������ �� ������

       
        InAction = false;
    }

    void MatchTarget(MatchTargetParams mp)
    {
        if (animator.isMatchingTarget) return;

        animator.MatchTarget(mp.pos, transform.rotation, mp.bodyPart,
            new MatchTargetWeightMask(mp.posWeight, 0),// vector�� xyz�� 1�ΰ͸� ��ġ�� match��Ų��. rotation�� match�Ƚ�ų�Ŵ� 0
            mp.startTime, mp.targetTime);
    }
    public void SetControl(bool hasControl)
    {
        this.hasControl = hasControl;
        characterController.enabled = hasControl;

        if(!hasControl)
        {
            animator.SetFloat("moveAmount", 0f);
            targetRotation = transform.rotation;
        }
    }

    public bool HasControl
    {
        get => hasControl;
        set => hasControl = value;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    public float RotationSpeed => rotationSpeed;

}

public class MatchTargetParams
{
    public Vector3 pos;
    public AvatarTarget bodyPart;
    public Vector3 posWeight;
    public float startTime;
    public float targetTime;
}
