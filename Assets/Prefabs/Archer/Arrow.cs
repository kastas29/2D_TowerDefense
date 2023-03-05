using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    int m_ArrowDamage;
    [SerializeField]
    int m_ArrowSpeed;
    Transform m_EnemyPosition;
    IDamageable m_Enemy;

    public void setEnemy(GameObject enemy)
    {
        m_EnemyPosition = enemy.transform;
        m_Enemy = enemy.GetComponent<IDamageable>();
    }

    private void Update()
    {
        if (m_EnemyPosition != null)
        {
            Vector3 dir = m_EnemyPosition.position - transform.position;
            dir.Normalize();
            this.GetComponent<Rigidbody2D>().MovePosition(transform.position + dir * Time.deltaTime * m_ArrowSpeed);
        }
        else {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform == m_EnemyPosition)
            Hit();
    }
    private void Hit()
    {
        m_Enemy?.Damage(m_ArrowDamage);
        Destroy(this.gameObject);
    }
}
