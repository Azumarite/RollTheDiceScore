using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    [Header("Music References")]
    public GameObject music1;          // default background
    public GameObject music2;          // event music (1 min)
    public GameObject musicGameOver;   // game over music

    private static MusicManager instance;
    private Coroutine music2Routine;

    void Awake()
    {
        // Make singleton to avoid duplicates
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        PlayDefault();
    }

    public void PlayDefault()
    {
        StopAllMusic();
        music1.SetActive(true);
    }

    public void PlayEventMusic()
    {
        if (music2Routine != null) StopCoroutine(music2Routine);
        StopAllMusic();
        music2.SetActive(true);
        music2Routine = StartCoroutine(ReturnToDefaultAfterDelay(60f));
    }

    public void PlayGameOver()
    {
        StopAllMusic();
        musicGameOver.SetActive(true);
    }

    private IEnumerator ReturnToDefaultAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayDefault();
    }

    private void StopAllMusic()
    {
        music1.SetActive(false);
        music2.SetActive(false);
        musicGameOver.SetActive(false);
    }

    // Static helpers to be called from anywhere
    public static void TriggerEventMusic()
    {
        if (instance != null) instance.PlayEventMusic();
    }

    public static void TriggerGameOverMusic()
    {
        if (instance != null) instance.PlayGameOver();
    }
}
