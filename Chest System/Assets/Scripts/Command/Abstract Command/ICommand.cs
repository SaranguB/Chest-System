using ChestSystem.Chest;
using UnityEngine;

namespace ChestSystem.Commands
{
    public interface ICommand
    {
        public void Execute(Player.PlayerService playerService, ChestController chestController);
        public void Undo();
    }
}
