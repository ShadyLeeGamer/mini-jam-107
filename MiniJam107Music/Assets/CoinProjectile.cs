using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinProjectile : Projectile
{
    public int coinPerAttack = 5;
    private void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>().Add(coinPerAttack);
    }
}
