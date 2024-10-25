using System;
using Assets.DI;
using Assets.Entity;
using UnityEngine;

namespace Assets.GameCombat.Actions
{
    public class Attack : ActionCombat
    {
        [SerializeField] private int _damage = 8;

        private DIContainer _container;

        public override void Init(DIContainer container)
        {
            _container = container;
        }

        public override bool Execute()
        {
            var entity = _container.Resolve<IGetDamage>(nameof(EntityType.Enemy));
            entity.TakeDamage(_damage);
            Debug.Log($"Stroke: {_container.Resolve<EntityType>()}. Damage: {_damage}");
            return true;
        }
    }
}