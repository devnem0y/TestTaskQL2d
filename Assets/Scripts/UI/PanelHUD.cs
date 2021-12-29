using UnityEngine;
using UnityEngine.UI;

public class PanelHUD : Widget
{
    [SerializeField] private Text _score;
    [SerializeField] private Text _countBall;

    private void Awake()
    {
        Dispatcher.OnBrickDestroy += OnScoreAdd;
        Dispatcher.OnSetCountBall += OnSetCountBall;
    }
    
    private void OnScoreAdd()
    {
        _score.text = $"SCORE: {GameData.instance.Score}";
    }
    
    private void OnSetCountBall()
    {
        _countBall.text = $"COUNT BALL: {GameData.instance.CountBall}";
    }

    public override void Show()
    {
        base.Show();
        OnScoreAdd();
        OnSetCountBall();
    }

    private void OnDestroy()
    {
        Dispatcher.OnBrickDestroy -= OnScoreAdd;
        Dispatcher.OnSetCountBall -= OnSetCountBall;
    }
}
