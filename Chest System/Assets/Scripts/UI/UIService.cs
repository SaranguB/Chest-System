using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class UIService : MonoBehaviour
    {
        private UnlockChestSelectionUIController unlockSelectionUIController;
        [SerializeField] private UnlockChestSelectionUIView unlockSelectionUIView;
        [SerializeField] private GameObject notEnoughGemPopUp;

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
            => GameService.Instance.eventService.OnChestNotUnlockedWithGemsEvent.AddListener(EnableNotEnougGemPopUp);

        private void UnSubscribeToEvents()
           => GameService.Instance.eventService.OnChestNotUnlockedWithGemsEvent.RemoveListener(EnableNotEnougGemPopUp);

        public void CreateSlot()
        {
            GameObject slot = Instantiate(chestSlotPrefab, chestSlotContainer);
            SlotsUIView slotsUIView = slot.GetComponent<SlotsUIView>();

            slotsUIController.AddSlot(slotsUIView);
        }

        public SlotsUIController GetSlots()
            => slotsUIController;

        private UnlockChestSelectionUIController GetUnlockSlection() 
            => unlockSelectionUIController;

        public void EnableNotEnougGemPopUp()
            => notEnoughGemPopUp.SetActive(true);

        public void DisableNotEnougGemPopUp()
            => notEnoughGemPopUp.SetActive(false);
    }
}
