#if UNITY_EDITOR
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Dynamics.Contact.Components;

namespace ANGELWARE.AW_OCS
{
    public class AW_ContactReceiver
    {
        /// <summary>
        /// Factory for creating VRC contact senders.
        /// </summary>
        /// <param name="senderRoot">Object to add to</param>
        /// <param name="rootTransform">Root Transform Object</param>
        /// <param name="shapeType">Shape; 0 = Sphere, 1 = Capsule</param>
        /// <param name="radius">Radius</param>
        /// <param name="height"></param>
        /// <param name="position">Position Offset</param>
        /// <param name="rotation">Rotation Offset (Quat)</param>
        /// <param name="collisionTags">List of Custom Collider Tags</param>
        /// <param name="allowSelf"></param>
        /// <param name="allowOthers"></param>
        /// <param name="localOnly"></param>
        /// <param name="receiverType">0 = Constant, 1 = Proximity, 2 = OnEnter</param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public VRCContactReceiver CreateContactReceiver(GameObject senderRoot, [CanBeNull] Transform rootTransform,
            int shapeType, float radius, float? height, Vector3 position, Vector3 rotation, List<string> collisionTags, 
            bool allowSelf, bool allowOthers, bool localOnly, int receiverType, string parameter)
        {
            VRCContactReceiver cR = senderRoot.GetComponent<VRCContactReceiver>();

            var qRotation = Quaternion.Euler(rotation);

            if (cR == null)
            {
                cR = senderRoot.AddComponent<VRCContactReceiver>();
            }

            float heightV = 0f;
            if (height == null)
            {
                heightV = 0f;
            }
            else
            {
                heightV = (float)height;
            }

            ContactBase.ShapeType shape;
            switch (shapeType)
            {
                case 0: shape = ContactBase.ShapeType.Sphere;
                    break;
                case 1: shape = ContactBase.ShapeType.Capsule;
                    break;
                default: shape = ContactBase.ShapeType.Sphere;
                    break;
            };

            ContactReceiver.ReceiverType rType ;
            switch (receiverType)
            {
                case 0: rType = ContactReceiver.ReceiverType.Constant;
                    break;
                case 1: rType = ContactReceiver.ReceiverType.OnEnter;
                    break;
                case 2: rType = ContactReceiver.ReceiverType.Proximity;
                    break;
                default: rType = ContactReceiver.ReceiverType.Constant;
                    break;
            };
            
            cR.rootTransform = rootTransform;
            cR.shapeType = shape;
            cR.radius = radius;
            cR.height = heightV;
            cR.position = position;
            cR.rotation = qRotation;
            cR.collisionTags = collisionTags;

            cR.allowSelf = allowSelf;
            cR.allowOthers = allowOthers;
            cR.localOnly = localOnly;
            cR.receiverType = rType;
            cR.parameter = parameter;

            return cR;
        }
    }
}
#endif