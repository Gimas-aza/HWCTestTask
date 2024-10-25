using System;
using Assets.DI;
using Assets.GameCombat.Effect;
using Assets.Entity;
using UnityEngine;

namespace Assets.GameCombat.Actions
{
    public class Barrier : ActionCombat
    {
        [SerializeField] private int _damageBlocking = 5;

        private DIContainer _container;

        public override void Init(DIContainer container)
        {
            _container = container;
            CurrentCoolDown = 0;
        }

        public override bool Execute()
        {
            if (CurrentCoolDown > 0) return false;
            CurrentCoolDown = CoolDown;
            _container.Resolve<IEffectManager>().AddEffect(new BarrierEffect
            {
                Duration = ActionTime,
                MaxDuration = ActionTime,
                DamageBlocking = _damageBlocking
            });
            Debug.Log($"Stroke: {_container.Resolve<EntityType>()}. Barrier");
            return true;
        }
    }
}