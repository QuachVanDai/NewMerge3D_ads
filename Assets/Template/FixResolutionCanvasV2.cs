using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace VTLTools.FixResolution
{
    [RequireComponent(typeof(CanvasScaler))]
    public class FixResolutionCanvasV2 : MonoBehaviour
    {
        #region Properties

        [SerializeField] private bool isDebug = true;
        [SerializeField] private ScreenTemplate[] screenTemplates = null;

        [SerializeField] private CanvasScaler canvasScaler;
        [SerializeField] private Camera cam;

        private Vector2 currentScreenSize = Vector2.zero;

        //
        [SerializeField] float longScreenMatch = 1;
        [SerializeField] float shortScreenMatch = 0;
        [SerializeField] RectTransform lightInfor;
        [SerializeField] Vector2 poslightInforLandscape;
        [SerializeField] Vector2 poslightInforPortrait;
        public RectTransform thisJoystick;
        [SerializeField] Vector2 posJoyLandscape;
        [SerializeField] Vector2 posJoyPortrait;

        public Anchor[] anchors = new Anchor[4];

        #endregion Properties

        #region Unity Event

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
        }

        private void Update()
        {
            // if (Manager.Instance.GetRatioScreen() >= 1)
            // {
            //     canvasScaler.referenceResolution = new Vector2(1280, 720);
            //     canvasScaler.matchWidthOrHeight = Helpers.IsLongScreen() ? longScreenMatch : shortScreenMatch;
            //     lightInfor.anchoredPosition = poslightInforLandscape;
            //     thisJoystick.anchorMin = anchors[0].vMin;
            //     thisJoystick.anchorMax = anchors[0].vMax;
            //     thisJoystick.anchoredPosition = posJoyLandscape;
            // }
            // else
            // {
            //     canvasScaler.referenceResolution = new Vector2(720, 1280);
            //     lightInfor.anchoredPosition = poslightInforPortrait;
            //     thisJoystick.anchorMin = anchors[1].vMin;
            //     thisJoystick.anchorMax = anchors[1].vMax;
            //     thisJoystick.anchoredPosition = posJoyPortrait;
            //     Update_ScreenSize();
            // }
            Update_ScreenSize();
        }

        #endregion Unity Event

        private void Init()
        {
            canvasScaler = GetComponent<CanvasScaler>();
            currentScreenSize = GetScreenSize();
            SortFactor();
            SetWindowAspectRatio();
        }

        private void Update_ScreenSize()
        {
            Vector2 newScreenSize = GetScreenSize();

            currentScreenSize = newScreenSize;
            SetWindowAspectRatio();
        }

        private void SetResolutionFactor(ScreenTemplate factor)
        {
            canvasScaler.matchWidthOrHeight = factor.MatchWidthOrHeight;
            if (isDebug)
                Debug.Log("<color=yellow>ResolutionFactor selected: </color>" + factor.Name);
        }

        private void SetResolutionFactor_Default()
        {
            ScreenTemplate factor =
                new ScreenTemplate(cam.pixelWidth, cam.pixelHeight, canvasScaler.matchWidthOrHeight);
            //
            SetResolutionFactor(factor);
        }

        public void SetWindowAspectRatio()
        {
            int indexSelect = GetFactorIndex();
            if (indexSelect == -1)
                SetResolutionFactor_Default();
            else
                SetResolutionFactor(screenTemplates[indexSelect]);
        }

        private int GetFactorIndex()
        {
            if (screenTemplates == null || screenTemplates.Length == 0)
                return -1;
            //
            int index = -1;
            float screenAspect = currentScreenSize.x / currentScreenSize.y;
            for (int i = 0; i < screenTemplates.Length; i++)
            {
                if (screenAspect == screenTemplates[i].ScreenAspect)
                {
                    index = i;
                    break;
                }
                else if (screenAspect < screenTemplates[i].ScreenAspect)
                {
                    if (i == 0)
                    {
                        index = i;
                    }
                    else
                    {
                        if (screenAspect - screenTemplates[i - 1].ScreenAspect <
                            screenTemplates[i].ScreenAspect - screenAspect)
                            index = i - 1;
                        else
                            index = i;
                    }

                    break;
                }
            }

            //
            return index;
        }

        private Vector2 GetScreenSize()
        {
            if (cam != null)
                return new Vector2(cam.pixelWidth, cam.pixelHeight);
            return new Vector2(Screen.width, Screen.height);
        }

        public void SortFactor()
        {
            for (int i = 0; i < screenTemplates.Length - 1; i++)
            for (int j = 0; j < screenTemplates.Length - 1; j++)
            {
                if (screenTemplates[j].ScreenAspect > screenTemplates[j + 1].ScreenAspect)
                {
                    ScreenTemplate tmp = screenTemplates[j];
                    screenTemplates[j] = screenTemplates[j + 1];
                    screenTemplates[j + 1] = tmp;
                }
            }
        }

        public bool IsLongScreen()
        {
            return GetScreenRatio() < 9f / 16f;
        }

        public float GetScreenRatio()
        {
            return (float)Screen.width / (float)Screen.height;
        }
    }
}

[Serializable]
public class Anchor
{
    public Vector2 vMin;
    public Vector2 vMax;
}