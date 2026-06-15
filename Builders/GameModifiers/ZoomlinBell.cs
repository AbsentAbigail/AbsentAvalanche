using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.GameSystems;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.GameModifiers;

[UsedImplicitly]
public class ZoomlinBell : IGameModifierBuilder
{
    public DataFileBuilder<GameModifierData, GameModifierDataBuilder> Builder()
    {   
        var boostBell = Absent.TryGet<GameModifierData>("BoostAllEffects");
        return new GameModifierDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Zoomlin Sun Bell")
            .WithDescription(
                $"""
                When you <redraw>, your next card is free to play
                <color=#{KeywordColours.Gray.ToHexRGB()}>(Sprites by Pelli)
                """)
            .WithBellSprite(Absent.GetBellSprite("ZoomlinBell", 0.9f))
            .WithDingerSprite(Absent.GetBellSprite("ZoomlinDinger", 0.7f))
            .WithRingSfxEvent(boostBell.ringSfxEvent)
            .WithRingSfxPitch(boostBell.ringSfxPitch)
            .WithSystemsToAdd(typeof(RedrawFreeCardSystem).AssemblyQualifiedName)
            .WithVisible()
            .WithValue(25)
            .SubscribeToAfterAllBuildEvent(Absent.AddToModifierPool);
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}