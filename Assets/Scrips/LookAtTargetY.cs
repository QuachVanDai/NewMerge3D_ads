using UnityEngine;

public class LookAtTargetY : MonoBehaviour
{
   public Transform target;
    [SerializeField] float rotateSpeed = 5f;

    private void Update()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;

        // Chỉ quan tâm mặt phẳng XZ
        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }
}