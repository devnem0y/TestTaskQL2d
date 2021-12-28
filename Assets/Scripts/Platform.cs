using UnityEngine;

public class Platform : MonoBehaviour
{
    private Camera _mainCamera;
    private float leftClamp;
    private float rightClamp;
    private float defaultLeftClamp = 303;
    private float defaultRightClamp = 1615;
    private float defaultPlatformWidthInPixels = 256;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        var sizeX = GameData.instance.СomplexityData.PlatformWidth;
        _spriteRenderer.size = new Vector2(sizeX, _spriteRenderer.size.y);
        _boxCollider2D.size = new Vector2(sizeX, _boxCollider2D.size.y);
        
        var platformShift = (defaultPlatformWidthInPixels - ((defaultPlatformWidthInPixels / 2) * _spriteRenderer.size.x)) / 2;
        leftClamp = defaultLeftClamp - platformShift;
        rightClamp = defaultRightClamp + platformShift;
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
        var mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        var mousePositionWorldX = _mainCamera.ScreenToWorldPoint(new Vector2(mousePositionPixels, 0)).x;
        transform.position = new Vector2(mousePositionWorldX, transform.position.y);
    }
}
