using UnityEngine;

public enum SoundType
{
    JUMP,
    HURT,
    SHOOT
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    [SerializeField] private AudioSource audioSourceObject;
    public static SoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySound(AudioClip sound, Vector3 origin, float volume = 1)
    {
        AudioSource audioSource = Instantiate(audioSourceObject, origin, Quaternion.identity);
        audioSource.clip = sound;
        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
