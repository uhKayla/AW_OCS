#if UNITY_EDITOR
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Dynamics.Contact.Components;

namespace ANGELWARE.AW_OCS
{
    public class AW_ContactSender
    {
        /// <summary>
        ///     Factory for creating VRC contact senders.
        /// </summary>
        /// <param name="senderRoot">Object to add to</param>
        /// <param name="rootTransform">Root Transform Object</param>
        /// <param name="shapeType">Shape; 0 = Sphere, 1 = Capsule</param>
        /// <param name="radius">Radius</param>
        /// <param name="position">Position Offset</param>
        /// <param name="rotation">Rotation Offset (Quat)</param>
        /// <param name="collisionTags">List of Custom Collider Tags</param>
        /// <returns></returns>
        public VRCContactSender CreateContactSender(GameObject senderRoot, [CanBeNull] Transform rootTransform,
            int shapeType, float radius, Vector3 position, Vector3 rotation, List<string> collisionTags)
        {
            var cS = senderRoot.GetComponent<VRCContactSender>();

            var qRotation = Quaternion.Euler(rotation);


            if (cS == null) cS = senderRoot.AddComponent<VRCContactSender>();

            ContactBase.ShapeType shape;
            switch (shapeType)
            {
                case 0:
                    shape = ContactBase.ShapeType.Sphere;
                    break;
                case 1:
                    shape = ContactBase.ShapeType.Capsule;
                    break;
                default:
                    shape = ContactBase.ShapeType.Sphere;
                    break;
            }

            ;

            cS.rootTransform = rootTransform;
            cS.shapeType = shape;
            cS.radius = radius;
            cS.position = position;
            cS.rotation = qRotation;
            cS.collisionTags = collisionTags;

            return cS;
        }
    }
}
#endif