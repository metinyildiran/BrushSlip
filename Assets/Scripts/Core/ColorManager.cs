using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance { get; private set; }
    private ColorLibrary ColorLibrary;

    public ColorLibrary.Colors currentColors;

    private void Awake()
    {
        Instance = this;

        ChangeColors();
    }

    private void ChangeColors()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return;

        ColorLibrary = Resources.Load<ColorLibrary>("Color Library");

        // switch colors every 10 levels
        int index = (SceneManager.GetActiveScene().buildIndex / 10) % ColorLibrary.colors.Length;

        ColorLibrary.currentColor = ColorLibrary.colors[index];

        SetMaterialColor("M_Primary", ShaderID.BaseColor, ColorLibrary.colors[index].primaryColor);
        SetMaterialColor("M_Secondary", ShaderID.BaseColor, ColorLibrary.colors[index].secondaryColor);
        SetMaterialColor("M_Platform", ShaderID.BaseColor, ColorLibrary.colors[index].platformColor);
        SetMaterialColor("M_Sprite", ShaderID.Color, ColorLibrary.colors[index].secondaryColor);

        currentColors = ColorLibrary.colors[index];

        Resources.UnloadUnusedAssets();
    }

    private void SetMaterialColor(string matName, int nameID, Color color)
    {
        Resources.Load<Material>($"Materials/{matName}").SetColor(nameID, color);
    }
}
