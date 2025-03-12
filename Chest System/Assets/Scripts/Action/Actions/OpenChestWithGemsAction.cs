using ChestSystem.Chest;
using ChestSystem.Player;

namespace ChestSystem.Actions
{
    public class OpenChestWithGemsAction : IAction
    {
        public void PerformAction()
        {
            GameService.Instance.SoundService.PlaySound(Sounds.ButtonPressedSound);
        }
    }
}
