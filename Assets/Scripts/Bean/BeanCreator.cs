#if UNITY_EDITOR
using System.Collections;
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(MeshCollider))]
public class BeanCreator : MonoBehaviour
{
    private GameObject bean;
    private CapsuleCollider beanCollider;

    private BoxCollider seatCollider;

    private void Setup()
    {
        bean = Resources.Load<GameObject>("Prefabs/Bean");

        seatCollider = GetComponent<BoxCollider>();
        beanCollider = bean.GetComponent<CapsuleCollider>();

        transform.localScale = Vector3.one;
    }

    public IEnumerator SpawnOnPlane()
    {
        ClearBeans();

        Vector2 area = new Vector2(seatCollider.size.x / beanCollider.radius / 2, seatCollider.size.z / beanCollider.radius / 2);

        for (int i = 0; i < area.x; i++)
        {
            for (int j = 0; j < area.y; j++)
            {
                Vector3 newPos;
                newPos.x = (gameObject.transform.position.x - seatCollider.size.x / 2) + (i * beanCollider.radius * 2) + beanCollider.radius;
                newPos.y = gameObject.transform.position.y + 0.01f;
                newPos.z = (gameObject.transform.position.z - seatCollider.size.z / 2) + (j * beanCollider.radius * 2) + beanCollider.radius;

                Ray ray = new Ray(newPos, Vector3.down);
                //Debug.DrawRay(newPos, Vector3.down, Color.red, 1.0f);

                if (Physics.Raycast(ray))
                {
                    GameObject o = PrefabUtility.InstantiatePrefab(bean, gameObject.transform) as GameObject;

                    o.transform.SetPositionAndRotation(newPos, Quaternion.identity);

                    yield return null;
                }
            }
        }
    }

    public void ClearBeans()
    {
        foreach (GameObject bean in GameObject.FindGameObjectsWithTag("Bean"))
        {
            if (bean.transform.IsChildOf(transform))
            {
                DestroyImmediate(bean);
            }
        }

        Setup();
    }
}
