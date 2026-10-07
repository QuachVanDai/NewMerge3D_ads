using UnityEngine;

public class RandomRotateY : MonoBehaviour
{
    [SerializeField] bool active = true;
    [SerializeField] float rotateSpeed = 100f;

    private int direction;

    private void OnEnable()
    {
        direction = Random.value > 0.5f ? 1 : -1;
    }

    private void Update()
    {
        if (!active)
            return;

        transform.Rotate(Vector3.up * direction * rotateSpeed * Time.deltaTime);
    }
}