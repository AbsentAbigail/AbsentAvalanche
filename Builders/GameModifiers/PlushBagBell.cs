using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.GameSystems;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.Scripts;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.GameModifiers;

[UsedImplicitly]
public class PlushBagBell : IGameModifierBuilder
{
    public DataFileBuilder<GameModifierData, GameModifierDataBuilder> Builder()
    {   
        var boostBell = Absent.TryGet<GameModifierData>("BoostAllEffects");
        return new GameModifierDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Sun Bell of the Plush Bag")
            .WithDescription(
                $"""
                <Companions> don't follow you into battle
                Gain a crowned {Absent.CardTag(PlushBag.Name)}
                <color=#{KeywordColours.Gray.ToHexRGB()}>(Sprites by Pelli)
                """)
            .WithBellSprite(Absent.GetBellSprite("PlushBagBell", 0.9f))
            .WithDingerSprite(Absent.GetBellSprite("PlushBagDinger", 0.6f))
            .WithRingSfxEvent(boostBell.ringSfxEvent)
            .WithRingSfxPitch(boostBell.ringSfxPitch)
            .WithSystemsToAdd(typeof(StopCompanionsSystem).AssemblyQualifiedName)
            .WithStartScripts(new Script<ScriptAddCardToDeck>("Add Plush Bag to deck", null))
            .WithVisible()
            .WithValue(25)
            .SubscribeToAfterAllBuildEvent(Absent.AddToModifierPool);
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}