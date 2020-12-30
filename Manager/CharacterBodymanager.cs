using TaleWorlds.Core;
using Helpers;
using TaleWorlds.CampaignSystem;
using System.Reflection;
using HarmonyLib;

namespace CharacterCreation.Manager
{
    class CharacterBodyManager
    {
        // Yeah, um, structs are passed by value in C#. So this method should basically do nothing. Unless you use
        // the 'ref' keyword. Unfortunately, it still won't work because both DynamicBodyProperties and BodyProperties
        // are themselves structs. To change the latter's DynamicProperties property's underlying field without actually
        // replacing its value, you will need to somehow acquire a reference to said field, becausing just
        // accessing it will return a copy of it. Not only that, but you also have to do the same for
        // Hero.BodyProperties itself since, as noted before, BodyProperties is a struct.
        // Good luck with that. Besides, this is overengineering when a BodyProperties constructor exist that
        // actually works as intended. - Designer225
        public static void CopyDynamicBodyProperties(DynamicBodyProperties src, DynamicBodyProperties target)
        {
            target.Age = src.Age;
            target.Build = src.Build;
            target.Weight = src.Weight;

        }

        // Mercifully, this does something. Although there might be an issue with birthdays causing edited heroes to age up
        // more often than not, so this needs to be rewritten.
        // Needs to differentiate if the game is under setup or in progress, but that is something for another update.
        public static void ResetBirthDayForAge(CharacterObject characterObject, float targetAge, bool randomize = false)
        {
            if (!characterObject.IsHero) return;
            characterObject.HeroObject.SetBirthDay(randomize ? HeroHelper.GetRandomBirthDayForAge(targetAge) : CampaignTime.YearsFromNow(-targetAge));
        }
    }
}