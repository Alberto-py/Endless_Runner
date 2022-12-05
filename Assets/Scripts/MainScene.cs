using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainScene : MonoBehaviour
{
    public AudioSource clip;

    public void play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void exit()
    {
        Debug.Log("Exti...");
        Application.Quit();
    }
    public void PlaySoundButton()
    {
        clip.Play();
    }

    public void QuitMusic()
    {
        if (Camera.main.GetComponent<AudioSource>().mute)
            Camera.main.GetComponent<AudioSource>().mute = false;
        else
            Camera.main.GetComponent<AudioSource>().mute = true;
    }

    public void QuitSound()
    {
        if (clip.mute)
            clip.mute = false;
        else
            clip.mute = true;
    }
}
