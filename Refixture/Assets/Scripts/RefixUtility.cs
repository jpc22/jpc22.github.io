using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RefixUtilities
{
    public static class RefixUtility
    {
        public static Bounds GetCombinedBoundingBoxOfChildren(Transform root)
        {
            if (root == null)
            {
                throw new System.ArgumentException("The supplied transform was null");
            }

            var colliders = root.GetComponentsInChildren<Collider>();
            if (colliders.Length == 0)
            {
                throw new System.ArgumentException("The supplied transform " + root?.name + " does not have any children with colliders");
            }

            Bounds totalBBox = colliders[0].bounds;
            foreach (var collider in colliders)
            {
                if (!collider.gameObject.CompareTag("Trigger"))
                {
                    totalBBox.Encapsulate(collider.bounds);
                }
            }
            return totalBBox;
        }
    }

    public class CallbackCounter
    {
        private int _popCount;
        private int _callbackCt;
        private System.Action _allAccountedFor;
        public CallbackCounter(int popCount, System.Action callback)
        {
            _popCount = popCount;
            _allAccountedFor = callback;
            _callbackCt = 0;
        }

        public void Callback()
        {
            _callbackCt++;
            if (_callbackCt == _popCount)
            {
                _allAccountedFor();
            }
        }
    }
}