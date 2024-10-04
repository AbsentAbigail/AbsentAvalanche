using Deadpan.Enums.Engine.Components.Modding;
using System;
using static StatusEffectApplyX;

namespace AbsentAvalanche.Helpers
{
    internal class StatusEffectHelper
    {
        public static StatusEffectDataBuilder DefaultStatusBuilder<T>(
            string name,
            string text = null,
            bool canStack = false,
            bool canBoost = false) where T : StatusEffectData
        {
            var status = new StatusEffectDataBuilder(Absent.Instance)
                .Create<T>(name)
                .WithCanBeBoosted(canBoost)
                .WithStackable(canStack);

            if (text != null)
                status.WithText(text);

            return status;
        }

        public static StatusEffectDataBuilder DefaultApplyXBuilder<T>(
            string name,
            string text = null,
            bool canStack = false,
            bool canBoost = false,
            string effectToApply = "Snow",
            ApplyToFlags applyToFlags = ApplyToFlags.None) where T : StatusEffectApplyX
        {
            return DefaultStatusBuilder<T>(name, text, canBoost, canStack)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    var status = data as T;

                    status.effectToApply = Absent.TryGet<StatusEffectData>(effectToApply);
                    status.applyToFlags = applyToFlags;
                });
        }
    }
}