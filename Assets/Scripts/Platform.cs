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

    private void Awake()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
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
