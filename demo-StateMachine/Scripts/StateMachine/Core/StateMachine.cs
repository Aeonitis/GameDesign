using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hiraeth.StateMachine
{
	/// <summary>
	/// The only MonoBehaviour, and the one that should be attached to
	/// any GameObject that wants to make use of this implementation.
	/// </summary>
	public class StateMachine : MonoBehaviour
	{
		[Tooltip("Set the initial state of this StateMachine")]
		[SerializeField] private ScriptableObjects.TransitionTableSO _transitionTableSO = default;

#if UNITY_EDITOR
		[Space]
		[SerializeField]
		internal Debugging.StateMachineDebugger _debugger = default;
#endif

		private readonly Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();
		internal State _currentState;

		/**
		 * It takes in a TransitionTableSO, and, during its Awake call,
		 * each ActionSO, ConditionSO, and StateSO in it is resolved into
		 * it's runtime counterpart, and the Transitions lists are generated.
		 */
		private void Awake()
		{
			_currentState = _transitionTableSO.GetInitialState(this);
			// Debug.Log("Current Actions: " + _currentState._actions.Length + "|" + _currentState._actions);
			// Debug.Log("Current Transitions: " + _currentState._transitions.Length + "|" + _currentState._transitions);
			// Debug.Log("Current _originSO.name: " + _currentState._originSO.name);
			_currentState.OnStateEnter();
#if UNITY_EDITOR
			_debugger.Awake(this);
#endif
		}

		public new bool TryGetComponent<T>(out T component) where T : Component
		{
			var type = typeof(T);
			if (!_cachedComponents.TryGetValue(type, out var value))
			{
				if (base.TryGetComponent<T>(out component))
					_cachedComponents.Add(type, component);

				return component != null;
			}

			component = (T)value;
			return true;
		}

		public T GetOrAddComponent<T>() where T : Component
		{
			if (!TryGetComponent<T>(out var component))
			{
				component = gameObject.AddComponent<T>();
				_cachedComponents.Add(typeof(T), component);
			}

			return component;
		}

		public new T GetComponent<T>() where T : Component
		{
			return TryGetComponent(out T component)
				? component : throw new InvalidOperationException($"{typeof(T).Name} not found in {name}.");
		}

		/**
		 * In Update, Actions of the current State are performed and then
		 * Conditions of every Transition of the current State are checked.
		 *
		 * The first Transition that evaluates to true gets triggered.
		 */
		private void Update()
		{
			if (_currentState.TryGetTransition(out var transitionState))
				Transition(transitionState);

			_currentState.OnUpdate();
		}

		private void Transition(State transitionState)
		{
			_currentState.OnStateExit();
			_currentState = transitionState;
			_currentState.OnStateEnter();
		}
	}
}
