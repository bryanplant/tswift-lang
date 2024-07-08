using System;

public static class Events
{
    private static Action _startButtonPressedAction;
    private static Action<float> _updateFpsAction;
    private static Action _viewButtonPressedAction;
    
    public static void RegisterStartButtonPressed(Action func)
    {
        _startButtonPressedAction += func;
    }
    
    public static void StartButtonPressed()
    {
        _startButtonPressedAction?.Invoke();
    }
    
    public static void RegisterViewButtonPressed(Action func)
    {
        _viewButtonPressedAction += func;
    }
    
    public static void ViewButtonPressed()
    {
        _viewButtonPressedAction?.Invoke();
    }
}