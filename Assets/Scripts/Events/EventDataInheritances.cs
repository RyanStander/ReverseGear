namespace Events
{
    //Event that informs subscribers of a debug log
    public class SendDebugLog : EventData
    {
        public readonly string Debuglog;

        public SendDebugLog(string givenLog) : base(EventType.ReceiveDebug)
        {
            Debuglog = givenLog;
        }
    }

    public class ToggleCameraEvent : EventData
    {
        public readonly bool IsPushingMode;

        public ToggleCameraEvent(bool pushing) : base(EventType.ToggleCamera)
        {
            IsPushingMode = pushing;
        }
    }

    public class PlayerCaughtEvent : EventData
    {
        public PlayerCaughtEvent() : base(EventType.PlayerCaught)
        {
            
        }
    }

    public class PlayerWinEvent : EventData
    {
        public PlayerWinEvent() : base(EventType.PlayerWin)
        {

        }
    }
}
