using System;
using UnityEngine;

namespace ExampleProject.UI.Shared
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIHideable : MonoBehaviour
    {
        #region Fields

        readonly CanvasGroup canvasGroup;

        #endregion

        #region Properties

        CanvasGroup CanvasGroup => canvasGroup != null ? canvasGroup : GetComponent<CanvasGroup>();

        #endregion

        #region LifeCycle   

        void OnEnable()
        {
            EventDispatcher.Instance.AddListener(EventName.OnStartCreativeServe, OnStartCreativeServeListener);
            SetCanvasGroupAlpha();
        }
        void OnDisable()
        {
            EventDispatcher.Instance.RemoveListener(EventName.OnStartCreativeServe, OnStartCreativeServeListener);
        }


        #endregion

        #region Private Methods

        void OnStartCreativeServeListener(EventName key, object data)
        {
            SetCanvasGroupAlpha();
        }
        void SetCanvasGroupAlpha()
        {
            CanvasGroup.alpha = 1;
        }

        #endregion

        #region Public Methods



        #endregion
    }
}