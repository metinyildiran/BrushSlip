using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : TouchPress
{
    private ParentConstraint parentConstraint;

    [SerializeField] private bool canTurn;
    private int turnDirection = 1;

    public Action<GameObject> OnDirectionChanged;

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

        if (!Physics.CapsuleCast(Vector3.up * 2, Vector3.down * 9, 0.5f, Vector3.up))
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    protected override void OnTouchPressed(InputAction.CallbackContext context)
    {
        ToggleTurnDirection();
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
            transform.DOKill(false);
            return;
        }

        transform.DOKill(false);

        transform.DORotate(new Vector3(0, transform.rotation.eulerAngles.y - (360 * turnDirection)), 2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(-1);
    }
}
