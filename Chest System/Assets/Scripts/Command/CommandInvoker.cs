using ChestSystem.Chest;
using ChestSystem.Player;
using System.Collections.Generic;

namespace ChestSystem.Commands
{
    public class CommandInvoker
    {
        private Stack<ICommand> commandRegistry = new Stack<ICommand>();
        private PlayerService playerService;

        public CommandInvoker(PlayerService playerService)
        {
            this.playerService = playerService;
        }

        public void ProcessCommands(ChestController chestController, ICommand commandToProcess)
        {
            ExecuteCommand(chestController, commandToProcess);
            RegisterCommand(commandToProcess);
        }

        private void ExecuteCommand(ChestController chestController, ICommand commandToProcess) => 
            commandToProcess.Execute(playerService, chestController);
 
        private void RegisterCommand(ICommand commandToProcess) => commandRegistry.Push(commandToProcess);

        public void Undo()
        {
            if (commandRegistry.Count != 0)
                commandRegistry.Pop().Undo();
        }
    }
}
