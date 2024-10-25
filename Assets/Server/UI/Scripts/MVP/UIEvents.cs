using System;
using Assets.Entity;

namespace Assets.MVP
{
    public class UIEvents
    {
        public event Action RestartLevel;
        public event Action<int, int, int> AbilitiesRecharge; 
        public event Action<EntityType, int> Health;
        public event Action<EntityType, int, int> EffectDuration;
        public event Action<EntityType, int, int> AddEffect;
        public event Action<EntityType, int> RemoveEffect;

        public void OnRestartLevel() => RestartLevel?.Invoke();
        public void OnAbilityRecharge(int abilityIndex, int recharge, int maxRecharge) => AbilitiesRecharge?.Invoke(abilityIndex, recharge, maxRecharge);
        public void OnHealth(EntityType entity, int health) => Health?.Invoke(entity, health);
        public void OnEffectDuration(EntityType entity, int effectIndex, int duration) => EffectDuration?.Invoke(entity, effectIndex, duration);
        public void OnAddEffect(EntityType entity, int effectIndex, int duration) => AddEffect?.Invoke(entity, effectIndex, duration);
        public void OnRemoveEffect(EntityType entity, int effectIndex) => RemoveEffect?.Invoke(entity, effectIndex);
    }
}