using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
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

    public void SetScale(Vector3 _scale)
    {
        this.transform.localScale = _scale;
    }
    public void SetPositionAndRotation(Vector3 _position, Quaternion _rotation)
    {
        this.transform.SetPositionAndRotation(_position, _rotation);
    }

    #endregion
}
