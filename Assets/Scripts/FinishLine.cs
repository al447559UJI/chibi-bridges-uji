using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] AudioClip finishSound;
    private bool playing = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playing)
        {
            StartCoroutine(InitializeFinishBehavior());
        }
    }

    private IEnumerator InitializeFinishBehavior()
    {
        playing = true;
        Transform origin = gameObject.transform;
        CinemachineCamera camera = GameObject.FindGameObjectWithTag("CinemachineCamera").GetComponent<CinemachineCamera>();
        camera.Follow = origin;
        SoundManager.instance.PlaySound(finishSound, origin.position);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("WinMenu");
    }
}
