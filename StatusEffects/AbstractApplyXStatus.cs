using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using System;
using static StatusEffectApplyX;

namespace AbsentAvalanche.StatusEffects
{
    internal abstract class AbstractApplyXStatus<T>(
            string name, string text = null,
            bool canStack = false, bool canBoost = false,
            string effectToApply = "Snow", ApplyToFlags applyToFlags = ApplyToFlags.None,
            Action<T> subscribe = null) : AbstractStatus<T>(name, text, canStack, canBoost, subscribe) where T : StatusEffectApplyX
    {
        protected string effectToApply = effectToApply;
        protected ApplyToFlags applyToFlags = applyToFlags;

        public override StatusEffectDataBuilder Builder()
        {
            subscribe ??= delegate { };
            return StatusEffectHelper.DefaultApplyXBuilder<T>(name, text, canStack, canBoost, effectToApply, applyToFlags)
                .SubscribeToAfterAllBuildEvent(d => subscribe.Invoke(d as T));
        }
    }
}