using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    #region Fields

    #endregion

    #region Properties

    #endregion

    #region LifeCycle

    #endregion

    #region Private Methods

    #endregion

    #region Public Methods

    public static bool PhysicRaycast2D(Camera _cam, out RaycastHit2D _hit)
    {
        // Cast a ray from the mouse position
        _hit = Physics2D.Raycast(_cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (_hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void DestroyImmediateAllChilds(Transform go)
    {
        for (int i = go.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(go.GetChild(i).gameObject);
        }
    }

    public static bool IsLongScreen()
    {
        return (float)Screen.currentResolution.width / (float)Screen.currentResolution.height < 9f / 16f;
    }
    public static List<T> GetAllChildrenComponent<T>(Transform _parent)
    {
        List<T> _l = new();
        foreach (Transform _child in _parent.GetComponentsInChildren<Transform>(true))
        {
            if (_child.GetComponent<T>() != null)
                _l.Add(_child.GetComponent<T>());
        }
        return _l;
    }
    #endregion
}