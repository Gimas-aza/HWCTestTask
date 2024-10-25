using System.Collections.Generic;
using Assets.GameCombat.Actions;
using UnityEngine;

namespace Assets.Entity
{
    [RequireComponent(typeof(EntityComponent))]
    public class InputEntity : MonoBehaviour
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
            for (var i = 0; i < _actions.Count; i++)
            {
                if (_entity.IsCanAction && i < 9 && Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    if (_actions[i].Execute())
                    {
                        _entity.IsCanAction = false;
                    }
                    else
                        Debug.Log("Can't execute action");
                }
            }
        }
    }
}
