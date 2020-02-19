using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : EntityHealth
{

    private bool m_IsDead;
    public bool IsDead { get => m_IsDead; }

    void Start()
    {

    }
    
    protected override void Death()
    {
        m_IsDead = m_Health <= 0;
        if (m_IsDead)
        {
            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        if (OnDeath != null)
        {
            OnDeath(gameObject);
        }

        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        
        Vector3 originalPosition = sprite.transform.position;
        for (int i = 0; i < 5; i++)
        {
            sprite.transform.position = originalPosition + new Vector3(
                Random.Range(-.3f, .3f),
                Random.Range(-.3f, .3f)
            );
            yield return new WaitForSeconds(.1f);
        }

        Destroy(this.gameObject);
    }

    void OnMouseDown()
    {
        // GameObject.FindObjectOfType<GameController>().SelectEnemy(gameObject);
    }
    
}
