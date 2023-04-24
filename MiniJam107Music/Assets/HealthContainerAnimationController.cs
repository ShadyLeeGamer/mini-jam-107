using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthContainer))]
public class HealthContainerAnimationController : MonoBehaviour
{
    private HealthContainer healthContainer;
    [SerializeField] private GameObject particleEmitterObject;

    // Start is called before the first frame update
    void Start()
    {
        healthContainer = GetComponent<HealthContainer>();
        healthContainer.OnDecrease += HealthContainer_OnDecrease;
    }

    private void HealthContainer_OnDecrease()
    {
        GameObject particle = Instantiate(particleEmitterObject);
        particle.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
