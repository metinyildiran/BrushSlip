using static UnityEngine.InputSystem.InputAction;

public abstract class TouchMove : TouchBase
{
    protected virtual void Start()
    {
        touchControls.Touch.Press.started += context => OnTouchMoved(context);
    }

    protected abstract void OnTouchMoved(CallbackContext context);
}
