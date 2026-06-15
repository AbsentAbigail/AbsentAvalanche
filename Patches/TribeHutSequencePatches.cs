using System.Linq;
using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Builders.Cards.PilotLeaders;
using AbsentAvalanche.Builders.Tribes;
using Deadpan.Enums.Engine.Components.Modding;
using FMODUnity;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace AbsentAvalanche.Patches;

//Tribe Hut Patch Part 1
[HarmonyPatch(typeof(TribeHutSequence), "SetupFlags")]
internal class TribeHutSequencePatches
{
    [UsedImplicitly]
    static void Postfix(TribeHutSequence __instance) //After it unlocks the base mods, it'll move on to ours.
    {
        PlushTribeHutObject(__instance);
        PilotTribeHutObject(__instance);
    }

    private static void PlushTribeHutObject(TribeHutSequence instance)
    {
        var gameObject = Object.Instantiate(instance.flags[0].gameObject, instance.flags[0].gameObject.transform.parent, false); //Clone the Snowdweller flag
        var flagDisplay = gameObject.GetComponent<TribeFlagDisplay>();
        var tribe = Absent.GetTribe(PlushTribe.Name);
        flagDisplay.flagSprite = tribe.flag; //Replace the flag with our tribe flag
        instance.flags = instance.flags.Append(flagDisplay).ToArray(); //Add it the flag to the list to check
        flagDisplay.SetAvailable(); //Set it available
        flagDisplay.SetUnlocked();
        
        var sequence2 = Object.FindObjectOfType<TribeDisplaySequence>(true);   //TribeDisplaySequence sequence should be unique, so Find should find the right one.
        var gameObject2 = Object.Instantiate(sequence2.displays[1].gameObject, sequence2.displays[2].gameObject.transform.parent, false);          //Copy one of them (Shademancers)
        sequence2.tribeNames = sequence2.tribeNames.Append(PlushTribe.Name).ToArray();                    //Add the name to the list
        sequence2.displays = sequence2.displays.Append(gameObject2).ToArray();                      //Add the display itself to the list

        //In case VS isn't recognizing Button, the class is from UnityEngine.UI.
        var button = flagDisplay.GetComponentInChildren<Button>();                               //Find the button component on our flagDisplay
        button.onClick.SetPersistentListenerState(0, UnityEventCallState.Off);   //Deactivate the cloned listener (which opens the Snowdweller display)
        button.onClick.AddListener(() => { sequence2.Run(PlushTribe.Name); });                            //Add our own listener that opens our display

        //(SfxOneShot)
        gameObject2.GetComponent<SfxOneshot>().eventRef = RuntimeManager.PathToEventReference("event:/sfx/status_icon/snow_decrease"); //Shuffling noises

        //0: Flag (ImageSprite)
        gameObject2.transform.GetChild(0).GetComponent<ImageSprite>().SetSprite(tribe.flag);        //Set the sprite of the ImageSprite component to our tribe flag

        //1: Left (ImageSprite)
        var maySprite = Absent.GetCard(May.Name).mainSprite;             //Find needle's sprite
        gameObject2.transform.GetChild(1).GetComponent<ImageSprite>().SetSprite(maySprite);            //and set it as the left image

        //2: Right (ImageSprite)
        var aliceSprite = Absent.GetCard(Alice.Name).mainSprite;           //Find Frost Muncher's sprite
        gameObject2.transform.GetChild(2).GetComponent<ImageSprite>().SetSprite(aliceSprite);           //and set it as the right image
        gameObject2.transform.GetChild(2).localScale *= 1.2f;                                       //and make it 20% bigger

        //3: Textbox (Image)
        gameObject2.transform.GetChild(3).GetComponent<Image>().color = new Color(0.12f, 0.47f, 0.57f); //Change the color of the textbox background

        //3-0: Text (LocalizeStringEvent)
        var collection = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English);   //Find a string table (in the desired language)
        gameObject2.transform.GetChild(3).GetChild(0).GetComponent<LocalizeStringEvent>().StringReference = collection.GetString(PlushTribe.DescKey);
        gameObject2.transform.GetChild(3).GetChild(0).GetComponent<LocalizeStringEvent>().transform.localScale += new Vector3(-0.17f, -0.17f, 0);
        
        //4:Title Ribbon (Image)
        //4-0: Text (LocalizeStringEvent)
        gameObject2.transform.GetChild(4).GetChild(0).GetComponent<LocalizeStringEvent>().StringReference = collection.GetString(PlushTribe.TitleKey);
    }
    
    private static void PilotTribeHutObject(TribeHutSequence instance)
    {
        var gameObject = Object.Instantiate(instance.flags[0].gameObject, instance.flags[0].gameObject.transform.parent, false); //Clone the Snowdweller flag
        var flagDisplay = gameObject.GetComponent<TribeFlagDisplay>();
        var tribe = Absent.GetTribe(PilotTribe.Name);
        flagDisplay.flagSprite = tribe.flag; //Replace the flag with our tribe flag
        instance.flags = instance.flags.Append(flagDisplay).ToArray(); //Add it the flag to the list to check
        flagDisplay.SetAvailable(); //Set it available
        flagDisplay.SetUnlocked();
        
        var sequence2 = Object.FindObjectOfType<TribeDisplaySequence>(true);   //TribeDisplaySequence sequence should be unique, so Find should find the right one.
        var gameObject2 = Object.Instantiate(sequence2.displays[1].gameObject, sequence2.displays[2].gameObject.transform.parent, false);          //Copy one of them (Shademancers)
        sequence2.tribeNames = sequence2.tribeNames.Append(PilotTribe.Name).ToArray();                    //Add the name to the list
        sequence2.displays = sequence2.displays.Append(gameObject2).ToArray();                      //Add the display itself to the list

        //In case VS isn't recognizing Button, the class is from UnityEngine.UI.
        var button = flagDisplay.GetComponentInChildren<Button>();                               //Find the button component on our flagDisplay
        button.onClick.SetPersistentListenerState(0, UnityEventCallState.Off);   //Deactivate the cloned listener (which opens the Snowdweller display)
        button.onClick.AddListener(() => { sequence2.Run(PilotTribe.Name); });                            //Add our own listener that opens our display

        //(SfxOneShot)
        gameObject2.GetComponent<SfxOneshot>().eventRef = RuntimeManager.PathToEventReference("event:/sfx/card/drag"); //Shuffling noises

        //0: Flag (ImageSprite)
        gameObject2.transform.GetChild(0).GetComponent<ImageSprite>().SetSprite(tribe.flag);        //Set the sprite of the ImageSprite component to our tribe flag

        //1: Left (ImageSprite)
        var ameliaDoodle = Absent.GetSprite("Amelia");             //Find needle's sprite
        gameObject2.transform.GetChild(1).GetComponent<ImageSprite>().SetSprite(ameliaDoodle);            //and set it as the left image

        //2: Right (ImageSprite)
        var ameliaDraw = Absent.GetCard(AmeliaDraw.Name).mainSprite;           //Find Frost Muncher's sprite
        gameObject2.transform.GetChild(2).GetComponent<ImageSprite>().SetSprite(ameliaDraw);           //and set it as the right image
        gameObject2.transform.GetChild(2).localScale *= 1.2f;                                       //and make it 20% bigger

        //3: Textbox (Image)
        gameObject2.transform.GetChild(3).GetComponent<Image>().color = new Color(0.12f, 0.47f, 0.57f); //Change the color of the textbox background

        //3-0: Text (LocalizeStringEvent)
        var collection = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English);   //Find a string table (in the desired language)
        gameObject2.transform.GetChild(3).GetChild(0).GetComponent<LocalizeStringEvent>().StringReference = collection.GetString(PilotTribe.DescKey);
        gameObject2.transform.GetChild(3).GetChild(0).GetComponent<LocalizeStringEvent>().transform.localPosition += new Vector3(0, -0.1f, 0);
        //4:Title Ribbon (Image)
        //4-0: Text (LocalizeStringEvent)
        gameObject2.transform.GetChild(4).GetChild(0).GetComponent<LocalizeStringEvent>().StringReference = collection.GetString(PilotTribe.TitleKey);
    }
}