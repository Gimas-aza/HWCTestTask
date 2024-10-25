using System;
using Assets.GameCombat.Effect;

namespace Assets.Entity
{
    public interface IEffectManager
    {
        void AddEffect(CombatEffect effect);
        void RemoveEffect(CombatEffect effect);
        void RemoveEffect(Type effect);
    }
}