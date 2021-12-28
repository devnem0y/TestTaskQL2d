using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform point;
    private Rigidbody2D _rigidbody;
    public float force;
    private bool _isMove;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (GameData.instance.SessionState)
        {
            case SessionState.GAME:
                if (_isMove) return;
                transform.position = new Vector2(point.position.x, point.position.y + 0.43f);

                if (Input.GetMouseButtonDown(0))
                {
                    _rigidbody.isKinematic = false;
                    _rigidbody.AddForce(new Vector2(0f, force));
                    _isMove = true;
                }
                break;
            case SessionState.WIN:
            case SessionState.LOSE:
                Reboot();
                break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.transform.CompareTag("Ground"))
        {
            Reboot();
            Dispatcher.Send(Event.ON_FAILING);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Platform"))
        {
            var hitPoint = other.contacts[0].point;
            var platformCenter = new Vector3(point.position.x, point.position.y);
            _rigidbody.velocity = Vector2.zero;

            float difference = platformCenter.x - hitPoint.x;
            if (hitPoint.x < platformCenter.x)
            {
                _rigidbody.AddForce(new Vector2(-Mathf.Abs(difference * 400), force));
            }
            else
            {
                _rigidbody.AddForce(new Vector2(Mathf.Abs(difference * 400), force));
            }
        }
    }

    private void Reboot()
    {
        _isMove = false;
        _rigidbody.isKinematic = true;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        transform.position = new Vector2(point.position.x, point.position.y + 0.43f);
    }
}
