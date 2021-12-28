using System;

public class Dispatcher
{
    #region Events
    
    public static event Action OnStart;
    public static event Action OnPause;
    public static event Action OnRestart;
    public static event Action OnWin;
    public static event Action OnLose;
    public static event Action OnNextLevel;
    public static event Action OnGenerationDone;
    public static event Action OnFailing;
    public static event Action OnScoreAdd;

    #endregion

    #region ActionsEvent

    private static Action GetEvent(Event e)
    {
        switch (e)
        {
            case Event.ON_START: return OnStart;
            case Event.ON_PAUSE: return OnPause;
            case Event.ON_RESTART: return OnRestart;
            case Event.ON_WIN: return OnWin;
            case Event.ON_LOSE: return OnLose;
            case Event.ON_NEXT_LEVEL: return OnNextLevel;
            case Event.ON_FAILING: return OnFailing;
            case Event.ON_SCORE_ADD: return OnScoreAdd;
            case Event.ON_GENERATION_DONE: return OnGenerationDone;

            default: throw new ArgumentOutOfRangeException(nameof(e), e, null);
        }
    }

    #endregion

    #region ActionsEventHasParam

    private static Action<object> GetEventHasParam(Event e)
    {
        switch (e)
        {
            

            default: throw new ArgumentOutOfRangeException(nameof(e), e, null);
        }
    }

    #endregion

    #region Send

    /// <summary>
    /// Отправка события без параметров
    /// </summary>
    /// <param name="e">Событие</param>
    public static void Send(Event e)
    {
        Invoker(GetEvent(e));
    }
    
    /// <summary>
    /// Отправка события с одним параметром любого типа
    /// </summary>
    /// <param name="e">Событие</param>
    /// <param name="arg">Параметр</param>
    public static void Send(Event e, object arg)
    {
        Invoker(GetEventHasParam(e), arg);
    }
    
    /// <summary>
    /// Отправка события с массивом параметров любого типа
    /// </summary>
    /// <param name="e">Событие</param>
    /// <param name="args">Массив параметров</param>
    public static void Send(Event e, params object[] args)
    {
        Invoker(GetEventHasParam(e), args);
    }
    
    private static void Invoker(Action action)
    {
        action?.Invoke();
    }

    private static void Invoker(Action<object> action, object arg)
    {
        action?.Invoke(arg);
    }
    
    private static void Invoker(Action<object> action, params object[] args)
    {
        action?.Invoke(args);
    }

    #endregion
}

public enum Event
{
    ON_START,
    ON_PAUSE,
    ON_RESTART,
    ON_NEXT_LEVEL,
    ON_WIN,
    ON_LOSE,
    ON_FAILING,
    ON_SCORE_ADD,

    ON_GENERATION_DONE,
}