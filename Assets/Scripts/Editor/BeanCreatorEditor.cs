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

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        beanCreator = (BeanCreator) target;

        if (GUILayout.Button("SpawnOnPlane"))
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
