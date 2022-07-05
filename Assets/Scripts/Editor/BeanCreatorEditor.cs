#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;
using UnityEditor;
#endif
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(BeanCreator))]
public class BeanCreatorEditor : Editor
{
    BeanCreator beanCreator;

    private void Awake()
    {
        beanCreator = (BeanCreator) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Spawn On Platform"))
        {
            EditorCoroutineUtility.StartCoroutine(beanCreator.SpawnOnPlane(), this);
        }

        if (GUILayout.Button("Clear"))
        {
            beanCreator.ClearBeans();
        }
    }
}
#endif
