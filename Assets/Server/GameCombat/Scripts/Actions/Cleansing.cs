using Assets.DI;
using Assets.GameCombat.Effect;
using Assets.Entity;
using UnityEngine;

namespace Assets.GameCombat.Actions
{
    public class Cleansing : ActionCombat
    {
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
            _container.Resolve<IEffectManager>().RemoveEffect(typeof(CombustionEffect));
            Debug.Log($"Stroke: {_container.Resolve<EntityType>()}. Cleansing");
            return true;
        }
    }
}