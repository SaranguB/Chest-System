using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{


    public class UIService : MonoBehaviour
    {
        private UnlockChesSelectionUIController unlockSelectionUIController;
        [SerializeField] private UnlockChestSelectionUIView unlockSelectionUIView;

        [Header("Add Chest Slot UI")]
        private SlotsUIController slotsUIController;
        [SerializeField] private GameObject chestSlotPrefab;
        [SerializeField] private Transform chestSlotContainer;
        [SerializeField] private int initialSlots;
        [SerializeField] private Button addSlotButton;


        [SerializeField] private Button generateChestButton;

        private void Start()
        {
            addSlotButton.onClick.AddListener(CreateSlot);
            slotsUIController = new SlotsUIController();
            unlockSelectionUIController = new UnlockChesSelectionUIController(unlockSelectionUIView);

            for (int i = 1; i <= initialSlots; i++)
                CreateSlot();

            generateChestButton.onClick.AddListener(()=> GameService.Instance.chestService.GenerateChest(GetSlots(), GetUnlockSlection()));
            SubscribeToEvents();

        }

        private void OnDisable()
        {
            UnSubscribeToEvents();
        }

        private void SubscribeToEvents()
        {

        }

        private void UnSubscribeToEvents()
        {

        }

        public void CreateSlot()
        {
            GameObject slot = Instantiate(chestSlotPrefab, chestSlotContainer);
            SlotsUIView slotsUIView = slot.GetComponent<SlotsUIView>();

            slotsUIController.AddSlot(slotsUIView);
        }

        public SlotsUIController GetSlots()
        {
            return slotsUIController;
        }

        private UnlockChesSelectionUIController GetUnlockSlection()
        {
            return unlockSelectionUIController;
        }


    }
}
