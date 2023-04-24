using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    public int speedX = 10;
    public int speedY = 0;
    public int damage = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += (Vector3)Vector2.right*0.001f*speedX;
        this.transform.position += (Vector3)Vector2.up * 0.001f * speedY;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Tower>() != null) return;
        try
        {
            collision.gameObject.GetComponent<HealthContainer>().Subtract(damage);
            Destroy(this.gameObject);
        }
        catch (Exception)
        {

        }

    }

}
