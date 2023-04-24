using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private List<GameObject> enemies;
    private List<double> timeStamps = new List<double>();
    public static MidiFile midiFile;
    private Note[] notesLookup;
    private event Action<Note> OnNote;
    private int noteIndex = 0;
    void Start()
    {
        OnNote += Spawn;
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/spawning.mid");
        var notes = midiFile.GetNotes();
        var array = new Note[midiFile.GetNotes().Count];
        notes.CopyTo(array, 0);
        notesLookup = array;
        SetTimeStamps(array);
    }

    // Update is called once per frame
    void Update()
    {
        if (noteIndex == timeStamps.Count - 1) return;
        double timeStamp = timeStamps[noteIndex];
        if (noteIndex < timeStamps.Count)
        {
            if (MusicController.Instance.GetAudioSourceTime() >= timeStamp)
            {
                OnNote?.Invoke(notesLookup[noteIndex]);
                noteIndex++;
            }
        }
    }

    void Spawn(Note note)
    {
        int noteIndex = (int)note.NoteName % spawnpoints.Length;
        int enemyTypeIndex = note.Octave % enemies.Count;

        GameObject enemyObject = Instantiate(enemies[enemyTypeIndex]);
        enemyObject.transform.position = spawnpoints[noteIndex].position;

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

}