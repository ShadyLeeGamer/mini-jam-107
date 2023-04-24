using Melanchall.DryWetMidi.MusicTheory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseTower : Tower
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float offsetX = .75f;
    [SerializeField] private float offsetY = 0f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Attack()
    {
        animator.Play("Attack");
        GameObject projectileInstance = Instantiate(projectile);
        projectileInstance.transform.position = transform.position + Vector3.right * offsetX;
        projectileInstance.transform.position = projectileInstance.transform.position + Vector3.up * offsetY;
    }
}
