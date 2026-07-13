using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class Invitation : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInvitation>(Name)
            .WithText("""
                      Invitation for {0}. Make them flee the battle
                      Single use!
                      """)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInvitation>(status =>
            {
                status.effectToApply = Absent.GetStatus("Escape");
                status.textInsert = "no one...";
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}