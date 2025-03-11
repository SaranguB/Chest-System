using System;
using UnityEngine;


namespace ChestSystem.Actions
{
    public class ActionService
    {
        private OpenChestWithGemsAction openChestWithGemsAction;
        public ActionService()
        {
            CreateActions();
        }

        private void CreateActions()
        {
            openChestWithGemsAction = new OpenChestWithGemsAction();
        }

        public OpenChestWithGemsAction GetOpenChestWithGemsAction()
        {
            return openChestWithGemsAction;
        }
    }
}
