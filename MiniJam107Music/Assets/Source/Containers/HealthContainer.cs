using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContainer : Container
{
    private void Start()
    {
        this.OnEmpty += HealthContainer_OnEmpty1;
    }

    private void HealthContainer_OnEmpty1()
    {
        Destroy(this.gameObject);
    }
}
