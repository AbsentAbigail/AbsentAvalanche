using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantShadeWispCopy : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return Absent.StatusCopy("Instant Summon Copy On Other Side With X Health", Name)
            .WithText("Summon a copy of an enemy on your side with <{a}><keyword=health>");
    }
}