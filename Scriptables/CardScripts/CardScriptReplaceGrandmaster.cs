#region

using AbsentAvalanche.Builders.Traits;

#endregion

namespace AbsentAvalanche.Scriptables.CardScripts;

internal class CardScriptReplaceGrandmaster : CardScript
{
    public override void Run(CardData target)
    {
        if (!target.traits.RemoveWhere(trait => trait.data.name == Absent.GetTrait(Grandmaster.Name).name))
        {
            return;
        }
        target.startWithEffects = [
            .. target.startWithEffects,
            Absent.SStack("While Active Unmovable To Enemies")
        ];
    }
}
