using AbsentUtilities;
using UnityEngine;
using UnityEngine.UI;

namespace AbsentAvalanche;

internal class ValCardImage : ScriptableCardImage
{
    public Image Image => GetComponent<Image>();

    // gets called when the card is created (e.g. Leaders having one consistent avatar)
    public override void AssignEvent()
    {
        // we use the CardData's main sprite for a backup here
        // otherwise it won't have any sprite
        Image.sprite = entity.data.mainSprite;
    }

    public override void UpdateEvent()
    {
        var scale = ScaleFunction(entity.damage.current);

        ((Card)entity.display).scriptableImage.transform.localScale = new Vector3(scale, scale, 1f);
    }

    private float ScaleFunction(int damage)
    {
        var mod = (Absent)AbsentUtils.GetModInfo().Mod;
        var max = mod.ValMaxSize;
        var min = mod.ValMinSize;

        var m = (max - min) / mod.ValMaxSizeAt;

        var scale = m * damage + min;
        return Mathf.Clamp(scale, min, max);
    }
}