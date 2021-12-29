using UnityEngine;

[CreateAssetMenu(fileName = "New СomplexityData", menuName = "MyScriptableObject/Сomplexity Data", order = 10)]
public class СomplexityData : ScriptableObject
{
    [SerializeField] private float _platformWidth;
    public float PlatformWidth => _platformWidth;
    [SerializeField] private float _ballForce;
    public float BallForce => _ballForce;
    [SerializeField] private int _bricksSize;
    public int BricksSize => _bricksSize;
    
}
