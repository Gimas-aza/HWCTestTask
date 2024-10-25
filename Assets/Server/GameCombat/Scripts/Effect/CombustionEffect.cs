using Assets.DI;
using Assets.Entity;
using Assets.MVP;

namespace Assets.GameCombat.Effect
{
    public class CombustionEffect : CombatEffect
    {
        public int Damage { get; set; }
        public override int Index { get; } = 1;

        public override void Execute(DIContainer container)
        {
            var entity = container.Resolve<EntityType>();
            var uiEvents = container.Resolve<UIEvents>();

            if (Duration <= 0)
            {
                SetFlagForUndoEffect();
                return;
            }

            container.Resolve<IGetDamage>().TakeDamage(Damage);
            uiEvents.OnEffectDuration(entity, Index, Duration);
            Duration--;
        }
    }
}