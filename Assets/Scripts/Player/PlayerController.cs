using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : TouchPress
{
    private ParentConstraint parentConstraint;

    [SerializeField] private bool canTurn;
    private int turnDirection = 1;

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
