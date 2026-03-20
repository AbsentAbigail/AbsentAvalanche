#region

using AbsentAvalanche.Builders.Traits;

#endregion

namespace AbsentAvalanche.Scriptables.CardScripts;

public class CardScriptComboConsume : CardScript
{
    public override void Run(CardData target)
    {
        AddTrait(target, Combo.Name, 1);
        AddTrait(target, "Consume", 1);
    }

    private static void AddTrait(CardData target, string trait, int amount)
    {
        var existingTrait = target.traits.Find(traitStack => traitStack.data.name == Absent.GetTrait(trait).name);
        if (existingTrait != null)
        {
            existingTrait.count += amount;
        }
        else
        {
            target.traits.Add(Absent.TStack(trait, amount));
        }
    }
}