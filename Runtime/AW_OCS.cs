using System;
using UnityEngine;
using VRC.SDKBase;

namespace ANGELWARE.AW_OCS
{
    [Serializable]
    public class AW_OCS : MonoBehaviour, IEditorOnly
    {
        public bool nsfwTouch;
        public Transform nsfwTouchTransform;
        public float nsfwTouchRadius;
        
        public bool dickTouch;
        public Transform dickTouchTransform;
        public float dickTouchRadius;
    }
}