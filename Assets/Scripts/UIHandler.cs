using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private GameObject _panelOptions;
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private GameObject _panelLose;
    [SerializeField] private GameObject _panelHUD;
    
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnOptions;
    [SerializeField] private Button _btnExit;
    
    [SerializeField] private Button _btnBackToMenuO;
    [SerializeField] private Button _btnBackToMenuW;
    [SerializeField] private Button _btnBackToMenuL;
    
    [SerializeField] private Text _scoreHUD;

    private void Awake()
    {
        _btnPlay.onClick.AddListener(OnPlay);
        _btnOptions.onClick.AddListener(OnOptions);
        _btnExit.onClick.AddListener(OnExit);
        _btnBackToMenuO.onClick.AddListener(OnBackToMenu);
        _btnBackToMenuW.onClick.AddListener(OnBackToMenu);
        _btnBackToMenuL.onClick.AddListener(OnBackToMenu);
        
        Dispatcher.OnBrickDestroy += OnScoreAdd;
        Dispatcher.OnWin += OnWin;
    }

    private void OnPlay()
    {
        _panelMenu.SetActive(false);
        _panelHUD.SetActive(true);
        Dispatcher.Send(Event.ON_START);
    }
    
    private void OnOptions()
    {
        _panelMenu.SetActive(false);
        _panelOptions.SetActive(true);
    }
    
    private void OnExit()
    {
        Application.Quit();
    }
    
    private void OnBackToMenu()
    {
        _panelOptions.SetActive(false);
        _panelWin.SetActive(false);
        _panelLose.SetActive(false);
        _panelMenu.SetActive(true);
    }

    private void OnScoreAdd()
    {
        _scoreHUD.text = $"SCORE: {GameData.instance.Score}";
    }

    private void OnWin()
    {
        _panelHUD.SetActive(false);
        _panelWin.SetActive(true);
    }

    private void OnDestroy()
    {
        Dispatcher.OnBrickDestroy -= OnScoreAdd;
        Dispatcher.OnWin -= OnWin;
    }
}
