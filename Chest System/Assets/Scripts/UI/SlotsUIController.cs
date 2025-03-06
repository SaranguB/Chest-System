using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace ChestSystem.UI
{
    public class SlotsUIController
    {
        private SlotsUIView SlotsUIView;
        private List<SlotsUIView> slotList;
        public SlotsUIController()
        {
            slotList = new List<SlotsUIView>();
        }


        public void AddSlot(SlotsUIView slotsUIView)
        {
            slotList.Add(slotsUIView);
            slotsUIView.SetSlotsUIController(this);

            SetSlotPosition();
        }

        public void SetSlotPosition()
        {
                slotList[slotList.Count - 1].transform.SetSiblingIndex(slotList.Count - 1);
        }
    }
}
