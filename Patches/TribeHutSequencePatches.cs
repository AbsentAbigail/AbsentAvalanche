using System.Linq;
using AbsentAvalanche.Cards.Companion;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace AbsentAvalanche.Patches;

//Tribe Hut Patch Part 1
[HarmonyPatch(typeof(TribeHutSequence), "SetupFlags")]
internal class TribeHutSequencePatches
{
    static void Postfix(TribeHutSequence __instance) //After it unlocks the base mods, it'll move on to ours.
    {
        var gameObject = Object.Instantiate(__instance.flags[0].gameObject, __instance.flags[0].gameObject.transform.parent, false); //Clone the Snowdweller flag
        var flagDisplay = gameObject.GetComponent<TribeFlagDisplay>();
        var tribe = AbsentUtils.GetTribe(PlushTribe.Name);
        flagDisplay.flagSprite = tribe.flag; //Replace the flag with our tribe flag
        __instance.flags = __instance.flags.Append(flagDisplay).ToArray(); //Add it the flag to the list to check
        flagDisplay.SetAvailable(); //Set it available
        flagDisplay.SetUnlocked();
        
        var sequence2 = Object.FindObjectOfType<TribeDisplaySequence>(true);   //TribeDisplaySequence sequence should be unique, so Find should find the right one.
        var gameObject2 = Object.Instantiate(sequence2.displays[1].gameObject, sequence2.displays[2].gameObject.transform.parent, false);          //Copy one of them (Shademancers)
        sequence2.tribeNames = sequence2.tribeNames.Append(PlushTribe.Name).ToArray();                    //Add the name to the list
        sequence2.displays = sequence2.displays.Append(gameObject2).ToArray();                      //Add the display itself to the list

        //In case VS isn't recognizing Button, the class is from UnityEngine.UI.
        var button = flagDisplay.GetComponentInChildren<Button>();                               //Find the button component on our flagDisplay
        button.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);   //Deactivate the cloned listener (which opens the Snowdweller display)
        button.onClick.AddListener(() => { sequence2.Run(PlushTribe.Name); });                            //Add our own listener that opens our display

        //(SfxOneShot)
        gameObject2.GetComponent<SfxOneshot>().eventRef = FMODUnity.RuntimeManager.PathToEventReference("event:/sfx/status_icon/snow_decrease"); //Shuffling noises

        //0: Flag (ImageSprite)
        gameObject2.transform.GetChild(0).GetComponent<ImageSprite>().SetSprite(tribe.flag);        //Set the sprite of the ImageSprite component to our tribe flag

        //1: Left (ImageSprite)
        var needle = AbsentUtils.GetCard(May.Name).mainSprite;             //Find needle's sprite
        gameObject2.transform.GetChild(1).GetComponent<ImageSprite>().SetSprite(needle);            //and set it as the left image

        //2: Right (ImageSprite)
        var muncher = AbsentUtils.GetCard(Alice.Name).mainSprite;           //Find Frost Muncher's sprite
        gameObject2.transform.GetChild(2).GetComponent<ImageSprite>().SetSprite(muncher);           //and set it as the right image
        gameObject2.transform.GetChild(2).localScale *= 1.2f;                                       //and make it 20% bigger

        //3: Textbox (Image)
        gameObject2.transform.GetChild(3).GetComponent<Image>().color = new Color(0.12f, 0.47f, 0.57f); //Change the color of the textbox background

        //3-0: Text (LocalizeStringEvent)
        var collection = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English);   //Find a string table (in the desired language)
        gameObject2.transform.GetChild(3).GetChild(0).GetComponent<LocalizeStringEvent>().StringReference = collection.GetString(PlushTribe.DescKey);
                                                                                                        //Set the string in the LocaliseStringEvent

        //4:Title Ribbon (Image)
        //4-0: Text (LocalizeStringEvent)
        gameObject2.transform.GetChild(4).GetChild(0).GetComponent<LocalizeStringEvent>().StringReference = collection.GetString(PlushTribe.TitleKey);
    }
}