
using ChestSystem.Events;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public class SlotsUIController
    {
        private SlotsUIView SlotsUIView;
        private List<SlotsUIView> slotList;
        public SlotsUIController()
        {
            slotList = new List<SlotsUIView>();
            //GameService.Instance.eventService.OnChestGeneratedEvent.AddListener(GetChestSlotPosition);
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

        public Transform GetChestSlotPosition()
        { 
            Transform slotTransform = slotList[0].transform;
            return slotTransform;

        }


    }
}
