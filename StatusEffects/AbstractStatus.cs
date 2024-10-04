using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using System;

namespace AbsentAvalanche.StatusEffects
{
    internal class AbstractStatus<T>(
            string name, string text = null,
            bool canStack = false, bool canBoost = false,
            Action<T> subscribe = null) where T : StatusEffectData
    {
        protected readonly string name = name;
        protected readonly string text = text;
        protected readonly bool canStack = canStack;
        protected readonly bool canBoost = canBoost;
        protected Action<T> subscribe = subscribe;

        public virtual StatusEffectDataBuilder Builder()
        {
            subscribe ??= delegate { };
            return StatusEffectHelper.DefaultStatusBuilder<T>(name, text, canStack, canBoost)
                .SubscribeToAfterAllBuildEvent(d => subscribe.Invoke(d as T));
        }
    }
}