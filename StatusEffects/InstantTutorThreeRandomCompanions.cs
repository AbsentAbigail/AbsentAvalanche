using System.Linq;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public class InstantTutorThreeRandomCompanions() : AbstractStatus<StatusEffectInstantTutor>(Name, subscribe: status =>
{
    status.source = StatusEffectInstantTutor.CardSource.Custom;
    status.summonCopy = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
    status.amount = 3;
    
    status.customCardList = AddressableLoader.GetGroup<CardData>("CardData")
        .Where(c =>
            c.cardType.name == "Friendly" &&
            !c.IsPet() &&
            c.mainSprite?.name != "Nothing")
        .Select(c => c.name).ToArray();
})
{
    public const string Name = "InstantTutorThreeRandomCompanions";
}