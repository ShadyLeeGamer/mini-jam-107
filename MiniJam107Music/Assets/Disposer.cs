using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disposer : MonoBehaviour
{
    private bool isAttacking = false;
    public float speed = 50f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (isAttacking)
        {
            this.transform.position += (Vector3)Vector2.right * 0.001f * speed;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
            collision.gameObject.GetComponent<HealthContainer>().Subtract(99999999);
            this.gameObject.AddComponent<Despawn>().despawnTime = 10f;
        }
    }
}
