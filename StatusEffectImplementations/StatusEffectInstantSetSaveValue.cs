#region

using System.Collections;
using AbsentAvalanche.Helpers;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantSetSaveValue : StatusEffectInstant
{
    public string dataKey = "leader_charms";
    private readonly string _folderKey = Absent.PrefixGuid("data");
    
    public override IEnumerator Process()
    {
        var previous = SaveSystem.statsSaver.LoadValue(dataKey, _folderKey, 0);
        LogHelper.Log("Setting " + dataKey + " to " + previous + " + " + count);
        SaveSystem.statsSaver.SaveValue(dataKey, previous + count, _folderKey);
        SaveSystem.statsSaver.SaveValue("source", target.data.id, _folderKey);
        yield return base.Process();
    }
}