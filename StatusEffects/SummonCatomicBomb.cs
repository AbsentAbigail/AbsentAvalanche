using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class SummonCatomicBomb() : AbstractStatus<StatusEffectData>(Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    private static readonly string CardName = CatomicBomb.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Summon Junk", Name)
            .WithTextInsert(AbstractCard.CardTag(CardName))
            .SubscribeToAfterAllBuildEvent(
                data => ((StatusEffectSummon)data).summonCard = AbsentUtils.GetCard(CardName));
    }
}