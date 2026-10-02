using System;
using UnityEngine;

public class MiniGameCamera : MonoBehaviour
{
    Camera cam;

    Camera ThisCamera
    {
        get
        {
            if (cam == null)
            {
                cam = GetComponent<Camera>();
            }

            return cam;
        }
    }

    [SerializeField] float ZoomIndex = 0.6f;
    float ScreenRatio => (float)Screen.currentResolution.height / (float)Screen.currentResolution.width;
    float hight => (float)Screen.currentResolution.height;
    float width => (float)Screen.currentResolution.width;

    [SerializeField] float defaultOrthoSize;
    [SerializeField] public float defaultFOV;


    private void Awake()
    {
        defaultOrthoSize = ThisCamera.orthographicSize;
        defaultFOV = ThisCamera.fieldOfView;
        Calculate();
    }

    private void Update()
    {
        if (Helpers.IsLongScreen())
        {
            if (ThisCamera.orthographic)
                ThisCamera.orthographicSize = defaultOrthoSize * (ScreenRatio * ZoomIndex);
            else
                ThisCamera.fieldOfView = defaultFOV * (ScreenRatio * ZoomIndex);
        }
    }

    public void Calculate()
    {
        if (Helpers.IsLongScreen())
        {
            if (ThisCamera.orthographic)
                ThisCamera.orthographicSize = defaultOrthoSize * (ScreenRatio * ZoomIndex);
            else
                ThisCamera.fieldOfView = defaultFOV * (ScreenRatio * ZoomIndex);
        }

        // DPDebug.Log($"MiniGameCamera.Calculate() - ScreenRatio: {ScreenRatio}, ZoomIndex: {ZoomIndex}, " +
        //     $"OrthographicSize: {ThisCamera.orthographicSize}, FieldOfView: {ThisCamera.fieldOfView}", this);
    }
}