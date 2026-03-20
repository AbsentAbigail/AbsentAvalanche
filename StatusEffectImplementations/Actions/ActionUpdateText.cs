#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations.Actions;

internal class ActionUpdateText(Entity entity) : PlayAction
{
    public readonly Entity entity = entity;

    public override IEnumerator Run()
    {
        entity.display.promptUpdateDescription = true;
        entity.PromptUpdate();
        entity.Update();
        yield break;
    }
}