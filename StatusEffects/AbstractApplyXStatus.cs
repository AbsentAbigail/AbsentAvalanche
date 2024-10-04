using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using System;
using static StatusEffectApplyX;

namespace AbsentAvalanche.StatusEffects
{
    internal abstract class AbstractApplyXStatus<T> : AbstractStatus<T> where T : StatusEffectApplyX
    {
        protected string effectToApply;
        protected ApplyToFlags applyToFlags;

        public AbstractApplyXStatus(
                string name, string text = null,
                bool canStack = false, bool canBoost = false,
                string effectToApply = "Snow", ApplyToFlags applyToFlags = ApplyToFlags.None,
                Action<T> subscribe = null) : base(name, text, canStack, canBoost, subscribe)
        {
            this.effectToApply = effectToApply;
            this.applyToFlags = applyToFlags;
        }

        public override StatusEffectDataBuilder Builder()
        {
            return StatusEffectHelper.DefaultApplyXBuilder<T>(name, text, canStack, canBoost, effectToApply, applyToFlags)
                .SubscribeToAfterAllBuildEvent(d => subscribe.Invoke(d as T));
        }
    }
}