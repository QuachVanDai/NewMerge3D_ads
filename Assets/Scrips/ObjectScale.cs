using System.Collections;
using UnityEngine;

public class ObjectScale : MonoBehaviour
{
    [SerializeField] private float delay = 0.2f;
    [SerializeField] private bool isLoop = true;

    private Coroutine coroutine;

    private void OnEnable()
    {
        coroutine = StartCoroutine(ActiveChildren());
    }

    private IEnumerator ActiveChildren()
    {
        do
        {
            // Tắt tất cả object con
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            // Bật lần lượt từng object con
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(delay);
            }

        } while (isLoop);
    }

    private void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}