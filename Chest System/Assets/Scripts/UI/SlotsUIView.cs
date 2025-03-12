using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class SlotsUIView : MonoBehaviour
    {
        private SlotsUIController SlotsUIController;
       

        public void SetSlotsUIController(SlotsUIController addSlotsUIController)
        {
            this.SlotsUIController = addSlotsUIController;
          
        }  

        public SlotsUIView GetCurrentSlotView()
        {
            return this;
        }
    }
}
