using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class SummonPillow() : AbstractStatus<StatusEffectData>(Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    private static readonly string CardName = Pillow.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Summon Junk", Name)
            .WithTextInsert(AbstractCard.CardTag(CardName))
            .SubscribeToAfterAllBuildEvent(
                data => ((StatusEffectSummon)data).summonCard = AbsentUtils.GetCard(CardName));
    }
}