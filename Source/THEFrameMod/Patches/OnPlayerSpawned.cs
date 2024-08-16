using GorillaLocomotion;
using HarmonyLib;
using System;
using UnityEngine;
namespace THEFrameMod.Patches
{
    [HarmonyPatch(typeof(Player), "Start")]
    public class OnPlayerSpawned
    {
        public static Action OnSpawned;
        static bool hasDone;
        static void Postfix(ref Player __instance)
        {
            try
            {
                OnSpawned();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please copy and send this error to @Rekoon in the modding server!");
                Debug.LogError(e.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}