using UnityEngine;

namespace ExampleProject.UI.FloatingCanvas
{
    public class LookAtCamera : MonoBehaviour
    {
        #region Fields

        private Camera _mainCamera;

        #endregion

        #region Properties



        #endregion

        #region LifeCycle

        private void Start()
        {
            _mainCamera = Camera.main;
            
            if (_mainCamera == null)
            {
                Debug.LogError("Main camera not found! Ensure the camera is tagged as 'MainCamera'.");
                enabled = false;
            }
        }

        private void LateUpdate()
        {
            if (_mainCamera != null)
            {
                transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward);
            }
        }

        #endregion

        #region Private Methods



        #endregion

        #region Public Methods



        #endregion
    }
}