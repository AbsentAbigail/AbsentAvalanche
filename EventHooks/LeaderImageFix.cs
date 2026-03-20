namespace AbsentAvalanche.EventHooks;

public class LeaderImageFix
{
    public static void FixImage(Entity entity)
    {
        if (entity.display is not Card { hasScriptableImage: false } card)
            return;
        card.mainImage.gameObject.SetActive(true);
    }
}