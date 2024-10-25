using Assets.DI;
using Assets.GameCombat.Effect;
using Assets.Entity;
using UnityEngine;

namespace Assets.GameCombat.Actions
{
    public class Fireball : ActionCombat
    {
        [SerializeField] private int _damage = 5;
        [SerializeField] private int _damagePerTurn = 1;

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
            _container.Resolve<IGetDamage>(nameof(EntityType.Enemy)).TakeDamage(_damage);
            _container.Resolve<IEffectManager>(nameof(EntityType.Enemy)).AddEffect(new CombustionEffect
            {
                Duration = ActionTime,
                MaxDuration = ActionTime,
                Damage = _damagePerTurn
            });
            Debug.Log($"Stroke: {_container.Resolve<EntityType>()}. Damage: {_damage}");
            return true;
        }
    }
}