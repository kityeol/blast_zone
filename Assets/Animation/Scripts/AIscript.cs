using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIscript : MonoBehaviour
{
    public Transform playerTransform;
    public Transform enemyTransform;
    public Animator anim;
    private bool wavingBack;

    void Start()
    {

    }

    void Update()
    {
        StartCoroutine(WaveBack());
    }

    private IEnumerator WaveBack()
    {
        wavingBack = true;
        if (Vector3.Distance(playerTransform.position, enemyTransform.position) < 100f)
        {
            anim.SetTrigger("WaveBack");
        }
        yield return new WaitForSeconds(3);
        wavingBack = false;
    }
}