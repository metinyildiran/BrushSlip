using TMPro;
using UnityEngine;

public class TextBase : MonoBehaviour
{
    protected TMP_Text text;

    protected virtual void Awake()
    {
        text = GetComponent<TMP_Text>();

        SetTextColor();

        SetText(text);
    }

    protected virtual void SetTextColor()
    {
        text.fontSharedMaterial.SetColor(ShaderID.FaceColor, ColorManager.Instance.currentColors.secondaryColor);
        text.fontSharedMaterial.SetColor(ShaderID.OutlineColor, ColorManager.Instance.currentColors.primaryColor);
    }

    protected virtual void SetText(TMP_Text tmp_text) { }
}
