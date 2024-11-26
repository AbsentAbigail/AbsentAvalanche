using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

internal class SummonPillow() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "SummonPillow";
    private const string CardName = Pillow.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Summon Junk", Name)
            .WithTextInsert(AbstractCard.CardTag(CardName))
            .SubscribeToAfterAllBuildEvent(
                data => ((StatusEffectSummon)data).summonCard = AbsentUtils.GetCard(CardName));
    }
}