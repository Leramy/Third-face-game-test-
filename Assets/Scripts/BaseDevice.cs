using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDevice : MonoBehaviour
{
    public float radius = 3.5f;

    void OnMouseDown()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        if(Vector3.Distance(player.position, transform.position)<radius)
        {
            Vector3 direction = transform.position - player.position;
            if(Vector3.Dot(player.forward, direction)>0.5f)
            {
                Operate();
            }
        }
    }

    public virtual void Operate()
    {

    }
}
