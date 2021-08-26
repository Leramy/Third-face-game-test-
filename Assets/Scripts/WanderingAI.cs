using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    private bool _alive;
    [SerializeField] private GameObject fireballPrefab; 
    private GameObject _fireball;
    public const float baseSpeed = 3.0f;


    void Start()
    {
        _alive = true; 
    }

    public float speed = 3.0f; 
    public float obstacleRange = 5.0f;

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    public void OnSpeedChanged(float value)
    { 
        speed = baseSpeed * value;
    }
    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                { 
                    if (_fireball == null)
                    { 
                        _fireball = Instantiate(fireballPrefab) as GameObject; 
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }

        }
    }

    public void SetAlive(bool alive)
    { 
        _alive = alive;
    }
}