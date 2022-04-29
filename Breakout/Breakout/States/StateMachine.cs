using Breakout.States;
using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState { get; private set; }
    private static StateMachine stateMachine;
    public StateMachine() {
        //BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        //BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        ActiveState = GameRunning.GetInstance();
        //GameRunning.GetInstance();
    }
    public static StateMachine GetStateMachine() {
        return stateMachine ?? (stateMachine = new StateMachine());
    }
    public void SwitchState(GameStateType stateType) {
        switch (stateType) {
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.GamePaused:
                break;
            default:
                break;
        }
    }
    public void UpdateState() {
            ActiveState.UpdateState();
    }

    public void RenderState() {
        ActiveState.RenderState();
    }
    public void ProcessEvent(GameEvent gameEvent) {
        StateTransformer transformer = new StateTransformer();
        if (gameEvent.Message == "CHANGE_STATE") {
            switch ( gameEvent.Message ) {
                case "CHANGE_STATE":
                    SwitchState(transformer.TransformStringToState(gameEvent.StringArg1));
                    break;
                default:
                    break;
            }
        }
    }
    }
}