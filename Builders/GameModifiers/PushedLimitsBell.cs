#region

using System.Linq;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.CardScripts;
using AbsentAvalanche.Scriptables.Scripts;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.GameModifiers;

[UsedImplicitly]
public class PushedLimitsBell : IGameModifierBuilder
{
    public DataFileBuilder<GameModifierData, GameModifierDataBuilder> Builder()
    {   
        var boostBell = Absent.TryGet<GameModifierData>("BoostAllEffects");
        return new GameModifierDataBuilder(Absent.Instance)
            .Create("PushedLimitsBell")
            .WithTitle("Sun Bell of Pushed Limits")
            .WithDescription(
                $"Add {Absent.KeywordTag(Combo.Name)} <1> and <keyword=consume> to all <Items> in your deck")
            .WithBellSprite(Absent.GetBellSprite("BellOfPushedLimits", 0.9f))
            .WithDingerSprite(Absent.GetBellSprite("Dinger", 1.82f))
            .WithStartScripts(new Script<ScriptRunScriptsOnCardsInDeck>("Add Combo and Consume to items in deck",
                script =>
                {
                    script.constraints =
                    [
                        TargetConstraintHelper.General<TargetConstraintIsCardType>("Is Item",
                            tc => tc.allowedTypes = [Absent.GetCardType("Item")])
                    ];
                    script.scripts =
                    [
                        new Script<CardScriptComboConsume>("Card Script Add Combo and Consume", null)
                    ];
                }))
            .WithRingSfxEvent(boostBell.ringSfxEvent)
            .WithRingSfxPitch(boostBell.ringSfxPitch)
            .WithSystemsToAdd()
            .WithVisible()
            .WithValue(25)
            .SubscribeToAfterAllBuildEvent(_ =>
            {
                foreach (var classes in AddressableLoader.GetGroup<ClassData>("ClassData"))
                {
                    foreach (var pool in classes.rewardPools)
                    {
                        if (pool.type != "Modifiers")
                        {
                            continue;
                        }

                        pool.list = pool.list
                            .AddItem(Absent.TryGet<GameModifierData>("PushedLimitsBell"))
                            .ToList();
                    }
                }
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}