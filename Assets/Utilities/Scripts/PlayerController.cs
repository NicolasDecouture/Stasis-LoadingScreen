using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour , ITriggerReceiver
{
    [SerializeField] float? _stasisTimer;
    [SerializeField] StasisObject _stasisObject;
    [SerializeField] Animator _animator;
    [SerializeField] float _hitForce;
    [SerializeField] LayerMask _layerInteractable;
    [SerializeField] TriggerEmitter _triggerEmitter;

    public void OnAttack(InputValue value)
    {
        _animator.SetTrigger("attack");
        _triggerEmitter.Reset();
    }


    public void ReceiveTriggerEnter(GameObject owner, Collider other)
    {
        if (_stasisObject == null) return;

        Vector3 hitPoint = other.gameObject.GetComponent<Collider>().ClosestPoint(owner.transform.position);

        Vector3 force = other.transform.position - hitPoint;
        force.Normalize();
        force *= _hitForce;

        _stasisObject.AccumulatedForce(force);
    }

    public void OnStasis(InputValue value)
    {
        if (_stasisObject == null && value.isPressed == false)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, _layerInteractable))
            {
                _stasisObject = hit.collider.GetComponent<StasisObject>();
                _stasisObject.OnStasis();
                _stasisTimer = 8;          
            }

        }
    }

    private void Update()
    {
        if (_stasisTimer != null)
        {
            _stasisTimer -= Time.deltaTime;

            if (_stasisTimer <= 0)
            {
                _stasisObject.ReleaseForce();
                _stasisObject = null;
                _stasisTimer = null;
            }
        }
    }
}
