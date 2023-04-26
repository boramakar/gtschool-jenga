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
   
   
   public static Action OnStartPhysicsSimulation;
   public static void StartPhysicsSimulation()
   {
      OnStartPhysicsSimulation?.Invoke();
   }
   
   
   public static Action OnEndPhysicsSimulation;
   public static void EndPhysicsSimulation()
   {
      OnEndPhysicsSimulation?.Invoke();
   }
}
