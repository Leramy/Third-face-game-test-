using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayDevice : MonoBehaviour
{
    // Start is called before the first frame update
    public void Operate()
    {
        Color random = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        GetComponent<Renderer>().material.color = random;
        Managers.Player.ChangeMoney(-10);
    }
}
