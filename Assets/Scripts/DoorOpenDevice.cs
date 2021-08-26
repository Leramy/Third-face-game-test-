using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
     // анимация по начальной и конечной точке
    [SerializeField] private Vector3 dPos;

    private bool _open;

    private void Operate()
    {
        if(_open)
        {
            Vector3 pos = transform.position - dPos;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", pos, "time", 5f, "easytype", iTween.EaseType.linear));
           // transform.position = pos;
        }
        else
        {
            Vector3 pos = transform.position + dPos;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", pos, "time", 5f, "easytype", iTween.EaseType.linear));
           // transform.position = pos;
        }
        _open = !_open;
    }

    private void Activate()
    {
        if(!_open)
        {
            Vector3 pos = transform.position + dPos;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", pos, "time", 5f, "easytype", iTween.EaseType.linear));
            _open = true;
        }
    }

    private void Deactivate()
    {
        if (_open)
        {
            Vector3 pos = transform.position - dPos;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", pos, "time", 5f, "easytype", iTween.EaseType.linear));
            _open = false;
        }
    }
}
