using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndListener : MonoBehaviour
{
    private MusicController musicController;
    [SerializeField]private GameManager gameManager;

    private void Start()
    {
        musicController = FindObjectOfType<MusicController>();
        musicController.OnMusicFinish += MusicController_OnMusicFinish;
    }

    private void MusicController_OnMusicFinish()
    {
        gameManager.Won();
    }
}
