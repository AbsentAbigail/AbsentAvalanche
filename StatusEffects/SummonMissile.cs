using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

internal class SummonMissile() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "Summon Missile";
    private const string CardName = Missile.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Summon Junk", Name)
            .WithTextInsert(CardHelper.CardTag(CardName))
            .SubscribeToAfterAllBuildEvent(
                data => ((StatusEffectSummon)data).summonCard = AbsentUtils.GetCard(CardName));
    }
}