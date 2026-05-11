using System.Collections;
using UnityEngine;

public class DestroyTimeout : MonoBehaviour
{

    [SerializeField] private float timeout;
    private Coroutine timeoutRoutine;

    void OnEnable()
    {
        timeoutRoutine = StartCoroutine(StartTimeout());
    }
    private IEnumerator StartTimeout()
    {
        yield return new WaitForSeconds(timeout);
        Destroy(gameObject);
    }

    public void Cancel()
    {
        if (timeoutRoutine != null)
        {
            StopCoroutine(timeoutRoutine);
            timeoutRoutine = null;
        }
    }

}
