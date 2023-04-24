using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttackAnimNow : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoopAttack());
    }

    IEnumerator LoopAttack()
    {
        while(true)
        {
            GetComponent<Animator>().Play("Attack");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
