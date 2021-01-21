﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayerControls.IPlayerInputActionMapActions
{
	// Assign delegate{} to events to initialise them with an empty delegate
	// so we can skip the null check when we use them

	// Gameplay
	public event UnityAction passEvent = delegate { };
	public event UnityAction passCancelledEvent = delegate { };
	public event UnityAction jumpEvent = delegate { };
	public event UnityAction jumpCancelledEvent = delegate { };
	public event UnityAction attackEvent = delegate { };
	public event UnityAction interactEvent = delegate { }; // Used to talk, pickup objects, interact with tools like the cooking cauldron
	public event UnityAction openInventoryEvent = delegate { }; // Used to bring up the inventory
	public event UnityAction pauseEvent = delegate { };
	public event UnityAction<Vector2> moveEvent = delegate { };
	public event UnityAction<Vector2, bool> cameraMoveEvent = delegate { };
	public event UnityAction enableMouseControlCameraEvent = delegate { };
	public event UnityAction disableMouseControlCameraEvent = delegate { };
	public event UnityAction startedRunning = delegate { };
	public event UnityAction stoppedRunning = delegate { };

	// Shared between menus and dialogues
	public event UnityAction moveSelectionEvent = delegate { };

	// Dialogues
	public event UnityAction advanceDialogueEvent = delegate { };

	// Menus
	public event UnityAction menuMouseMoveEvent = delegate { };
	public event UnityAction menuConfirmEvent = delegate { };
	public event UnityAction menuCancelEvent = delegate { };
	public event UnityAction menuUnpauseEvent = delegate { };
	
	private PlayerControls gameInput;

	private void OnEnable()
	{
		if (gameInput == null)
		{
			gameInput = new PlayerControls();
			gameInput.PlayerInputActionMap.SetCallbacks(this);
		}

		EnableInput();
	}

	private void OnDisable()
	{
		DisableInput();
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			attackEvent.Invoke();
	}

	public void OnOpenInventory(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			openInventoryEvent.Invoke();
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			interactEvent.Invoke();
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			jumpEvent.Invoke();

		if (context.phase == InputActionPhase.Canceled)
			jumpCancelledEvent.Invoke();
	}

	public void OnPass(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			passEvent.Invoke();

		if (context.phase == InputActionPhase.Canceled)
			passCancelledEvent.Invoke();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveEvent.Invoke(context.ReadValue<Vector2>());
	}

	public void OnRun(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				startedRunning.Invoke();
				break;
			case InputActionPhase.Canceled:
				stoppedRunning.Invoke();
				break;
		}
	}

	public void OnPause(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			pauseEvent.Invoke();
	}

	public void OnRotateCamera(InputAction.CallbackContext context)
	{
		cameraMoveEvent.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
	}

	public void OnMouseControlCamera(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			enableMouseControlCameraEvent.Invoke();

		if (context.phase == InputActionPhase.Canceled)
			disableMouseControlCameraEvent.Invoke();
	}

	private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";

	public void OnMoveSelection(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			moveSelectionEvent();
	}

	public void OnAdvanceDialogue(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			advanceDialogueEvent();
	}

	public void OnConfirm(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			menuConfirmEvent();
	}

	public void OnCancel(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			menuCancelEvent();
	}

	public void OnMouseMove(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			menuMouseMoveEvent();
	}

	public void OnUnpause(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			menuUnpauseEvent();
	}

	public void EnableInput()
	{
		gameInput.PlayerInputActionMap.Enable();
	}

	public void DisableInput()
	{
		gameInput.PlayerInputActionMap.Disable();
	}

	public bool LeftMouseDown() => Mouse.current.leftButton.isPressed;
}