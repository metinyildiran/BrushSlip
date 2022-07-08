using UnityEngine;

public class TouchBase : MonoBehaviour
{
    protected TouchControls touchControls;

    protected virtual void Awake()
    {
        touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }
}
