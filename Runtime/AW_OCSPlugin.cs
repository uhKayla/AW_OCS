using System.Collections.Generic;
using ANGELWARE.AW_OCS;
using nadena.dev.ndmf;
using UnityEngine;
using UnityEngine.Animations;

[assembly: ExportsPlugin(typeof(AW_OCSPlugin))]

namespace ANGELWARE.AW_OCS
{
    public class AW_OCSPlugin : Plugin<AW_OCSPlugin>
    {
        private AW_ContactReceiver _contactReceiver;
        private AW_ContactSender _contactSender;
        private AW_OCS _component;
        
        public override string QualifiedName => "AW.OCS";
        
        protected override void Configure()
        {
            InPhase(BuildPhase.Generating).AfterPlugin("AW.APS")
                .Run("AW_OCS Build Dynamics", ctx =>
            {
                _component = Object.FindObjectOfType<AW_OCS>();
                if (_component == null) return;
                _contactReceiver = new AW_ContactReceiver();
                _contactSender = new AW_ContactSender();
                    
                var worldGameObject = new GameObject("World");
                var rootGameObject = new GameObject("OCS_Root");
                rootGameObject.transform.SetParent(_component.transform);
                
                var senders = CreateSenders(worldGameObject);
                senders.transform.SetParent(rootGameObject.transform);

                var receiver = CreateReceivers(worldGameObject);
                receiver.transform.SetParent(rootGameObject.transform);
            });
        }

        private GameObject CreateSenders(GameObject world)
        {
            var sendersObject = new GameObject("Senders");
            
            var versionObject = new GameObject("OCS_VersionBeacon");
            _contactSender.CreateContactSender(versionObject, null, 0, 0.01f, Vector3.zero, Vector3.zero,
                new List<string> { "OCS_V1" });
            versionObject.transform.SetParent(sendersObject.transform, false);

            var optionObjects = new GameObject("OCS_Senders");
            
            optionObjects.transform.SetParent(sendersObject.transform, false);
            
            var opt1Object = new GameObject("OCS_Option1Sender");
            var opt2Object = new GameObject("OCS_Option2Sender");
            var opt3Object = new GameObject("OCS_Option3Sender");
            var optFinishObject = new GameObject("OCS_FinishSender");
            
            opt1Object.transform.SetParent(optionObjects.transform, false);
            opt2Object.transform.SetParent(optionObjects.transform, false);
            opt3Object.transform.SetParent(optionObjects.transform, false);
            optFinishObject.transform.SetParent(optionObjects.transform, false);
            
            _contactSender.CreateContactSender(opt1Object, null, 0, 0.01f, Vector3.zero, Vector3.zero,
                new List<string> { "OCS_Option1" });
            
            _contactSender.CreateContactSender(opt2Object, null, 0, 0.01f, Vector3.zero, Vector3.zero,
                new List<string> { "OCS_Option2" });
            
            _contactSender.CreateContactSender(opt3Object, null, 0, 0.01f, Vector3.zero, Vector3.zero,
                new List<string> { "OCS_Option3" });
            
            _contactSender.CreateContactSender(optFinishObject, null, 0, 0.01f, Vector3.zero, Vector3.zero,
                new List<string> { "OCS_Finish" });
            
            var optConstraint = optionObjects.AddComponent<ParentConstraint>();
            optConstraint.AddSource(new ConstraintSource { sourceTransform = world.transform, weight = 1 });
            optConstraint.SetTranslationOffset(0, new Vector3(0, 0, 0));
            optConstraint.constraintActive = true;
            
            return sendersObject;
        }

        private GameObject CreateReceivers(GameObject world)
        {
            var receiversObject = new GameObject("Receivers");
            
            var versionObject = new GameObject("OCS_VersionReceiver");
            
            versionObject.transform.SetParent(receiversObject.transform, false);

            _contactReceiver.CreateContactReceiver(versionObject, null, 0, 1, 0,
                Vector3.zero, Vector3.zero, new List<string> { "OCS_V1" }, false, 
                true, true, 0, "OCS/Connected");
            
            var optionObjects = new GameObject("OCS_Receivers");
            
            optionObjects.transform.SetParent(receiversObject.transform, false);
            
            var opt1Object = new GameObject("OCS_Option1Receiver");
            var opt2Object = new GameObject("OCS_Option2Receiver");
            var opt3Object = new GameObject("OCS_Option3Receiver");
            var optFinishObject = new GameObject("OCS_FinishReceiver");
            
            opt1Object.transform.SetParent(optionObjects.transform, false);
            opt2Object.transform.SetParent(optionObjects.transform, false);
            opt3Object.transform.SetParent(optionObjects.transform, false);
            optFinishObject.transform.SetParent(optionObjects.transform, false);
            
            _contactReceiver.CreateContactReceiver(opt1Object, null, 0, 1, 0,
                Vector3.zero, Vector3.zero, new List<string> { "OCS_Option1" }, false, 
                true, true, 0, "OCS/Opt1");
            
            _contactReceiver.CreateContactReceiver(opt2Object, null, 0, 1, 0,
                Vector3.zero, Vector3.zero, new List<string> { "OCS_Option2" }, false, 
                true, true, 0, "OCS/Opt2");
            
            _contactReceiver.CreateContactReceiver(opt3Object, null, 0, 1, 0,
                Vector3.zero, Vector3.zero, new List<string> { "OCS_Option3" }, false, 
                true, true, 0, "OCS/Opt3");
            
            _contactReceiver.CreateContactReceiver(optFinishObject, null, 0, 1, 0,
                Vector3.zero, Vector3.zero, new List<string> { "OCS_Finish" }, false, 
                true, true, 0, "OCS/Finish");
            
            var optConstraint = optionObjects.AddComponent<ParentConstraint>();
            optConstraint.AddSource(new ConstraintSource { sourceTransform = world.transform, weight = 1 });
            optConstraint.SetTranslationOffset(0, new Vector3(0, 0, 0));
            optConstraint.constraintActive = true;

            if (_component.nsfwTouch)
            {
                var nsfwTouchObject = new GameObject("OCS_NSFWTouch");
                _contactReceiver.CreateContactReceiver(nsfwTouchObject, _component.nsfwTouchTransform, 0, _component.nsfwTouchRadius, 0,
                    Vector3.zero, Vector3.zero, new List<string> { "Hand", "Finger" }, false, 
                    true, true, 0, "OCS/NSFW/Touch");
                nsfwTouchObject.transform.SetParent(_component.nsfwTouchTransform, false);
            }
            
            if (_component.dickTouch)
            {
                var nsfwTouchObject = new GameObject("OCS_DickTouch");
                _contactReceiver.CreateContactReceiver(nsfwTouchObject, _component.dickTouchTransform, 0, _component.dickTouchRadius, 0,
                    Vector3.zero, Vector3.zero, new List<string> { "Hand", "Finger" }, false, 
                    true, true, 0, "OCS/NSFW/TouchDick");
                nsfwTouchObject.transform.SetParent(_component.dickTouchTransform, false);
            }

            return receiversObject;

        }
    }
}