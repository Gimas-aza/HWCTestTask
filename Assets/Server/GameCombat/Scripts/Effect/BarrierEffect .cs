using Assets.DI;
using Assets.Entity;
using Assets.MVP;

namespace Assets.GameCombat.Effect
{
    public class BarrierEffect : CombatEffect
    {
        public int DamageBlocking { get; set; }
        public override int Index { get; } = 0;

        public override void Execute(DIContainer container)
        {
            var entity = container.Resolve<EntityType>();
            var uiEvents = container.Resolve<UIEvents>();

            if (Duration <= 0)
            {
                SetFlagForUndoEffect();
                return;
            }
            
            container.Resolve<IDamageBlocker>().SetBlockDamage(DamageBlocking);
            uiEvents.OnEffectDuration(entity, Index, Duration);
            Duration--;
        }
    }
}