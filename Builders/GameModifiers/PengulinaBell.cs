#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.GameSystems;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.GameModifiers;

[UsedImplicitly]
public class PengulinaBell : IGameModifierBuilder
{
    public DataFileBuilder<GameModifierData, GameModifierDataBuilder> Builder()
    {   
        var boostBell = Absent.TryGet<GameModifierData>("BoostAllEffects");
        return new GameModifierDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Sun Bell of the Pocket Hug")
            .WithDescription(
                $"""
                Apply <1><keyword=snow> to enemies when they enter battle
                <color=#{KeywordColours.Gray.ToHexRGB()}>(Sprites by Pelli)
                """)
            .WithBellSprite(Absent.GetBellSprite("PengulinaBell", 0.9f))
            .WithDingerSprite(Absent.GetBellSprite("PengulinaFeet", 0.5f))
            .WithRingSfxEvent(boostBell.ringSfxEvent)
            .WithRingSfxPitch(boostBell.ringSfxPitch)
            .WithSystemsToAdd(typeof(InitialSnowEnemiesSystem).AssemblyQualifiedName)
            .WithVisible()
            .WithValue(25)
            .SubscribeToAfterAllBuildEvent(Absent.AddToModifierPool);
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}