using UnityEngine;
using UnityEngine.Audio;

// Centralized audio controller responsible for playing ambient sounds,
// sound effects, interaction audio, and managing audio mixer volumes.

public class AudioManager : MonoBehaviour
{
    // Audio sources responsible for different types of sounds
    public AudioSource AmbientSource;
    public AudioSource SFXSource;
    public AudioSource HackingSource;
    public AudioSource StepsSource;
    [Header("Clips")] // Audio clips used in the game
    public AudioClip background;
    public AudioClip cabinet;
    public AudioClip steps;
    public AudioClip pickup;
    public AudioClip keyboard;
    public AudioClip trash;
    public AudioClip printer;
    public AudioClip click;
    public AudioClip notebook;
    public AudioClip hackingMusic;
    [Header("Mixers")] // Reference to the AudioMixer used to control volume levels
    public AudioMixer mixer;



    private void Start()
    {
        // Start ambient background music when the scene loads
        AmbientSource.clip = background;
        AmbientSource.Play();
    }

    public void StopAmbient()
    {
        // Stops ambient music if it is currently playing
        if (AmbientSource != null && AmbientSource.isPlaying)
        {
            AmbientSource.Stop();
            AmbientSource.clip = null;
            AmbientSource.loop = false;
        }
    }

    
    
    public void PlayCabinetSound()
    {
        // Plays cabinet interaction sound
        if (cabinet != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(cabinet);
        }
    }

    public void PlayPickupSound()
    {
        // Plays sound when player picks up an object
        if (pickup != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(pickup);
        }
    }

    public void PlayKeyboardSound()
    {
        // Plays typing sound effect
        if (keyboard != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(keyboard);
        }
    }

    public void PlayTrashSound()
    {
        // Plays sound when interacting with trash
        if (trash != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(trash);
        }
    }

    public void PlayPrinterSound()
    {
        // Plays printer sound effect
        if (printer != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(printer);
        }
    }

    public void PlayClickSound()
    {
        // Plays UI click sound
        if (click != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(click);
        }
    }

    public void PlayNotebookSound()
    {
        // Plays notebook interaction sound
        if (notebook != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(notebook);
        }
    }

    public void MuteSteps()
    {
        // Mutes footstep sounds using the AudioMixer
        mixer.SetFloat("StepsVolume", -80f); 
    }

    public void UnmuteSteps()
    {
        // Restores footstep sound volume
        mixer.SetFloat("StepsVolume", 0f); 
    }

    public void PlayHackingSound()
    {
        // Plays hacking background music in a loop
        if (hackingMusic != null && HackingSource != null)
        {
            HackingSource.clip = hackingMusic;
            HackingSource.loop = true; 
            HackingSource.Play();
        }
    }

    public void StopHackingSound()
    {
        // Stops hacking music
        if (HackingSource != null && HackingSource.isPlaying)
        {
            HackingSource.Stop();
            HackingSource.clip = null;
            HackingSource.loop = false;
        }
    }

    public void StopSFX()
    {
        // Stops currently playing SFX sounds
        if (SFXSource != null && SFXSource.isPlaying)
        {
            SFXSource.Stop();
            SFXSource.clip = null;
            SFXSource.loop = false;
        }
    }

}
