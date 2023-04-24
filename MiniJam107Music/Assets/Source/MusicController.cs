using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public static AudioSource GlobalAudioSource { get; private set; }
    public static MidiFile midiFile;
    private List<double> timeStamps = new List<double>();
    [SerializeField] private AudioSource backgroundTrack;
    [SerializeField] private AudioClip[] audioClips;
    private Dictionary<int, AudioSource> noteDict = new Dictionary<int, AudioSource>();
    private Note[] notesLookup;
    public static MusicController Instance;
    public event Action<Note> OnNote;
    public float songDelayInSeconds;
    private int noteIndex = 0;
    public event Action OnMusicFinish;
    public AudioMixerGroup audioMixerGroupMusic, audioMixerGroupSFX;

    // Start is called before the first frame update
    void Start()
    {
        GlobalAudioSource = gameObject.AddComponent<AudioSource>();
        GlobalAudioSource.outputAudioMixerGroup = audioMixerGroupSFX;
        GlobalAudioSource.volume = 0.5f;
        Instance = this;
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (noteIndex == timeStamps.Count) return;
        if(noteIndex == timeStamps.Count - 1)
        {
            noteIndex++;
            OnMusicFinish?.Invoke();
        }
        double timeStamp = timeStamps[noteIndex];
        if (noteIndex < timeStamps.Count)
        {
            if (GetAudioSourceTime() >= timeStamp){
                OnNote?.Invoke(notesLookup[noteIndex]);
                noteIndex++;
            }
        }
    }
    public void StartGame()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/customMidi.mid");
        Debug.Log("Starting game!");
        var notes = midiFile.GetNotes();
        var array = new Note[midiFile.GetNotes().Count];
        notes.CopyTo(array, 0);
        notesLookup = array;
        SetTimeStamps(array);

        Invoke(nameof(StartAudio), songDelayInSeconds);
    }
    public void SetTimeStamps(Note[] notes)
    {
        for (int i = 0; i < notes.Length; i++)
        {
            Note note = notes[i];
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, midiFile.GetTempoMap());
            timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
        }
    }

    private void StartAudio()
    {
        for(int i = 0; i < audioClips.Length; i++)
        {
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0f;
            audioSource.outputAudioMixerGroup = audioMixerGroupMusic;
            noteDict.Add(i, audioSource);
            audioSource.clip = audioClips[i];
            audioSource.Play();
        }
    }

    public void IncreaseVolume(int key)
    {
        AudioSource audioSource = noteDict[key];
        audioSource.volume = audioSource.volume + 0.34f;
    }

    public void DecreaseVolume(int key)
    {
        AudioSource audioSource = noteDict[key];
        audioSource.volume = audioSource.volume - 0.34f;
    }
    public double GetAudioSourceTime()
    {
        return (double)backgroundTrack.time;
    }

    public bool IsFinished()
    {
        return !backgroundTrack.isPlaying;
    }
}
