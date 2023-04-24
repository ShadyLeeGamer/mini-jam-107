using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private HealthContainer healthContainer;
    private void Start()
    {
        healthContainer.OnEmpty += HealthContainer_OnEmpty1;
    }

    private void HealthContainer_OnEmpty1()
    {
        Destroy(this.gameObject);
    }
}
