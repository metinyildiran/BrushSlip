using DG.Tweening;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 offset;

    private PlayerController[] controllers;

    private void Awake()
    {
        SetPerspectiveSize();

        offset = transform.position;

        controllers = FindObjectsOfType<PlayerController>();
    }

    private void Start()
    {
        controllers[0].OnDirectionChanged += FollowPlayer;
        controllers[1].OnDirectionChanged += FollowPlayer;

        Camera.main.backgroundColor = ColorManager.Instance.currentColors.backgroundColor;
    }

    private void SetPerspectiveSize()
    {
        float currentAspect = (float) Screen.width / (float) Screen.height;
        Camera.main.fieldOfView = Mathf.Floor(1920 / currentAspect / 56f);
    }

    private void FollowPlayer(GameObject o)
    {
        transform.DOMove(LastCameraPosition(o), 0.8f).SetEase(Ease.OutSine);
    }

    private Vector3 LastCameraPosition(GameObject o)
    {
        return o.transform.position + offset;
    }
}
