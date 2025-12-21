using UnityEngine;
using UnityEngine.UI;

public class SettingsSwitcher : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    [Header("UI Images")]
    public Image musicImage;
    public Image sfxImage;

    [Header("Colors")]
    public Color enabledColor = new Color(1f, 1f, 1f, 1f);
    public Color disabledColor = new Color(1f, 1f, 1f, 0.4f);

    [Header("States")]
    public bool musicOn = true;
    public bool sfxOn = true;

    private void Start()
    {
        ApplyMusicState();
        ApplySfxState();
    }

    // --------------------
    // SWITCHERS
    // --------------------

    public void SwitchMusic()
    {
        musicOn = !musicOn;
        ApplyMusicState();
    }

    public void SwitchSfx()
    {
        sfxOn = !sfxOn;
        ApplySfxState();
    }

    // --------------------
    // APPLY STATES
    // --------------------

    void ApplyMusicState()
    {
        if (musicSource != null)
            musicSource.mute = !musicOn;

        if (musicImage != null)
            musicImage.color = musicOn ? enabledColor : disabledColor;
    }

    void ApplySfxState()
    {
        foreach (AudioSource sfx in sfxSources)
        {
            if (sfx != null)
                sfx.mute = !sfxOn;
        }

        if (sfxImage != null)
            sfxImage.color = sfxOn ? enabledColor : disabledColor;
    }
}
