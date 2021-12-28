using UnityEngine;

public class Ball : MonoBehaviour
{
    private const int SPEED = 220;
    
    [SerializeField] private Transform point;
    private Rigidbody2D _rigidbody;
    private float _force;
    private bool _isMove;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        Dispatcher.OnSetupParam += OnSetupParam;
    }

    private void OnSetupParam()
    {
        _force = GameData.instance.СomplexityData.BallForce;
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
                    _rigidbody.AddForce(new Vector2(0f, _force));
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
                _rigidbody.AddForce(new Vector2(-Mathf.Abs(difference * _force - SPEED), _force));
            }
            else
            {
                _rigidbody.AddForce(new Vector2(Mathf.Abs(difference * _force - SPEED), _force));
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

    private void OnDestroy()
    {
        Dispatcher.OnSetupParam -= OnSetupParam;
    }
}
