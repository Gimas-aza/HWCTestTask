using Assets.DI;
using Assets.GameCombat.Effect;
using Assets.Entity;
using UnityEngine;

namespace Assets.GameCombat.Actions
{
    public class Regeneration : ActionCombat
    {
        [SerializeField] private int _regenerationHealth = 2;

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
            _container.Resolve<IEffectManager>().AddEffect(new RegenerationEffect
            {
                Duration = ActionTime,
                MaxDuration = ActionTime,
                Heal = _regenerationHealth
            });
            Debug.Log($"Stroke: {_container.Resolve<EntityType>()}. Regeneration");
            return true;
        }
    }
}