// #if UNITY_EDITOR
// using System.Collections.Generic;
// using ANGELWARE.AW_AAC;
// using ANGELWARE.AW_AAC.Plugin;
// using AnimatorAsCode.V1;
// using AnimatorAsCode.V1.ModularAvatar;
// using AnimatorAsCode.V1.NDMFProcessor;
// using nadena.dev.ndmf;
// using UnityEngine;
// using UnityEngine.Serialization;
// using VRC.SDK3.Avatars.Components;
// using VRC.SDKBase;
//
// [assembly: ExportsPlugin(typeof(AW_OCSPlugin))]
// namespace ANGELWARE.AW_AAC
// {
//     // ReSharper disable once InconsistentNaming
//     [AddComponentMenu("ANGELWARE/AAC/AW_OCS")]
//
//     public class AW_OCS : MonoBehaviour, IEditorOnly
//     {
//         [SerializeField] public Transform rootTransform;
//
//         [HideInInspector]
//         public GameObject rootRObj;
//         public GameObject rootSObj;
//
//         public GameObject grFObj;
//         public GameObject gsFObj;
//         
//         public GameObject grO1Obj;
//         public GameObject grO2Obj;
//         public GameObject grO3Obj;
//         
//         public GameObject gsO1Obj;
//         public GameObject gsO2Obj;
//         public GameObject gsO3Obj;
//
//         public List<string> parameterList;
//     }
//
//     // ReSharper disable once InconsistentNaming
//     public class AW_OCSPlugin : AacPlugin<AW_OCS>
//     {
//         // private readonly IDirectTreeManager _directTreeManager;
//         //
//         // public AW_OCSPlugin(IDirectTreeManager directTreeManager)
//         // {
//         //     _directTreeManager = directTreeManager;
//         // }
//         
//         protected override bool UseWriteDefaults(AW_OCS exampleToggle, BuildContext ctx)
//         {
//             return false;
//         }
//         
//         protected override AacPluginOutput Execute()
//         {
//             // var ctrl = aac.NewAnimatorController();
//             // var fx = ctrl.NewLayer("Tree");
//             // var param = fx.FloatParameter("Internal/Float");
//             // fx.OverrideValue(param, 1.0f);
//             // var maAc = MaAc.Create(my.gameObject);
//
//             var masterDbt = aac.NewBlendTree().Direct();
//
//             CreateContactSenders();
//             CreateContactReceivers();
//             var contactTree = CreateContactTree();
//             
//             AW_DirectTreeManager.AddDbtToList(contactTree, my.parameterList);
//             
//             // maAc.NewMergeAnimator(ctrl, VRCAvatarDescriptor.AnimLayerType.FX);
//
//             return AacPluginOutput.Regular();
//         }
//
//         private void CreateContactSenders()
//         {
//             Debug.Log("Creating Contact Senders...");
//             my.rootTransform = my.gameObject.transform.Find("Armature/Hips/Spine");
//             var spineT = my.rootTransform;
//
//             if (spineT == null)
//             {
//                 Debug.LogError("Spine transform not found!");
//                 return;
//             }
//
//             my.rootSObj = new GameObject("OCS_Senders");
//             my.rootSObj.transform.SetParent(spineT, false);
//     
//             var gsTransform = my.rootSObj.transform.position;
//             var gsRotation = my.rootSObj.transform.rotation.eulerAngles;
//             var gsCollisionTagsStr = new string[]
//             {
//                 "OCS/GlobalBeacon",
//                 "OCS/GlobalBeacon/Ver/0"
//             };
//             var gsCollisionTags = new List<string>(gsCollisionTagsStr);
//     
//             AW_ContactSender.CreateContactSender(my.rootSObj,
//                 my.rootSObj.transform,
//                 0,
//                 0.1f,
//                 new Vector3(0f,0f,0f),
//                 new Vector3(0f,0f,0f),
//                 gsCollisionTags);
//             
//             var gsFCollisionTagsStr = new string[]
//             {
//                 "OCS/GlobalBeacon/Finish"
//             };
//             var gsFCollisionTags = new List<string>(gsFCollisionTagsStr);
//             my.gsFObj = new GameObject("gsFobj");
//             my.gsFObj.transform.SetParent(my.rootSObj.transform);
//             
//             AW_ContactSender.CreateContactSender(my.gsFObj,
//                 my.rootSObj.transform,
//                 0,
//                 0.1f,
//                 new Vector3(0f,0f,0f),
//                 new Vector3(0f,0f,0f),
//                 gsFCollisionTags);
//             
//             var gsO1CollisionTagsStr = new string[]
//             {
//                 "OCS/GlobalBeacon/Opt1"
//             };
//             var gsO1CollisionTags = new List<string>(gsO1CollisionTagsStr);
//             my.gsO1Obj = new GameObject("gs01obj");
//             my.gsO1Obj.transform.SetParent(my.rootSObj.transform);
//             
//             AW_ContactSender.CreateContactSender(my.gsO1Obj,
//                 my.rootSObj.transform,
//                 0,
//                 0.1f,
//                 new Vector3(0f,0f,0f),
//                 new Vector3(0f,0f,0f),
//                 gsO1CollisionTags);
//             
//             var gsO2CollisionTagsStr = new string[]
//             {
//                 "OCS/GlobalBeacon/Opt2"
//             };
//             var gsO2CollisionTags = new List<string>(gsO2CollisionTagsStr);
//             my.gsO2Obj = new GameObject("gs02obj");
//             my.gsO2Obj.transform.SetParent(my.rootSObj.transform);
//             
//             AW_ContactSender.CreateContactSender(my.gsO2Obj,
//                 my.rootSObj.transform,
//                 0,
//                 0.1f,
//                 new Vector3(0f,0f,0f),
//                 new Vector3(0f,0f,0f),
//                 gsO2CollisionTags);
//             
//             var gsO3CollisionTagsStr = new string[]
//             {
//                 "OCS/GlobalBeacon/Opt3"
//             };
//             var gsO3CollisionTags = new List<string>(gsO3CollisionTagsStr);
//             my.gsO3Obj = new GameObject("gs03obj");
//             my.gsO3Obj.transform.SetParent(my.rootSObj.transform);
//             
//             AW_ContactSender.CreateContactSender(my.gsO3Obj,
//                 my.rootSObj.transform,
//                 0,
//                 0.1f,
//                 new Vector3(0f,0f,0f),
//                 new Vector3(0f,0f,0f),
//                 gsO3CollisionTags);
//
//             // var paramSenderList = new List<string>();
//             // paramSenderList.AddRange(gsCollisionTagsStr);
//             // paramSenderList.AddRange(gsO1CollisionTagsStr);
//             // paramSenderList.AddRange(gsO2CollisionTagsStr);
//             // paramSenderList.AddRange(gsO3CollisionTagsStr);
//             
//             // AW_DirectTreeManager.ParameterList.AddRange(paramSenderList);
//         }
//
//         private void CreateContactReceivers()
//         {
//             Debug.Log("Creating Contact Receivers...");
//             var spineT = my.rootTransform;
//
//             if (spineT == null)
//             {
//                 Debug.LogError("Spine transform not found!");
//                 return;
//             }
//
//             my.rootRObj = new GameObject("OCS_Receivers");
//             my.rootRObj.transform.SetParent(spineT, false);
//             
//             string[] grCollisionTagsStr = new[]
//             {
//                 "OCS/GlobalBeacon",
//                 "OCS/GlobalBeacon/Ver/0",
//             };
//             var grCollisionTags = new List<string>(grCollisionTagsStr);
//             AW_ContactReceiver.CreateContactReceiver(my.rootRObj,
//                 my.rootRObj.transform,
//                 0,
//                 5f,
//                 0f,
//                 new Vector3(0, 0, 0),
//                 new Vector3(0, 0, 0),
//                 grCollisionTags,
//                 false,
//                 true,
//                 false,
//                 1,
//                 $"OCS/GlobalBeacon/Connect");
//             
//             string[] grFCollisionTagsStr = new[]
//             {
//                 "OCS/GlobalBeacon/Finish"
//             };
//             var grFCollisionTags = new List<string>(grFCollisionTagsStr);
//             
//             my.grFObj = new GameObject("grFobj");
//             my.grFObj.transform.SetParent(my.rootRObj.transform);
//             
//             AW_ContactReceiver.CreateContactReceiver(my.grFObj,
//                 my.rootRObj.transform,
//                 0,
//                 5f,
//                 0f,
//                 new Vector3(0, 0, 0),
//                 new Vector3(0, 0, 0),
//                 grCollisionTags,
//                 false,
//                 true,
//                 false,
//                 1,
//                 $"OCS/GlobalBeacon/Finish");
//             
//             string[] grO1CollisionTagsStr = new[]
//             {
//                 "OCS/GlobalBeacon/Opt1",
//             };
//             var grO1CollisionTags = new List<string>(grO1CollisionTagsStr);
//             
//             my.grO1Obj = new GameObject("grO1obj");
//             my.grO1Obj.transform.SetParent(my.rootRObj.transform);
//             
//             AW_ContactReceiver.CreateContactReceiver(my.grO1Obj,
//                 my.rootRObj.transform,
//                 0,
//                 5f,
//                 0f,
//                 new Vector3(0, 0, 0),
//                 new Vector3(0, 0, 0),
//                 grO1CollisionTags,
//                 false,
//                 true,
//                 false,
//                 0,
//                 $"OCS/GlobalBeacon/R/Opt1");
//             
//             string[] grO2CollisionTagsStr = new[]
//             {
//                 "OCS/GlobalBeacon/Opt2"
//             };
//             var grO2CollisionTags = new List<string>(grO2CollisionTagsStr);
//             
//             my.grO2Obj = new GameObject("grO2obj");
//             my.grO2Obj.transform.SetParent(my.rootRObj.transform);
//             
//             AW_ContactReceiver.CreateContactReceiver(my.grO2Obj,
//                 my.rootRObj.transform,
//                 0,
//                 5f,
//                 0f,
//                 new Vector3(0, 0, 0),
//                 new Vector3(0, 0, 0),
//                 grO2CollisionTags,
//                 false,
//                 true,
//                 false,
//                 0,
//                 $"OCS/GlobalBeacon/R/Opt2");
//             
//             string[] grO3CollisionTagsStr = new[]
//             {
//                 "OCS/GlobalBeacon/Opt3"
//             };
//             var grO3CollisionTags = new List<string>(grO3CollisionTagsStr);
//             
//             my.grO3Obj = new GameObject("grO3obj");
//             my.grO3Obj.transform.SetParent(my.rootRObj.transform);
//             
//             AW_ContactReceiver.CreateContactReceiver(my.grO3Obj,
//                 my.rootRObj.transform,
//                 0,
//                 5f,
//                 0f,
//                 new Vector3(0, 0, 0),
//                 new Vector3(0, 0, 0),
//                 grO3CollisionTags,
//                 false,
//                 true,
//                 false,
//                 0,
//                 $"OCS/GlobalBeacon/R/Opt3");
//             
//             // var paramSenderList = new List<string>();
//             // paramSenderList.AddRange(grCollisionTagsStr);
//             // paramSenderList.AddRange(grO1CollisionTagsStr);
//             // paramSenderList.AddRange(grO2CollisionTagsStr);
//             // paramSenderList.AddRange(grO3CollisionTagsStr);
//             //
//             // AW_DirectTreeManager.ParameterList.AddRange(paramSenderList);
//         }
//
//         private AacFlBlendTreeDirect CreateContactTree()
//         {
//             Debug.Log("Creating Contact Tree...");
//             var dbt = aac.NewBlendTree().Direct();
//
//             // Parameters
//             var floatParam = aac.NoAnimator().FloatParameter("Internal/Float");
//             var enableParam = aac.NoAnimator().FloatParameter("OCS/Enable");
//             
//             var finishParam = aac.NoAnimator().FloatParameter("OCS/GlobalBeacon/Finish");
//             var connectParam = aac.NoAnimator().FloatParameter("OCS/GlobalBeacon/Connect");
//             
//             // Receivers
//             var opt1RParam = aac.NoAnimator().FloatParameter("OCS/GlobalBeacon/R/Opt1");
//             var opt2RParam = aac.NoAnimator().FloatParameter("OCS/GlobalBeacon/R/Opt2");
//             var opt3RParam = aac.NoAnimator().FloatParameter("OCS/GlobalBeacon/R/Opt3");
//             
//             // Senders
//             var opt1SParam = aac.NoAnimator().FloatParameter("OCS/GlobalBeacon/S/Opt1");
//             var opt2SParam = aac.NoAnimator().FloatParameter("OCS/GlobalBeacon/S/Opt2");
//             var opt3SParam = aac.NoAnimator().FloatParameter("OCS/GlobalBeacon/S/Opt3");
//
//             var pStrings = new string[]
//             {
//                 "Internal/Float",
//                 "OCS/Enable",
//                 "OCS/GlobalBeacon/Finish",
//                 "OCS/GlobalBeacon/Connect",
//                 "OCS/GlobalBeacon/R/Opt1",
//                 "OCS/GlobalBeacon/R/Opt2",
//                 "OCS/GlobalBeacon/R/Opt3",
//                 "OCS/GlobalBeacon/S/Opt1",
//                 "OCS/GlobalBeacon/S/Opt2",
//                 "OCS/GlobalBeacon/S/Opt3",
//                 "OCS/GlobalBeacon",
//                 "OCS/GlobalBeacon/Ver/0"
//             };
//             
//             my.parameterList.AddRange(pStrings);
//             
//             #region Animation Clips
//             
//             // Finish Sender Enabler
//             var disableFinishS = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.gsFObj).WithOneFrame(0.0f);
//             });
//             
//             var enableFinishS = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.gsFObj).WithOneFrame(1.0f);
//             });
//
//             var enableFinishS1D = aac.NewBlendTree()
//                 .Simple1D(finishParam)
//                 .WithAnimation(disableFinishS, 0.0f)
//                 .WithAnimation(enableFinishS, 1.0f);
//             
//             // Finish Receiver Enabler
//             var disableFinishR = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.grFObj).WithOneFrame(0.0f);
//             });
//             
//             var enableFinishR = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.grFObj).WithOneFrame(1.0f);
//             });
//
//             var enableFinishR1D = aac.NewBlendTree()
//                 .Simple1D(finishParam)
//                 .WithAnimation(disableFinishR, 0.0f)
//                 .WithAnimation(enableFinishR, 1.0f);
//             
//             // Opt1 Sender Enabler
//             var disableOpt1S = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.gsO1Obj).WithOneFrame(0.0f);
//             });
//             
//             var enableOpt1S = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.gsO1Obj).WithOneFrame(1.0f);
//             });
//
//             var enableOpt1S1D = aac.NewBlendTree()
//                 .Simple1D(opt1SParam)
//                 .WithAnimation(disableOpt1S, 0.0f)
//                 .WithAnimation(enableOpt1S, 1.0f);
//             
//             // Opt2 Sender Enabler
//             var disableOpt2S = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.gsO2Obj).WithOneFrame(0.0f);
//             });
//             
//             var enableOpt2S = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.gsO2Obj).WithOneFrame(1.0f);
//             });
//
//             var enableOpt2S1D = aac.NewBlendTree()
//                 .Simple1D(opt2SParam)
//                 .WithAnimation(disableOpt2S, 0.0f)
//                 .WithAnimation(enableOpt2S, 1.0f);
//             
//             // Opt3 Sender Enabler
//             var disableOpt3S = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.gsO3Obj).WithOneFrame(0.0f);
//             });
//             
//             var enableOpt3S = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.gsO3Obj).WithOneFrame(1.0f);
//             });
//
//             var enableOpt3S1D = aac.NewBlendTree()
//                 .Simple1D(opt3SParam)
//                 .WithAnimation(disableOpt3S, 0.0f)
//                 .WithAnimation(enableOpt3S, 1.0f);
//             
//             // Opt1 Receiver Enabler
//             var disableOpt1R = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.grO1Obj).WithOneFrame(0.0f);
//             });
//             
//             var enableOpt1R = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.grO1Obj).WithOneFrame(1.0f);
//             });
//
//             var enableOpt1R1D = aac.NewBlendTree()
//                 .Simple1D(opt1RParam)
//                 .WithAnimation(disableOpt1R, 0.0f)
//                 .WithAnimation(enableOpt1R, 1.0f);
//             
//             // Opt2 Receiver Enabler
//             var disableOpt2R = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.grO2Obj).WithOneFrame(0.0f);
//             });
//             
//             var enableOpt2R = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.grO2Obj).WithOneFrame(1.0f);
//             });
//
//             var enableOpt2R1D = aac.NewBlendTree()
//                 .Simple1D(opt2RParam)
//                 .WithAnimation(disableFinishS, 0.0f)
//                 .WithAnimation(enableFinishS, 1.0f);
//             
//             // Opt3 Receiver Enabler
//             var disableOpt3R = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.grO3Obj).WithOneFrame(0.0f);
//             });
//             
//             var enableOpt3R = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.grO3Obj).WithOneFrame(1.0f);
//             });
//
//             var enableOpt3R1D = aac.NewBlendTree()
//                 .Simple1D(opt2RParam)
//                 .WithAnimation(disableOpt3R, 0.0f)
//                 .WithAnimation(enableOpt3R, 1.0f);
//             
//             // Connect Sender Enabler
//             var disableConnS = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.rootSObj).WithOneFrame(0.0f);
//             });
//             
//             var enableConnS = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.rootSObj).WithOneFrame(1.0f);
//             });
//
//             var enableConnS1D = aac.NewBlendTree()
//                 .Simple1D(connectParam)
//                 .WithAnimation(disableConnS, 0.0f)
//                 .WithAnimation(enableConnS, 1.0f);
//             
//             // Connect Receiver Enabler
//             var disableConnR = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.rootRObj).WithOneFrame(0.0f);
//             });
//             
//             var enableConnR = aac.NewClip().NonLooping().Animating(clip =>
//             {
//                 clip.Animates(my.rootRObj).WithOneFrame(1.0f);
//             });
//
//             var enableConnR1D = aac.NewBlendTree()
//                 .Simple1D(connectParam)
//                 .WithAnimation(disableConnR, 0.0f)
//                 .WithAnimation(enableConnR, 1.0f);
//             
//             
//             #endregion
//             // Enable Connection
//             var enableDbt = aac.NewBlendTree().Direct();
//
//             enableDbt.WithAnimation(enableFinishS, connectParam)
//                 .WithAnimation(enableFinishS1D, connectParam)
//                 .WithAnimation(enableOpt1S1D, connectParam)
//                 .WithAnimation(enableOpt2S1D, connectParam)
//                 .WithAnimation(enableOpt3S1D, connectParam)
//                 .WithAnimation(enableFinishR1D, connectParam)
//                 .WithAnimation(enableOpt1R1D, connectParam)
//                 .WithAnimation(enableOpt2R1D, connectParam)
//                 .WithAnimation(enableOpt3R1D, connectParam);
//
//             dbt.WithAnimation(enableDbt, floatParam);
//             
//             return dbt;
//         }
//     }
// }
// #endif