using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

internal class SummonCatomicBomb() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "Summon Catomic Bomb";
    private const string CardName = CatomicBomb.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Summon Junk", Name)
            .WithTextInsert(AbstractCard.CardTag(CardName))
            .SubscribeToAfterAllBuildEvent(
                data => ((StatusEffectSummon)data).summonCard = AbsentUtils.GetCard(CardName));
    }
}