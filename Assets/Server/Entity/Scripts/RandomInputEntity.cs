using System.Collections.Generic;
using Assets.GameCombat.Actions;
using UnityEngine;

namespace Assets.Entity
{
    [RequireComponent(typeof(EntityComponent))]
    public class RandomInputEntity : MonoBehaviour
    {
        private EntityComponent _entity;
        private List<ActionCombat> _actions;

        private void Awake()
        {
            _entity = GetComponent<EntityComponent>();
            _actions = _entity.CombatSystem.GetActions();
        }

        private void Update()
        {
            if (_entity.IsCanAction)
            {
                var index = Random.Range(0, _actions.Count);
                if (_actions[index].Execute())
                    _entity.IsCanAction = false;
            }
        }
    }
}