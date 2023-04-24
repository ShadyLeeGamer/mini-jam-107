using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    public int dropMoney = 10;
    public int speed = 10;
    public int damage = 1;
    public float attackSpeed = 0.5f;
    private bool isOnCooldown;
    private GameObject target;
    private AudioSource audioSource;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip gravitySound;
    private GameObject player;
    private HealthContainer healthContainer;
    private Animator animator;
    private bool gravityKill;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        healthContainer = GetComponent<HealthContainer>();
        healthContainer.OnEmpty += HealthContainer_OnEmpty;
        animator = GetComponent<Animator>();
    }

    private void HealthContainer_OnEmpty()
    {
        player.GetComponent<CurrencyContainer>().Add(dropMoney);
        Stats.Instance.alienKillContainer.Add(1);
        MusicController.GlobalAudioSource.PlayOneShot(deathSound);
    }

    private void FixedUpdate()
    {
        if (!gravityKill)
        {
            this.transform.position -= (Vector3)Vector2.right * 0.001f * speed;
        }
        else
        {
            this.transform.position += (Vector3)Vector2.up * 0.001f * speed;
            healthContainer.Subtract(10);
        }

    }

    public void GravityKill()
    {
        audioSource.PlayOneShot(gravitySound);
        this.gravityKill = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            collision.gameObject.GetComponent<Tower>();
            target = collision.gameObject;
        }
        catch (Exception)
        {

        }
        
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (!isOnCooldown && target != null)
        {
            try
            {
                HealthContainer health = target.GetComponent<HealthContainer>();
                health.OnEmpty += OnTargetDeath;
                health.Subtract(damage);
                audioSource.PlayOneShot(attackSound);
                animator.Play("Attack");
                StartCoroutine(CooldownCoroutine());
            }
            catch (Exception)
            {

            }

        }
    }

    private void OnTargetDeath()
    {
        target = null;
    }

    IEnumerator CooldownCoroutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(attackSpeed);
        isOnCooldown = false;
    }
    //TODO MOVE
    private void OnDestroy()
    {
        healthContainer.OnEmpty -= HealthContainer_OnEmpty;
    }
}
