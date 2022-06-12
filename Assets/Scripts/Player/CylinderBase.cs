using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public abstract class CylinderBase : TouchPress
{
    protected bool CanTurn { get; private set; }
    private int turnDirection;

    protected override void Awake()
    {
        base.Awake();

        turnDirection = SetCylinderTurnDirection();
        CanTurn = SetCanTurn();
    }

    protected override void Start()
    {
        base.Start();

        TurnCylinder();
    }

    protected override void OnTouchPressed(InputAction.CallbackContext context)
    {
        ToggleTurnDirection();
    }

    private void TurnCylinder()
    {
        if (!CanTurn)
        {
            transform.DOKill(false);
            return;
        }

        transform.DOKill(false);

        transform.DORotate(new Vector3(0, transform.rotation.eulerAngles.y - (360 * turnDirection)), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(-1);
    }

    private void ToggleTurnDirection()
    {
        if (turnDirection == 1)
        {
            turnDirection = -1;
        }
        else
        {
            turnDirection = 1;
        }

        if (CanTurn)
        {
            CanTurn = false;
        }
        else
        {
            CanTurn = true;
        }

        ToggleParentConstraint();

        TurnCylinder();
    }

    private void ToggleParentConstraint()
    {
        if (GetComponent<ParentConstraint>().isActiveAndEnabled)
        {
            GetComponent<ParentConstraint>().constraintActive = false;
        }

        if (!GetComponent<ParentConstraint>().isActiveAndEnabled)
        {
            GetComponent<ParentConstraint>().constraintActive = true;
        }
    }

    protected abstract int SetCylinderTurnDirection();
    protected abstract bool SetCanTurn();
}
