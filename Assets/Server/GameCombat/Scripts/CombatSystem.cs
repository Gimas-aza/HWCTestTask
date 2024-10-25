using System;
using System.Collections.Generic;
using Assets.DI;
using Assets.GameCombat.Actions;
using UnityEngine;

namespace Assets.GameCombat
{
    [Serializable]
    public class CombatSystem
    {
        [SerializeField] private List<ActionCombat> _actions;

        public void Init(DIContainer container)
        {
            foreach (var action in _actions)
            {
                action.Init(container);
            }
        }

        public void UpdateEveryTurn()
        {
            foreach (var action in _actions)
            {
                action.UpdateEveryTurn();
            }
        }

        public List<ActionCombat> GetActions() => _actions;
    }
}
