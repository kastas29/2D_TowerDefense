using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    GameObject m_Arrow;
    [SerializeField]
    float m_TimePerArrow;
    Queue<GameObject> m_Queue = new Queue<GameObject>();

    bool m_Attacking = false;
    // Update is called once per frame

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemic") EnemyInArea(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemic") m_Queue.Dequeue();
    }

    //Añado el enemigo que ha entrado en el area, si no hay está la corutina activa la inicio.
    private void EnemyInArea(GameObject enemy)
    {
        m_Queue.Enqueue(enemy);
        if (!m_Attacking) {
            m_Attacking = true;
            InvokeRepeating("_Attack", 0, m_TimePerArrow);
        } 
    }

    //Cuando haya un enemigo en la cola, intentara instanciar una flecha cada X tiempo.
    //Si la cola está vacía parara la corutina...
    private void _Attack()
    {
        if (m_Queue.Count > 0)
        {
            //instanciar flechas
            GameObject arrow = Instantiate(m_Arrow, transform.position, Quaternion.identity);
            arrow.GetComponent<Arrow>().setEnemy(m_Queue.Peek().gameObject);
        } 
        else
        {
            m_Attacking = false;
            CancelInvoke("_Attack");
        }
    }
}
