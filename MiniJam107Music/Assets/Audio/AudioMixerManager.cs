using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] string[] volumeParameter;
    [SerializeField, Range(0.0001f, 1)] float[] defaultVolume;

    public static AudioMixerManager Instance { get; set; }

    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    void Start()
    {
        RefreshVolumes();
    }

    public void RefreshVolumes()
    {
        for (int i = 0; i < volumeParameter.Length; i++)
        {
            if (volumeParameter[i] != null)
                SetVolume(i, LoadVolume(i));
        }
    }

    public void SetVolume(int index, float volume)
    {
        mixer.SetFloat(volumeParameter[index], Mathf.Log10(volume) * 20);
    }

    public float LoadVolume(int index)
    {
        return PlayerPrefs.GetFloat(volumeParameter[index], defaultVolume[index]);
    }

    public void SaveVolume(int index, float value)
    {
        PlayerPrefs.SetFloat(volumeParameter[index], value);
    }

    public float LoadDefaultVolume(int index)
    {
        return defaultVolume[index];
    }
}