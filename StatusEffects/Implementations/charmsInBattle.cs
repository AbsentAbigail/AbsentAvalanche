using System;
using System.Collections;
using System.Collections.Generic;
using AbsentUtilities;
using UnityEngine;
using UnityEngine.UI;

namespace AbsentAvalanche.StatusEffects.Implementations;

// Code by CautiousNinja
internal class StatusEffectBestowUpgrades : StatusEffectData
{

    [SerializeField]
    public List<CardUpgradeData> upgrades; //List that stores all current upgrades

    int stackToIndex = 0; //This is used to stack more upgrades onto the target

    bool reset; //This tracks whether to skip the charm animation on reload


    //These values are used to maintain the target's current stats for after the charms are applied
    int tempDmg;
    int tempDmgMax;
    int tempHP;
    int tempHPMax;
    int tempCntr;
    int tempCntrMax;
    int tempUses;
    int tempUsesMax;
    int tempEB;
    float tempEF;

    private CardData originalCard;

    public override void Init()
    {
        base.OnStack += StackUpgrades; //This is where all the applications begin
        base.OnEnd += RemoveUpgrades; //The charms are removed
        Events.OnBattleEnd += BattleEnd; //Effectctively just calls RemoveUpgrades this is a just triple checker for removing the charms. Probably overkill
        Events.OnCampaignSaved += QueueReset; //This allows the charm animation to skip when the campaign is saved and reloaded. At least it should
    }

    public override bool RunStackEvent(int stacks)
    {
        textKey = AbsentUtils.GetStatus("Cleanse").textKey; //This removes the text on this effect so that it only appears in attack effects

        LogHelper.Log("[Bestow Upgrades] Run Stack Event");

        if (upgrades != null && applier == null)
            return false; //Null checker
        else if (upgrades == null)
            upgrades = new List<CardUpgradeData>(); //Defines the list

        if (upgrades.Count == 0)
        {
            originalCard = target.data.Clone();
        }

        stackToIndex = Math.Max(upgrades.Count, 0); //Tracks where the current list is, this way only new charms are applied

        //This creates the upgrade list adding only the upgrades that can actually be assigned to the target
        if (applier.data.upgrades != null && applier.data.upgrades.Count > 0)
        {
            List<CardUpgradeData> temp = applier.data.upgrades;

            List<CardUpgradeData> temp2 = new List<CardUpgradeData>();

            for (int i = 0; i < temp.Count; i++)                        //Put chuckle charm at start of list if it exists
            {
                LogHelper.Log("[Bestow Upgrades] Searching " + temp[i].name);

                if (temp[i].CanAssign(target))
                {
                    if (temp[i].name == "CardUpgradeRemoveCharmLimit")
                    {
                        temp2.Insert(0, temp[i]);
                    }
                    else
                    {
                        temp2.Add(temp[i]);
                    }
                }
            }

            upgrades.AddRange(temp2);
            return true;
        }

        return false;
    }

    public IEnumerator StackUpgrades(int stacks)
    {

        //The targets current and current max stats are stored
        if (target.data.hasAttack)
        {
            tempDmg = target.damage.current - target.tempDamage.Value;
            tempDmgMax = target.damage.max;
        }
        if (target.data.hasHealth)
        {
            tempHP = target.hp.current;
            tempHPMax = target.hp.max;
        }
        tempCntr = target.counter.current;
        tempCntrMax = target.counter.max;
        tempUses = target.uses.current;
        tempUsesMax = target.uses.max;
        tempEB = target.effectBonus;
        tempEF = target.effectFactor;

        //On campaign reload, this should be called to skip the charm animating
        if (reset)
        {
            Reset();
            reset = false;
        }
        else
        {

            //All the appliable upgrades are added here
            for (int i = stackToIndex; i < upgrades.Count; i++)
            {
                LogHelper.Log("[Bestow Upgrades] Assigning " + upgrades[i].name);
                AssignNoVis(target, upgrades[i].Clone());
            }

            LogHelper.Log("[Bestow Upgrades] Trying to run assign sequence");

            LogHelper.Log("[Bestow Upgrades] Assigned, Run?");
            yield return AnimateCharm(); //This method calls the Update() method which will change the visuals and stats of the card
        }

    }

    public IEnumerator ClearOnlyStartStatuses()
    {
        //The game's method for giving entities charms requires it to clear all statuses on the card.
        //This only clears the starting statuses, so they can be reapplied, but leaves all other status effects

        //Clear all statuses in the card's initial status effects
        for (int i = 0; i < target.statusEffects.Count; i++)
        {
            foreach (CardData.StatusEffectStacks stack in target.data.startWithEffects)
            {
                if (target.statusEffects[i].name == stack.data.name)
                {
                    LogHelper.Log("[Bestow Upgrades] Removing " + stack.count + " stacks from " + stack.data.name);
                    yield return target.statusEffects[i].RemoveStacks(stack.count, false);
                }
            }
        }


        //Clear all statuses in the card's initial attack effects
        for (int i = 0; i < target.attackEffects.Count; i++)
        {
            foreach (CardData.StatusEffectStacks stack in target.data.attackEffects)
            {
                if (target.attackEffects[i].data.name == stack.data.name)
                {
                    LogHelper.Log("[Bestow Upgrades] Removing " + stack.count + " stacks from " + stack.data.name);
                    target.attackEffects[i].count -= stack.count;
                    if (target.attackEffects[i].count <= 0)
                    {
                        target.attackEffects.Remove(target.attackEffects[i]);
                    }
                }
            }
        }

        //Clear all traits the card starts with.
        for (int i = 0; i < target.traits.Count; i++)
        {
            CardData.TraitStacks trait = target.data.traits.Find((CardData.TraitStacks a) => a.data.name == target.traits[i].data.name);

            if (trait != null)
            {
                if (target.traits[i].count > trait.count)
                {
                    yield return target.traits[i].RemoveEffectStacks(target, trait.count);
                }
                else
                {
                    yield return target.traits[i].DisableEffects();

                    target.traits.RemoveAt(i);
                }
            }

        }

        target.startingEffectsApplied = false;
    }


    public void AssignNoVis(Entity entity, CardUpgradeData upgrade)
    {
        //The game's method for adding charms to entities updates the display each time.
        //This allows you to update the display of the card after all charms and upgrades are applied.

        Events.InvokeUpgradeAssign(entity, upgrade);
        upgrade.Assign(entity.data);

    }

    public override bool RunEndEvent()
    {
        //Just checking if we should trying running the end event
        return target != null && upgrades.Count > 0;
    }

    public void BattleEnd()
    {
        if (target == null)
            return; //Null target check

        //Has to be done using the action queue because RemoveUpgrades is an IEnumerator
        ActionQueue.Add(new ActionSequence(RemoveUpgrades()), true);

    }

    public void QueueReset()
    {
        //This allows indicates to skip the animation on campaign reload
        reset = true;
    }

    public void Reset()
    {
        //This is called when this effect is loaded from a campaign to just remove and readd the upgrades without the charm animation


        //Null target check
        if (target == null)
            return;

        //Remove the upgrades
        BattleEnd();

        //Readd upgrades
        for (int i = 0; i < upgrades.Count; i++)
        {
            LogHelper.Log("[Bestow Upgrades] Assigning " + upgrades[i].name);
            AssignNoVis(target, upgrades[i].Clone());
        }

        //Update the card
        ActionQueue.Stack(new ActionSequence(Update()), true);

        reset = false;
    }

    public IEnumerator RemoveUpgrades()
    {

        //This removes all the upgrades

        //Null target check
        if (target == null || originalCard == null)
            yield break;


        //Stores the target's current and max health
        if (target.data.hasAttack)
        {
            tempDmg = target.damage.current - target.tempDamage.Value;
            tempDmgMax = target.damage.max;
        }
        if (target.data.hasHealth)
        {
            tempHP = target.hp.current;
            tempHPMax = target.hp.max;
        }
        tempCntr = target.counter.current;
        tempCntrMax = target.counter.max;
        tempUses = target.uses.current;
        tempUsesMax = target.uses.max;
        tempEB = target.effectBonus;
        tempEF = target.effectFactor;

        LogHelper.Log("[Bestow Upgrades] Removing all upgrades");

        target.data = originalCard; //Set the data to the original target data

        //This should be obselete with the copying of the data above, but I saved it just in case that doesn't work/bugs out.
        /*foreach (CardUpgradeData up in upgrades)
        {
            LogHelper.Log("[Bestow Upgrades] Removing " + up.name);

            //Checks to make sure the upgrade is still there
            CardUpgradeData toRemove = target.data.upgrades.Find((CardUpgradeData a) => a.name == up.name);

            if ((bool)toRemove)
                UnAssign(toRemove, target.data);
        }*/

        //Updates the card display
        yield return Update();

    }

    public void OnDestroy()
    {
        //When this effect is destroy it ends

        //Null target check
        if (target != null)
        {
            BattleEnd();
        }


        Events.OnBattleEnd -= BattleEnd; //Not a statusEffectData event; it must be unhooked
        Events.OnCampaignSaved -= QueueReset; //Not a statusEffectData event; it must be unhooked

        reset = false;
    }


    //All these variables are for the charm animtation sequence
    [SerializeField]
    public Image background;

    [SerializeField]
    public float backgroundAlpha = 0.67f;

    [SerializeField]
    public Transform cardHolder;

    [SerializeField]
    public float cardScale = 1f;

    [SerializeField]
    public UnityEngine.Animator animator;

    public CardUpgradeData upgradeData;

    public float fade;

    public static float fadeInDur = 0.5f;

    public static float fadeOutDur = 0.25f;

    public Transform previousParent;

    public int previousChildIndex;

    public Vector3 previousPosition;


    public void Focus()
    {
        //Bringss the card center-screen for the charm animation sequence

        target.ForceUnHover();
        previousParent = target.transform.parent;
        previousChildIndex = target.transform.GetSiblingIndex();
        previousPosition = target.transform.localPosition;
        target.transform.SetParent(cardHolder, worldPositionStays: true);
        LeanTween.moveLocal(target.gameObject, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuart);
        target.wobbler?.WobbleRandom();
        LeanTween.scale(target.gameObject, Vector3.one * cardScale, 0.67f).setEase(LeanTweenType.easeOutBack);
        LeanTween.rotateLocal(target.gameObject, Vector3.zero, 1f).setEase(LeanTweenType.easeOutBack);
    }

    public void Unfocus()
    {
        //Sets the card back to where it was

        if ((bool)target && target.StillExists())
        {
            target.transform.parent = previousParent;
            target.transform.SetSiblingIndex(previousChildIndex);
            target.TweenToContainer();
            target.wobbler?.WobbleRandom();
        }
    }

    public IEnumerator Update()
    {

        //Sets all the temp stats to the difference between the original stats and the new stats
        if (target.data.hasAttack)
        {
            tempDmgMax -= target.data.damage;
            tempDmg -= tempDmgMax;
        }
        if (target.data.hasHealth)
        {
            tempHPMax -= target.data.hp;
            tempHP = Math.Max(tempHP - tempHPMax, 1);
        }

        tempCntrMax -= target.data.counter;
        tempCntr = Math.Max(tempCntr - tempCntrMax, 0);
        tempUsesMax -= target.data.uses;
        tempUses = Math.Max(tempUses - tempUsesMax, 0);
        tempEB = Math.Max(tempEB - (tempEB - target.effectBonus), 0);
        tempEF = Math.Max(tempEF - (tempEF - target.effectFactor), 1f);

        yield return ClearOnlyStartStatuses(); //Removes only the starting statuses, so that UpdateData() can reapply them.

        if (target.display is Card card)
        {
            yield return card.UpdateData();

            //Calculates what the current stats should be based on the difference the between the new and previous
            if (target.data.hasAttack)
                target.damage.current = Math.Max(tempDmg, 0);
            if (target.data.hasHealth)
                target.hp.current = tempHP;
            target.counter.current = tempCntr;
            target.uses.current = tempUses;
            target.effectBonus = tempEB;
            target.effectFactor = tempEF;

            yield return card.UpdateDisplay(); //Updates the visual display of the card
        }
    }

    public IEnumerator AnimateCharm()
    {
        AssignCharmSequence animation = UnityEngine.Object.FindObjectOfType<AssignCharmSequence>(true); //This is the animation sequence for adding a charm to a card. Many of the local variables are copied from this class's

        background = animation.background;

        yield return Sequences.Wait(0.5f);
        BackgroundFade(backgroundAlpha, fadeInDur);
        LogHelper.Log("[Bestow Charm] Focus");
        Focus(); //Bring card center screen
        LogHelper.Log("[Bestow Charm] Animator Set Trigger");

        animator = animation.animator;
        animator.SetTrigger("Assign");

        Rumble();

        LogHelper.Log("[Bestow Charm] Charm SFX");
        SfxSystem.OneShot("event:/sfx/inventory/charm_assign");
        LogHelper.Log("[Bestow Charm] Wait");
        yield return new WaitForSeconds(2.5f);
        BackgroundFade(0f, fadeOutDur);

        yield return Update(); //Card Data and effects are updated

        yield return new WaitForSeconds(0.5f);

        LogHelper.Log("[Bestow Charm] Wait More");
        yield return new WaitForSeconds(fadeOutDur * 0.5f);
        LogHelper.Log("[Bestow Charm] Unfocus");
        Unfocus(); //Send card back to where it was
        LogHelper.Log("[Bestow Charm] Wait More");
        yield return new WaitForSeconds(fadeOutDur * 0.5f);
    }

    public void BackgroundFade(float alpha, float dur)
    {
        //This doesn't do anything, but with the right information it could

        LeanTween.cancel(background.gameObject);
        LeanTween.value(background.gameObject, fade, alpha, dur).setEase(LeanTweenType.easeOutQuad).setOnUpdate(delegate (float a)
        {
            fade = a;
            background.color = background.color.WithAlpha(a);
        });
    }

    public void Rumble()
    {
        //This shakes the screen
        Events.InvokeScreenRumble(0f, 1f, 0f, 1f, 0.5f, 0.25f);
    }
}