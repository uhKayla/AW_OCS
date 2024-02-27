#if UNITY_EDITOR
using System;
using System.Diagnostics;
using ANGELWARE.AW_OCS;
using AnimatorAsCode.V1;
using AnimatorAsCode.V1.ModularAvatar;
using AnimatorAsCode.V1.NDMFProcessor;
using AnimatorAsCode.V1.VRC;
using nadena.dev.ndmf;
using VRC.SDK3.Avatars.Components;

[assembly: ExportsPlugin(typeof(AW_OCSAAC))]

namespace ANGELWARE.AW_OCS
{
    // ReSharper disable once InconsistentNaming
    public class AW_OCSAAC : AacPlugin<AW_OCS>
    {
        protected override AacPluginOutput Execute()
        {
            var ctrl = aac.NewAnimatorController();
            var fx = ctrl.NewLayer("AnimatedAPS");
            var maAc = MaAc.Create(my.gameObject);

            var ocsBitList = new AW_OCSParameter[]
            {
                new AW_OCSParameter
                {
                    Ocs0 = true,
                    Ocs1 = false,
                    Ocs2 = false,
                    Ocs3 = false,
                    parameter = "Menu/APS/Pussy"
                },
                new AW_OCSParameter
                {
                    Ocs0 = false,
                    Ocs1 = true,
                    Ocs2 = false,
                    Ocs3 = false,
                    parameter = "Menu/APS/Ass"
                },
                new AW_OCSParameter
                {
                    Ocs0 = false,
                    Ocs1 = false,
                    Ocs2 = true,
                    Ocs3 = false,
                    parameter = "Menu/APS/Mouth"
                },
                new AW_OCSParameter
                {
                    Ocs0 = true,
                    Ocs1 = true,
                    Ocs2 = true,
                    Ocs3 = false,
                    parameter = "Menu/APS/Breasts"
                },
            };
            
            // Toggle Layer
            var toggleLayer = ctrl.NewLayer("Toggles");
            // Toggle Layer Init State
            var initState = toggleLayer.NewState("Init").WithAnimation(aac.DummyClipLasting(1, AacFlUnit.Frames));
            
            // Make a new layer and add toggles for each of the components automagically
            foreach (var ocs in ocsBitList)
            {
                // Trigger parameter (use in menu)
                var p0 = fx.BoolParameter("OCS/Bit/0");
                var p1 = fx.BoolParameter("OCS/Bit/1");
                var p2 = fx.BoolParameter("OCS/Bit/2");
                var p3 = fx.BoolParameter("OCS/Bit/3");

                // Tracking parameter (tracks state of this animator)
                var pTracking = fx.BoolParameter(ocs.parameter + "/Tracking");
                // Toggle parameter (drives smoothed toggle)
                var pToggle = fx.FloatParameter(ocs.parameter);
                // State for this hole
                var toggledState = toggleLayer.NewState(ocs.parameter).WithAnimation(aac.DummyClipLasting(1, AacFlUnit.Frames));
                // Parameter Driver
                toggledState.Drives(pToggle, 1.0f);
                toggledState.Drives(pTracking, true);
            
                foreach (var ocsOther in ocsBitList)
                {
                    if (ocsOther.parameter == ocs.parameter)
                        continue;
                    
                    // Short lived temp parameter while generating this state
                    var pOtherTracking = aac.NoAnimator().BoolParameter(ocsOther.parameter + "/Tracking");
                    // Drive all other tracking states to false when this state is activated.
                    toggledState.Drives(pOtherTracking, false);
                }

                var initStateTransition = initState.TransitionsTo(toggledState);
                
                // Transition from Init
                if (ocs.Ocs0)
                    initStateTransition.When(p0.IsTrue());
                else
                    initStateTransition.When(p0.IsFalse());
                
                if (ocs.Ocs1)
                    initStateTransition.When(p1.IsTrue());
                else
                    initStateTransition.When(p1.IsFalse());
                
                if (ocs.Ocs2)
                    initStateTransition.When(p2.IsTrue());
                else
                    initStateTransition.When(p2.IsFalse());
                
                if (ocs.Ocs3)
                    initStateTransition.When(p3.IsTrue());
                else
                    initStateTransition.When(p3.IsFalse());
                
                /*
                 * I am going to leave this off here, we are having trouble activating two bits at the same time, we
                 * need some sort of buffer that will wait a second before actually activating, so we can give the system
                 * time to enable the second and third bit contacts.
                 *
                 * We also are currently not turning things off, but I can't really think of a viable way to solve this
                 * currently.
                 *
                 * This script will need to be moved outside of OCS somehow to prevent an extra dependency with this,
                 * but I *really* do not want another dependency of my own, so perhaps moving it into our master AAC
                 * namespace will be the solution here.
                 */
                
                
                // Transition to Init
                toggledState.TransitionsTo(initState).Automatically();
                
                
            }
            maAc.NewMergeAnimator(ctrl, VRCAvatarDescriptor.AnimLayerType.FX);
            
            return AacPluginOutput.Regular();
        }
    }

    [Serializable]
    public class AW_OCSParameter
    {
        public string parameter;
        private byte ocsFlags;

        [Flags]
        private enum OCSFlags : byte
        {
            None = 0, // 0000
            Ocs0 = 1, // 0001
            Ocs1 = 2, // 0010
            Ocs2 = 4, // 0100
            Ocs3 = 8  // 1000
        }

        public bool Ocs0
        {
            get => (ocsFlags & (byte)OCSFlags.Ocs0) != 0;
            set
            {
                if (value) ocsFlags |= (byte)OCSFlags.Ocs0;
                else ocsFlags &= (byte)~OCSFlags.Ocs0;
            }
        }

        public bool Ocs1
        {
            get => (ocsFlags & (byte)OCSFlags.Ocs1) != 0;
            set
            {
                if (value) ocsFlags |= (byte)OCSFlags.Ocs1;
                else ocsFlags &= (byte)~OCSFlags.Ocs1;
            }
        }

        public bool Ocs2
        {
            get => (ocsFlags & (byte)OCSFlags.Ocs2) != 0;
            set
            {
                if (value) ocsFlags |= (byte)OCSFlags.Ocs2;
                else ocsFlags &= (byte)~OCSFlags.Ocs2;
            }
        }

        public bool Ocs3
        {
            get => (ocsFlags & (byte)OCSFlags.Ocs3) != 0;
            set
            {
                if (value) ocsFlags |= (byte)OCSFlags.Ocs3;
                else ocsFlags &= (byte)~OCSFlags.Ocs3;
            }
        }
    }

}
#endif