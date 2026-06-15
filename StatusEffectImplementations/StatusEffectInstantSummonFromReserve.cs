using System.Collections;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantSummonFromReserve : StatusEffectInstantSummon
{
    public CardData backupSummon;

    public override IEnumerator Process()
    {
        targetSummon.summonCard = GetCard();

        return base.Process();
    }

    private CardData GetCard()
    {
        var reserve = References.Player
            .data.inventory.reserve;
        return reserve.Count <= 0 ? backupSummon.Clone() : reserve.list.RandomItem();
    }
}