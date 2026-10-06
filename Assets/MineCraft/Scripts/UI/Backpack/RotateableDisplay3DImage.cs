using ExampleProject.UI.Shared;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ExampleProject.UI.Backpack
{
    public class RotateableDisplay3DImage : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        #region Fields

        public bool enableIdleRotation = false;

        [SerializeField] float rotateSpeed = 0.5f;
        [SerializeField] float acceleration = 14f;
        [SerializeField] float deceleration = 1f;
        [SerializeField] float maxRotationSpeed = 720f;
        [SerializeField] float idleRotationSpeed = 20f;
        [SerializeField] UI3DDisplay item3DDisplay;

        float targetRotation = 0;
        float currentRotationSpeed;
        bool isDragging;

        #endregion

        #region Properties



        #endregion

        #region LifeCycle   

        void Update()
        {
            if (!isDragging)
            {
                currentRotationSpeed = Mathf.MoveTowards(
                    currentRotationSpeed,
                    enableIdleRotation ? idleRotationSpeed : 0f,
                    deceleration * maxRotationSpeed * Time.unscaledDeltaTime);
            }

            targetRotation += currentRotationSpeed * Time.unscaledDeltaTime;
            item3DDisplay.RotateY(targetRotation);
        }

        #endregion

        #region Private Methods

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            float targetSpeed = -eventData.delta.x * rotateSpeed * 60f;
            targetSpeed = Mathf.Clamp(targetSpeed, -maxRotationSpeed, maxRotationSpeed);

            currentRotationSpeed = Mathf.MoveTowards(
                currentRotationSpeed,
                targetSpeed,
                acceleration * maxRotationSpeed * Time.unscaledDeltaTime);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
        }

        #endregion

        #region Public Methods



        #endregion
    }
}