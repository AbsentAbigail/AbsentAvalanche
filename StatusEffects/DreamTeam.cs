using System;
using AbsentAvalanche.Keywords;
using AbsentAvalanche.Patches;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public static class DreamTeam
{
    public static StatusEffectDataBuilder[] EffectBuilders(string cardNameA, string cardNameB,
        Action<StatusEffectInstantChangeForm> ascendedModifiers = null)
    {
        FinalBossGenerationSettingsPatches.DreamTeamSwaps.Add([
                $"{cardNameA}And{cardNameB}",
                NameWhenDeployed(cardNameA, cardNameB),
                NameWhenDeployedAscended(cardNameA, cardNameB)
            ]);
        
        return [
            Instant(cardNameA, cardNameB),
            WhenDeployed(cardNameA, cardNameB),
            InstantAscended(cardNameA, cardNameB, ascendedModifiers),
            WhenDeployedAscended(cardNameA, cardNameB)
        ];
    }
    
    public static string NameInstant(string a, string b)
    {
        return $"DreamTeamInstant{a}And{b}";
    }

    public static StatusEffectDataBuilder Instant(string aName, string bName)
    {
        return new StatusEffectDataBuilder(AbsentUtils.GetModInfo().Mod)
            .Create<StatusEffectInstantChangeForm>(NameInstant(aName, bName))
            .WithStackable(true)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectInstantChangeForm)data;

                status.animation = AbsentUtils.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2").animation;
                status.phaseOptions = [AbsentUtils.GetCard(aName), AbsentUtils.GetCard(bName)];
                status.splitCount = 2;
            });
    }

    public static string NameWhenDeployed(string a, string b)
    {
        return $"DreamTeamDeploy{a}And{b}";
    }

    public static StatusEffectDataBuilder WhenDeployed(string aName, string bName)
    {
        return new StatusEffectDataBuilder(AbsentUtils.GetModInfo().Mod)
            .Create<StatusEffectApplyXWhenDeployed>(NameWhenDeployed(aName, bName))
            .WithText($"{Keywords.DreamTeam.Tag} {AbstractCard.CardTag(aName)} and {AbstractCard.CardTag(bName)}")
            .WithStackable(true)
            .WithCanBeBoosted(true) // Boostable to allow Lumin Ring
            .WithOrder(-10) // Show as first effect on card text
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectApplyXWhenDeployed)data;

                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = AbsentUtils.GetStatus(NameInstant(aName, bName));
            });
    }

    public static string NameInstantAscended(string a, string b)
    {
        return $"DreamTeamInstant{a}And{b}Ascended";
    }

    public static StatusEffectDataBuilder InstantAscended(string aName, string bName,
        Action<StatusEffectInstantChangeForm> subscribe = null)
    {
        return new StatusEffectDataBuilder(AbsentUtils.GetModInfo().Mod)
            .Create<StatusEffectInstantChangeForm>(NameInstantAscended(aName, bName))
            .WithStackable(true)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectInstantChangeForm)data;

                var finalBossEffect = AbsentUtils.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2");

                status.animation = finalBossEffect.animation;
                status.phaseOptions = [AbsentUtils.GetCard(aName), AbsentUtils.GetCard(bName)];
                status.splitCount = 2;
                status.bossTransform = new CardData.StatusEffectStacks(finalBossEffect, 1);
                status.startWithEffects =
                [
                    .. status.startWithEffects,
                    AbsentUtils.SStack("ImmuneToSnow")
                ];
            })
            .SubscribeToAfterAllBuildEvent(d => (subscribe ?? delegate { }).Invoke((StatusEffectInstantChangeForm)d));
    }

    public static string NameWhenDeployedAscended(string a, string b)
    {
        return $"DreamTeamDeploy{a}And{b}Ascended";
    }

    public static StatusEffectDataBuilder WhenDeployedAscended(string aName, string bName)
    {
        return new StatusEffectDataBuilder(AbsentUtils.GetModInfo().Mod)
            .Create<StatusEffectApplyXWhenDeployed>(NameWhenDeployedAscended(aName, bName))
            .WithText($"{Keywords.DreamTeam.Tag} evil {AbstractCard.CardTag(aName)} and evil {AbstractCard.CardTag(bName)}")
            .WithStackable(true)
            .WithCanBeBoosted(true) // Boostable to allow Lumin Ring
            .WithOrder(-10)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectApplyXWhenDeployed)data;

                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = AbsentUtils.GetStatus(NameInstantAscended(aName, bName));
            });
    }
}