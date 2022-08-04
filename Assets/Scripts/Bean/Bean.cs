using DG.Tweening;
using UnityEngine;

public class Bean : MonoBehaviour
{
    [SerializeField] private GameObject childObject;
    private MeshRenderer _meshRenderer;

    private bool isColorized;

    private void Awake()
    {
        _meshRenderer = childObject.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        childObject.transform.DOScale(0.0f, 0.38f);

        if (!isColorized)
        {
            GameManager.Instance.DecreaseBean();
        }

        isColorized = true;

        gameObject.tag = "Untagged";
    }

    private void OnTriggerExit(Collider other)
    {
        childObject.transform.DOScale(0.8f, 0.5f).SetEase(Ease.OutBack);

        _meshRenderer.material.DOColor(ColorManager.Instance.currentColors.secondaryColor, 0.1f);
    }
}
