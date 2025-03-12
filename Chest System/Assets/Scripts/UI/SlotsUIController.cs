
using ChestSystem.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public class SlotsUIController
    {
        private SlotsUIView slotsUIView;
        private List<SlotsUIView> slotList;
        private Dictionary<SlotsUIView, bool> IsSlotAvailable;
        private SlotsUIView CurrentSlot;
        public SlotsUIController()
        {
            slotList = new List<SlotsUIView>();
            IsSlotAvailable = new Dictionary<SlotsUIView, bool>();
        }


        public void AddSlot(SlotsUIView slotsUIView)
        {
            slotList.Add(slotsUIView);
            SetIsSlotHasAChest(slotsUIView, true);
            slotsUIView.SetSlotsUIController(this);

            SetSlotPosition();
        }

        public void SetIsSlotHasAChest(SlotsUIView slotsUIView, bool value)
        {
            if (!IsSlotAvailable.ContainsKey(slotsUIView))
            {
                IsSlotAvailable.Add(slotsUIView, true);
            }
            else
            {
                IsSlotAvailable[slotsUIView] = value;
            }
        }

        public void SetSlotPosition()
        {
            slotList[slotList.Count - 1].transform.SetSiblingIndex(slotList.Count - 1);
        }

        public Transform GetChestSlotPosition()
        {
            Transform slotTransform = GetSlot();
            return slotTransform;

        }

        private Transform GetSlot()
        {
            if (slotList.Count > 0)
            {
                foreach (var slot in slotList)
                {
                    if (CheckSlotIsAvailable(slot))
                    {
                        IsSlotAvailable[slot] = false;
                        CurrentSlot = slot;
                        return slot.transform;
                    }
                }
            }
            return null;
        }

        
        private bool CheckSlotIsAvailable(SlotsUIView slot)
        {
            return IsSlotAvailable.TryGetValue(slot, out bool isAvailable) && isAvailable;
        }

        public bool CheckAnySlotAvailble()
        {
            bool isAvailable = false;
            foreach (var slot in IsSlotAvailable)
            {
                if (slot.Value == true)
                {
                    isAvailable = true;
                }
            }
            return isAvailable;
        }

        public SlotsUIView GetCurrentSlot()
        {
            return CurrentSlot;
        }
    }
}
