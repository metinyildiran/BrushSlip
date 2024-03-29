﻿using static UnityEngine.InputSystem.InputAction;

public abstract class TouchPress : TouchBase
{
    protected virtual void Start()
    {
        touchControls.Touch.Press.performed += context => OnTouchPressed(context);
    }

    protected abstract void OnTouchPressed(CallbackContext context);
}
