using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace FSM
{
    public abstract class MonoBehaviourStateMachine<T> : MonoBehaviour
    {
        public bool DebugMode = false;

        protected StateMachine<T> stateMachine;

        protected abstract State<T> GetFirstState();
        protected abstract T GetStatesPlugScript();

        private void Awake()
        {
            stateMachine = new StateMachine<T>(this, GetStatesPlugScript());
        }

        private void Start()
        {
            stateMachine.ChangeState(GetFirstState());
        }

        private void Update()
        {
            stateMachine.Update();
        }
    }

    public class StateMachine<T>
    {
        State<T> currentState;
        private MonoBehaviourStateMachine<T> parent;
        private T refScript;

        private void DebugMessage(string message)
        {
            if (parent.DebugMode)
            {
                Debug.Log(String.Format("SM[{0}]: {1}", parent.gameObject.name, message));
            }
        }

        public StateMachine(MonoBehaviourStateMachine<T> parent, T refScript)
        {
            this.parent = parent;
            this.refScript = refScript;
        }

        public void ChangeState(State<T> newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Init(this, refScript);
            DebugMessage("Entering");
            currentState.Enter();
            DebugMessage(newState.GetType().Name);
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.ExecuteAtUpdate();
            }
        }
    }


    public abstract class State<T>
    {
        protected T plug;
        protected StateMachine<T> parent;

        public void Init(StateMachine<T> parent, T plug)
        {
            this.parent = parent;
            this.plug = plug;
        }

        public abstract void Enter();

        public virtual void ExecuteAtUpdate() { }

        public virtual void Exit() { }

        protected void ChangeState(State<T> newState)
        {
            parent.ChangeState(newState);
        }
    }
}


