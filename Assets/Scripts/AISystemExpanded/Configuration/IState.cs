namespace AISystemExpanded.Configuration
{
	public interface IState
	{
		void Enter();

		StateType? Tick();
		
		void Exit();
	}
}