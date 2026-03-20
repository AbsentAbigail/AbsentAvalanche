#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace AbsentAvalanche.Scriptables.CardImage;

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

    private static float ScaleFunction(int damage)
    {
        var mod = Absent.Instance;
        var max = mod.valMaxSize;
        var min = mod.valMinSize;

        var m = (max - min) / mod.valMaxSizeAt;

        var scale = m * damage + min;
        return Mathf.Clamp(scale, min, max);
    }
}