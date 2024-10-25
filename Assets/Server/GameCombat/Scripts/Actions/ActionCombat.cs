using System;
using Assets.DI;
using UnityEngine;

namespace Assets.GameCombat.Actions
{
    public abstract class ActionCombat : MonoBehaviour
    {
        [SerializeField] protected int CoolDown;
        [SerializeField] protected int ActionTime;

        protected int CurrentCoolDown;

        public abstract void Init(DIContainer container);
        public abstract bool Execute();

        public void UpdateEveryTurn()
        {
            if (CurrentCoolDown > 0)
            {
                CurrentCoolDown--;
            }
        }
    }
}