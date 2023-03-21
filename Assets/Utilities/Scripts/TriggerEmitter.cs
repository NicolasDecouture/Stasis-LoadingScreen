using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEmitter : MonoBehaviour
{
    [SerializeField] MonoBehaviour _receiver;
    [SerializeField] bool _collideOnce;

    List<Collider> _collidersDone = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (_collideOnce && _collidersDone.Contains(other))
            return;

        if (_collideOnce)
            _collidersDone.Add(other);

        if (_receiver is ITriggerReceiver receiver)
            receiver.ReceiveTriggerEnter(gameObject, other);
    }

    public void Reset()
    {
        _collidersDone.Clear();
    }
}
