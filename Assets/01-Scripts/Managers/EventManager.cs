using System;

public static class EventManager
{
   public static event Action<StackChangeDirection> OnChangeStackEvent;
   public static void ChangeStackEvent(StackChangeDirection stackChangeDirection)
   {
      OnChangeStackEvent?.Invoke(stackChangeDirection);
   }
   
   public static event Action<bool> OnToggleInputEvent;
   public static void ToggleInputEvent(bool isEnabled)
   {
      OnToggleInputEvent?.Invoke(isEnabled);
   }
}
