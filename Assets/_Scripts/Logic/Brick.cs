using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Coroutine destroyRoutine = null;
    public Audiomanager Audiomanager;

    private void OnCollisionEnter(Collision other)
    {
        GameManager.Instance.IncreasePoint();
        if (destroyRoutine != null) return;
        if (!other.gameObject.CompareTag("Ball")) return;
        destroyRoutine = StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        if (Audiomanager != null)
        {
            Audiomanager.PlayBreak();
        }
        yield return new WaitForSeconds(0.1f); // two physics frames to ensure proper collision
        GameManager.Instance.OnBrickDestroyed(transform.position);

        Destroy(gameObject);
    }
}
