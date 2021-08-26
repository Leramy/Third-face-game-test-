using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;
    private float speed_value;
    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void Start()
    {
        speed_value = 1;  
    }

    void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
            WanderingAI wanderingAI = _enemy.GetComponent<WanderingAI>();
            wanderingAI.OnSpeedChanged(speed_value);
        }
    }

    private void OnSpeedChanged(float value)
    {
        speed_value = value;
    }
    
}
