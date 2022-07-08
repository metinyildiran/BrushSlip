using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ParentConstraint))]
public class PlayerController : TouchPress
{
    [SerializeField] private GameObject exclamationText;

    public Action<GameObject> OnDirectionChanged;

    private ParentConstraint parentConstraint;

    [SerializeField] private bool canTurn;
    private int turnDirection = 1;
    private bool isOutOfPlatform;
    private bool isGameFinished;
    private bool isGameFailed;

    protected override void Awake()
    {
        base.Awake();

        parentConstraint = GetComponent<ParentConstraint>();
    }

    protected override void Start()
    {
        base.Start();

        GameManager.Instance.OnGameFinished += OnGameFinished;
        GameManager.Instance.OnGameFailed += OnGameFailed;

        TurnCylinder();
    }

    private void Update()
    {
        CheckIfInSafeZone();
    }

    private void CheckIfInSafeZone()
    {
        if (canTurn) return;

        if (!Physics.Raycast(transform.position, Vector3.down))
        {
            isOutOfPlatform = true;
            exclamationText.SetActive(true);
        }
        else
        {
            isOutOfPlatform = false;
            exclamationText.SetActive(false);
        }
    }

    protected override void OnTouchPressed(InputAction.CallbackContext context)
    {
        if (isOutOfPlatform)
        {
            GameManager.Instance.OnGameFailed?.Invoke();
        }
        else
        {
            ToggleTurnDirection();
        }
    }

    private void OnGameFinished()
    {
        isGameFinished = true;

        transform.DOKill();
    }

    private void OnGameFailed()
    {
        isGameFailed = true;

        transform.DOKill();
    }

    private void ToggleTurnDirection()
    {
        turnDirection *= -1;

        canTurn = !canTurn;

        parentConstraint.constraintActive = !parentConstraint.constraintActive;

        TurnCylinder();
    }

    private void TurnCylinder()
    {
        if (isGameFinished || isGameFailed) return;

        if (canTurn)
            OnDirectionChanged?.Invoke(gameObject);
        else
        {
            transform.DOKill();
            return;
        }

        transform.DOKill();

        transform.DORotate(new Vector3(0, transform.rotation.eulerAngles.y - (360 * turnDirection)), 2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(-1);
    }
}
