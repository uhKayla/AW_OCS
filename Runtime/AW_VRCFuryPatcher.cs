#if UNITY_EDITOR
using System;
using HarmonyLib;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Dynamics.Contact.Components;
using VRC.SDKBase;

namespace ANGELWARE.AW_APS
{
    /// <summary>
    /// VRCFuryObliterator :^)
    ///
    /// vrcf likes to "upgrade" any haptics it finds using specific TPS tags... this forces it to stop patching our
    /// "legacy" "haptics" if we are using any sort of tps tags in our components....
    ///
    /// note to anybody reading this, yes it does break APS if vrcfury tries to upgrade things. don't ask me why, i just
    /// dont want it to touch my stuff.
    /// </summary>
    public static class AW_VRCFuryPatcher
    {
        private static readonly Harmony _harmony = new("com.angelware.vrcfpatch");
        
        [InitializeOnLoadMethod]
        public static void ObliterateThatShit()
        {
            var senderBuilderType = Type.GetType(
                "VF.Feature.SpsSendersForAllBuilder, VRCFury-Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"
            );

            if (senderBuilderType == null)
                return;

            var isTpsSenderMethodInfo = AccessTools.FirstMethod(senderBuilderType, method => method.Name == "IsTPSSender");
            var prefixMethodInfo = AccessTools.FirstMethod(typeof(AW_VRCFuryPatcher), method => method.Name == nameof(Obliterator));

            HarmonyMethod prefix = new(prefixMethodInfo);
            _harmony.Patch(isTpsSenderMethodInfo, prefix);
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private static bool Obliterator(VRCContactSender c, ref bool __result)
        {
            if (c == null || !c.GetComponent<AW_PatchMarker>())
                return true;

            __result = false;
            return false;
        }
    }

    public class AW_PatchMarker : MonoBehaviour, IEditorOnly
    {
        // Marker class used for the patch 
    }
}
#endif