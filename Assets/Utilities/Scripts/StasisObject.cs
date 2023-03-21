using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisObject : MonoBehaviour
{
    Vector3 _accumulatedForce;
    float _nbHit;
    [SerializeField] Material _material;
    [SerializeField] ParticleSystem _hitParticleSystem;
    [SerializeField] ParticleSystem _stasisParticleSystem;
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _material.SetColor("_BaseColor", Color.gray);
        _material.SetColor("_EmissionColor", Color.gray);
        _material.SetFloat("_Alpha", 0);
    }

    public void OnStasis()
    {
        _stasisParticleSystem.Play();
        _material.SetFloat("_Alpha", 1);
    }

    public void AccumulatedForce(Vector3 force)
    {
        _nbHit++;
        _accumulatedForce += force;

        Color color = Color.Lerp(Color.yellow, Color.red, (float)_nbHit / 6.0f);
        _material.SetColor("_BaseColor", color);
        _material.SetColor("_EmissionColor", color);
        _hitParticleSystem.Play();
    }

    public void ReleaseForce()
    {
        _rb.AddForce(_accumulatedForce, ForceMode.Impulse);
        _accumulatedForce = new Vector3();
        _nbHit = 0;
        _material.SetColor("_BaseColor", Color.gray);
        _material.SetColor("_EmissionColor", Color.gray);
        _material.SetFloat("_Alpha", 0);
    }
}
