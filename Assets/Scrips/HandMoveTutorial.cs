using System.Collections;
using UnityEngine;

public class HandMoveTutorial : MonoBehaviour
{
    public Transform hand;
    public Transform pos1;
    public Transform pos2;

    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private float waitDuration = 0.2f;

    private Coroutine moveCoroutine;

    private void OnEnable()
    {
        StartHandMove();
    }

    private void OnDisable()
    {
        StopHandMove();
    }

    public void StartHandMove()
    {
        StopHandMove();

        if (hand == null || pos1 == null || pos2 == null)
        {
            Debug.LogWarning("HandMoveTutorial requires Hand, Pos1 and Pos2 references.", this);
            return;
        }

        moveCoroutine = StartCoroutine(MoveHandLoop());
    }

    public void StopHandMove()
    {
        if (moveCoroutine == null)
            return;

        StopCoroutine(moveCoroutine);
        moveCoroutine = null;
    }

    private IEnumerator MoveHandLoop()
    {
        while (true)
        {
            hand.position = pos1.position;
            yield return MoveHand(pos2.position);

            if (waitDuration > 0f)
                yield return new WaitForSeconds(waitDuration);
        }
    }

    private IEnumerator MoveHand(Vector3 targetPosition)
    {
        Vector3 startPosition = hand.position;
        float elapsed = 0f;
        float duration = Mathf.Max(0.01f, moveDuration);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            hand.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }

        hand.position = targetPosition;
    }
}

// [SerializeField] private float moveDuration = 0.5f;
// [SerializeField] private float waitDuration = 0.2f;

// private Coroutine moveCoroutine;

// private void OnEnable()
// {
//     StartHandMove();
// }

// private void OnDisable()
// {
//     StopHandMove();
// }

// public void StartHandMove()
// {
//     StopHandMove();

//     if (hand == null || pos1 == null || pos2 == null)
//     {
//         Debug.LogWarning("HandMoveTutorial requires Hand, Pos1 and Pos2 references.", this);
//         return;
//     }

//     moveCoroutine = StartCoroutine(MoveHandLoop());
// }

// public void StopHandMove()
// {
//     if (moveCoroutine == null)
//         return;

//     StopCoroutine(moveCoroutine);
//     moveCoroutine = null;
// }

// private IEnumerator MoveHandLoop()
// {
//     hand.position = pos1.position;

//     while (true)
//     {
//         yield return MoveHand(pos2.position);
//         yield return new WaitForSeconds(waitDuration);
//         yield return MoveHand(pos1.position);
//         yield return new WaitForSeconds(waitDuration);
//     }
// }

// private IEnumerator MoveHand(Vector3 targetPosition)
// {
//     Vector3 startPosition = hand.position;
//     float elapsed = 0f;
//     float duration = Mathf.Max(0.01f, moveDuration);

//     while (elapsed < duration)
//     {
//         elapsed += Time.deltaTime;
//         hand.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
//         yield return null;
//     }

//     hand.position = targetPosition;
// }
