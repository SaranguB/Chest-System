using ChestSystem.Chest;

namespace ChestSystem.Commands
{
    public interface ICommand
    {
        public void Execute(Player.PlayerService playerService, ChestController chestController);
        public void Undo();
    }
}
