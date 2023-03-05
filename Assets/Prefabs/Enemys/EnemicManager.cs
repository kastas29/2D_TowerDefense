using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemicManager : MonoBehaviour, IDamageable
{
    public event Action IDied;
    public List<Waypoint> waypoints;
    public Waypoint waypoint_actual;
    [SerializeField]
    int m_Hp;
    void Start()
    {
        waypoint_actual = waypoints[0];
        Vector3 direccio = (waypoint_actual.Position - this.transform.position);
        direccio.Normalize();
	    this.GetComponent<Rigidbody2D>().velocity = direccio * 1;
    }

    void Update()
    {
        if ((waypoint_actual.Position - this.transform.position).magnitude <= 0.1)
        {
            waypoints.Remove(waypoint_actual);
            if (waypoints.Count > 0)
            {
                waypoint_actual = waypoints[0];
                Vector3 vector = (waypoint_actual.Position - this.transform.position);
                vector.Normalize();
	            this.GetComponent<Rigidbody2D>().velocity = vector * 1;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void setWaypoints(List<Waypoint> llista)
    {
        this.waypoints = llista;
    }

    protected void OnCollisionExit2D(Collision2D collisionInfo)
    {
        /*
        if (collisionInfo.transform.tag != "Enemic")
        {
            Vector3 vector = (waypoint_actual.Position - this.transform.position);
            vector.Normalize();
	        this.GetComponent<Rigidbody2D>().velocity = vector * 1;
        }
        */
    }
    protected void OnCollisionStay2D(Collision2D collisionInfo)
    {
        /*
        if (collisionInfo.transform.tag == "Enemic")
        {
            Vector3 vector = (waypoint_actual.Position - this.transform.position);
            vector.Normalize();
	        this.GetComponent<Rigidbody2D>().velocity = vector * 1;
        }
        */
    }

    //Aplicar daño y comprobar vida.
    public void Damage(int damageTaken)
    {
        m_Hp -= damageTaken;
        if (m_Hp < 0) {
            IDied.Invoke();
            Destroy(this.gameObject);
        }
    }
}
