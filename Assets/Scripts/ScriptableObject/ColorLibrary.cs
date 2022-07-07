using UnityEngine;

[CreateAssetMenu(fileName = "Color Library", menuName = "Scriptable Objects/Color Library")]
public class ColorLibrary : ScriptableObject
{
    [HideInInspector] public Colors currentColor;

    [System.Serializable]
    public class Colors
    {
        public Color primaryColor;
        public Color secondaryColor;
        public Color backgroundColor;
        public Color platformColor;
    }

    [NonReorderable]
    public Colors[] colors;
}