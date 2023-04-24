using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    private GameObject canvasInstance;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canvasInstance != null) return;
        canvasInstance = Instantiate(gameOverCanvas);
    }
}
