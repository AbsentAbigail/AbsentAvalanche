#region

using System.Threading.Tasks;

#endregion

namespace AbsentAvalanche.EventHooks;

public static class LuminBlessing
{
    public static Task ResetLuminBlessing()
    {
        SaveSystem.statsSaver.SaveValue("leader_charms", 0, Absent.PrefixGuid("data"));
        
        return Task.CompletedTask;
    }
}