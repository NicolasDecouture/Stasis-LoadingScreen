using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerReceiver
{
    void ReceiveTriggerEnter(GameObject owner, Collider other);
}


