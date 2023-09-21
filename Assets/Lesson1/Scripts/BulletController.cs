using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _speed;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.forward * _speed * Time.fixedDeltaTime;
        _rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 0.7f);
    }
}
