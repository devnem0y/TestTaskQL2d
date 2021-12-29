using UnityEngine;

public class Platform : MonoBehaviour
{
    private const float DEFAULT_LEFT_CLAMP = 303;
    private const float DEFAULT_RIGHT_CLAMP = 1615;
    private const float DEFAULT_PLATFORM_WIDTH_IN_PIXELS = 256;
    
    private Camera _mainCamera;
    private float _leftClamp;
    private float _rightClamp;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        
        Dispatcher.OnSetupParam += OnSetupParam;
    }

    private void Update()
    {
        if (GameData.instance.SessionState == SessionState.GAME)
        {
            Movement();
        }
    }

    private void Movement()
    {
        var mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, _leftClamp, _rightClamp);
        var mousePositionWorldX = _mainCamera.ScreenToWorldPoint(new Vector2(mousePositionPixels, 0)).x;
        transform.position = new Vector2(mousePositionWorldX, transform.position.y);
    }

    private void OnSetupParam()
    {
        var sizeX = GameData.instance.СomplexityData.PlatformWidth;
        _spriteRenderer.size = new Vector2(sizeX, _spriteRenderer.size.y);
        _boxCollider2D.size = new Vector2(sizeX, _boxCollider2D.size.y);
        
        var platformShift = (DEFAULT_PLATFORM_WIDTH_IN_PIXELS - ((DEFAULT_PLATFORM_WIDTH_IN_PIXELS / 2) * _spriteRenderer.size.x)) / 2;
        _leftClamp = DEFAULT_LEFT_CLAMP - platformShift;
        _rightClamp = DEFAULT_RIGHT_CLAMP + platformShift;
    }
    
    private void OnDestroy()
    {
        Dispatcher.OnSetupParam -= OnSetupParam;
    }
}
