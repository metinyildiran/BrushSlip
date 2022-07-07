using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ParentConstraint))]
public class PlayerController : TouchPress
{
    [SerializeField] private GameObject exclamationText;

    private ParentConstraint parentConstraint;

    [SerializeField] private bool canTurn;
    private int turnDirection = 1;

    public Action<GameObject> OnDirectionChanged;

    private bool isOutOfPlatform;

    protected override void Awake()
    {
        base.Awake();

        parentConstraint = GetComponent<ParentConstraint>();
    }

    protected override void Start()
    {
        base.Start();

        TurnCylinder();
    }

    private void Update()
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

    private void ToggleTurnDirection()
    {
        turnDirection *= -1;

        canTurn = !canTurn;

        parentConstraint.constraintActive = !parentConstraint.constraintActive;

        TurnCylinder();
    }

    private void TurnCylinder()
    {
        if (canTurn)
            OnDirectionChanged?.Invoke(gameObject);

        if (!canTurn)
        {
            transform.DOKill();
            return;
        }

        transform.DOKill();

        transform.DORotate(new Vector3(0, transform.rotation.eulerAngles.y - (360 * turnDirection)), 2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(-1);
    }
}
