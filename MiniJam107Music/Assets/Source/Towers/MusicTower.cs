using Melanchall.DryWetMidi.MusicTheory;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;

[RequireComponent(typeof(HealthContainer))]
[RequireComponent(typeof(BoxCollider2D))]
public class MusicTower : BaseTower
{
    [SerializeField] private NoteName noteName;

    private void Awake()
    {
        MusicController.Instance.OnNote += Attack;
        MusicController.Instance.IncreaseVolume((int)noteName);
    }

    private void Attack(Melanchall.DryWetMidi.Interaction.Note note)
    {
        if(note.NoteName == this.noteName)
        {
            base.Attack();
        }
        
    }

    private void OnDestroy()
    {
        MusicController.Instance.OnNote -= Attack;
        MusicController.Instance.DecreaseVolume((int)noteName);
    }
}
