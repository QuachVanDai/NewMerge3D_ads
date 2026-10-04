using DG.Tweening;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform target;
    [SerializeField] Transform targetDefault;
    [SerializeField] float duration = 0.5f;

    Tween moveTween;
    Tween rotateTween;

    void LateUpdate()
    {
        if (target == null)
        {
            SetDefault();
            return;
        } 

        moveTween?.Kill();
        rotateTween?.Kill();

        moveTween = mainCamera.transform.DOMove(target.position, duration);
        rotateTween = mainCamera.transform.DORotate(target.eulerAngles, duration);
    }
    public void Settarget(Transform _target)
    {
        target = _target;
    }
      public void SetDefault()
    {
        target = targetDefault;
    }
}