using UnityEngine.InputSystem;

public class InGameUIPanel : TouchPress
{
    protected override void OnTouchPressed(InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
    }
}
