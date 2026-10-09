using UnityEngine;

public class UISpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private RectTransform parent;

    [Header("Settings")]
    [SerializeField] private int count = 5;
    [SerializeField] private float spacing = 100f;
    [SerializeField] private Vector2 direction = Vector2.right;

    public void Spawn()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, parent);

            RectTransform rect = obj.GetComponent<RectTransform>();

            rect.anchoredPosition = direction.normalized * spacing * i;
        }
    }
}