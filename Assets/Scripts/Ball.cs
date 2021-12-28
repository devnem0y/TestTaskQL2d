using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform point;
    private Rigidbody2D _rigidbody;
    public float force;
    private bool isMove = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isMove)
        {
            transform.position = new Vector2(point.position.x, point.position.y + 0.43f);

            if (Input.GetMouseButtonDown(0))
            {
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(new Vector2(0f, force));
                isMove = true;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isMove = false;
            _rigidbody.isKinematic = true;
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            transform.position = new Vector2(point.position.x, point.position.y + 0.43f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Platform"))
        {
            Vector3 hitPoint = other.contacts[0].point;
            Vector3 platformCenter = new Vector3(point.position.x, point.position.y);
            _rigidbody.velocity = Vector2.zero;

            float difference = platformCenter.x - hitPoint.x;
            if (hitPoint.x < platformCenter.x)
            {
                _rigidbody.AddForce(new Vector2(-Mathf.Abs(difference * 600), force));
            }
            else
            {
                _rigidbody.AddForce(new Vector2(Mathf.Abs(difference * 600), force));
            }
        }
    }
}
