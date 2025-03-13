using ChestSystem.Chest;
using ChestSystem.Player;
using System.Collections.Generic;

namespace ChestSystem.Commands
{
    public class CommandInvoker
    {
        private Dictionary<ChestController, Stack<ICommand>> chestCommandHistory = new Dictionary<ChestController, Stack<ICommand>>();
        private PlayerService playerService;

        public CommandInvoker(PlayerService playerService)
        {
            this.playerService = playerService;
        }

        public void ProcessCommands(ChestController chestController, ICommand commandToProcess)
        {
            ExecuteCommand(chestController, commandToProcess);
            RegisterCommand(chestController, commandToProcess);
        }

        private void ExecuteCommand(ChestController chestController, ICommand commandToProcess)
            => commandToProcess.Execute(playerService, chestController);

        private void RegisterCommand(ChestController chestController, ICommand commandToProcess)
        {
            if (!chestCommandHistory.ContainsKey(chestController))
            {
                chestCommandHistory[chestController] = new Stack<ICommand>();
            }
            chestCommandHistory[chestController].Push(commandToProcess);
        }

        public void Undo(ChestController chestController)
        {
            if (chestCommandHistory.ContainsKey(chestController) && chestCommandHistory[chestController].Count > 0)
            {
                chestCommandHistory[chestController].Pop().Undo();
            }
        }
    }
}
