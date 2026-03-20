#region

using System;
using AbsentAvalanche.Patches;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

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

        return
        [
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
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantChangeForm>(NameInstant(aName, bName))
            .WithStackable(true)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectInstantChangeForm)data;

                status.animation = Absent.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2").animation;
                status.phaseOptions = [Absent.GetCard(aName), Absent.GetCard(bName)];
                status.splitCount = 2;
            });
    }

    public static string NameWhenDeployed(string a, string b)
    {
        return $"DreamTeamDeploy{a}And{b}";
    }

    public static StatusEffectDataBuilder WhenDeployed(string aName, string bName)
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenDeployed>(NameWhenDeployed(aName, bName))
            .WithText(
                $"{Absent.KeywordTag(Keywords.DreamTeam.Name)} {Absent.CardTag(aName)} and {Absent.CardTag(bName)}")
            .WithStackable(true)
            .WithCanBeBoosted(true) // Boostable to allow Lumin Ring
            .WithOrder(-10) // Show as first effect on card text
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectApplyXWhenDeployed)data;

                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Absent.GetStatus(NameInstant(aName, bName));
            });
    }

    public static string NameInstantAscended(string a, string b)
    {
        return $"DreamTeamInstant{a}And{b}Ascended";
    }

    public static StatusEffectDataBuilder InstantAscended(string aName, string bName,
        Action<StatusEffectInstantChangeForm> subscribe = null)
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantChangeForm>(NameInstantAscended(aName, bName))
            .WithStackable(true)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectInstantChangeForm)data;

                var finalBossEffect = Absent.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2");

                status.animation = finalBossEffect.animation;
                status.phaseOptions = [Absent.GetCard(aName), Absent.GetCard(bName)];
                status.splitCount = 2;
                status.bossTransform = new CardData.StatusEffectStacks(finalBossEffect, 1);
                status.startWithEffects =
                [
                    .. status.startWithEffects,
                    Absent.SStack("ImmuneToSnow")
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
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenDeployed>(NameWhenDeployedAscended(aName, bName))
            .WithText(
                $"{Absent.KeywordTag(Keywords.DreamTeam.Name)} evil {Absent.CardTag(aName)} and evil {Absent.CardTag(bName)}")
            .WithStackable(true)
            .WithCanBeBoosted(true) // Boostable to allow Lumin Ring
            .WithOrder(-10)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectApplyXWhenDeployed)data;

                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Absent.GetStatus(NameInstantAscended(aName, bName));
            });
    }
}