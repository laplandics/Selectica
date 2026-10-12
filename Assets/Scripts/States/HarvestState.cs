namespace Selectica
{
    public class HarvestState : GameState
    {
        public override void OnEnter()
        { G.Resolve<Inputs>().SwitchInputs(InputMapType.Gameplay); }

        public override void OnKeyPressed(InputValueType key)
        {
            if (key == InputValueType.MouseKeyLeft) return;
            EnterSelectionState();
        }
        
        private void EnterSelectionState() => G.Resolve<States>().ChangeState<SelectionState>();

        public override void OnExit() { }
    }
}