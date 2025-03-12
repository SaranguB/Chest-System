using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class UIService : MonoBehaviour
    {
        private UnlockChestSelectionUIController unlockSelectionUIController;
        [SerializeField] private UnlockChestSelectionUIView unlockSelectionUIView;
        [SerializeField] private GameObject notEnoughGemPopUp;
        [SerializeField] private GameObject slotNotAvailablePopUp;

        [Header("Add Chest Slot UI")]
        private SlotsUIController slotsUIController;
        [SerializeField] private GameObject chestSlotPrefab;
        [SerializeField] private Transform chestSlotContainer;
        [SerializeField] private int initialSlots;
        [SerializeField] private Button addSlotButton;
        [SerializeField] private Button generateChestButton;

        private void Start()
        {
            slotsUIController = new SlotsUIController();
            unlockSelectionUIController = new UnlockChestSelectionUIController(unlockSelectionUIView);

            createInitialSlots();
            AddListenersToButton();
            SubscribeToEvents();
        }

        private void AddListenersToButton()
        {
            addSlotButton.onClick.AddListener(CreateSlot);
            addSlotButton.onClick.AddListener(PlayButtonSound);
            generateChestButton.onClick.AddListener(()
                => GameService.Instance.chestService.GenerateChest(GetSlots(), GetUnlockSlection()));
        }

        private void createInitialSlots()
        {
            for (int i = 1; i <= initialSlots; i++)
                CreateSlot();
        }

        private void OnDisable()
            => UnSubscribeToEvents();

        private void SubscribeToEvents()
        {
            GameService.Instance.eventService.onChestNotUnlockedWithGemsEvent.AddListener(EnableNotEnougGemPopUp);
            GameService.Instance.eventService.onSlotNotAvailableEvent.AddListener(EnableSlotNotAvailable);
        }

        private void UnSubscribeToEvents()
        {
            GameService.Instance.eventService.onChestNotUnlockedWithGemsEvent.RemoveListener(EnableNotEnougGemPopUp);
            GameService.Instance.eventService.onSlotNotAvailableEvent.RemoveListener(EnableSlotNotAvailable);
        }

        public void CreateSlot()
        {
            GameObject slot = Instantiate(chestSlotPrefab, chestSlotContainer);
            SlotsUIView slotsUIView = slot.GetComponent<SlotsUIView>();

            slotsUIController.AddSlot(slotsUIView);
        }
        private void PlayButtonSound()
        {
            GameService.Instance.SoundService.PlaySound(Sounds.ButtonPressedSound);
        }

        public SlotsUIController GetSlots()
            => slotsUIController;

        private UnlockChestSelectionUIController GetUnlockSlection()
            => unlockSelectionUIController;

        public void EnableNotEnougGemPopUp()
            => notEnoughGemPopUp.SetActive(true);

        public void DisableNotEnougGemPopUp()
            => notEnoughGemPopUp.SetActive(false);

        public void EnableSlotNotAvailable()
            => slotNotAvailablePopUp.SetActive(true);

        public void DisableSlotNotAvailable()
            => slotNotAvailablePopUp.SetActive(false);
    }
}
