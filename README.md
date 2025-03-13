Project Structure Overview
ChestSystem/

Chest/ – Handles chest logic and ChestController
StateMachine/ – Manages all chest states (Locked, Unlocking, Unlocked, Collected)
UI/ – Contains views (MonoBehaviour) and UI controllers (logic classes)
Commands/ – Implements the ICommand pattern for actions like unlocking and undoing
Services/ – Provides services like ChestService, PlayerService, and timers
Data/ – Contains ScriptableObjects and data containers
Utilis/ – Utility extensions and helpers
GameService.cs – A singleton providing centralized access to services across the game

Core Features
Chest types with configurable timers and rewards
Start timer or unlock instantly with gems
Individual slots for holding chests
Undo unlock with gems using the Command Pattern
Modular UI built with UIView (MonoBehaviour) and UIController (logic)
State Machine managing chest lifecycles
Clean separation of concerns using MVC structure
Easily extendable for new chest types or UI panels
Architecture Highlights
MVC Pattern: Clean separation between UI view and controller logic
Command Pattern: Used for actions like "Unlock with Gems" with undo support
State Pattern: Controls chest state transitions cleanly and safely
Service Locator (GameService): Centralized access to shared services
How to Use
Add chests to available slots via the UI
Unlock using timer or gems
Undo gem unlock if needed
Collect rewards after unlocking
View chest status through dynamic UI
Technologies Used
Unity Engine (C#)
TextMeshPro for UI
ScriptableObjects for data
Custom State Machine & Command Pattern