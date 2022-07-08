using UnityEngine.InputSystem;

public class FailedGameUIPanel : TouchPress
{
    protected override void OnTouchPressed(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.IsGameFinished)
        {
            LevelManager.Instance.RestartLevel();
        }
    }
}
