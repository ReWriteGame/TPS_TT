using UnityEngine;

namespace Extension.LayerMask
{
    public static class ExtensionsLayerMask
    {
        public static bool ContainLayer(this UnityEngine.LayerMask mask, int layer) => (mask.value & 1 << layer) > 0;
        // make all layers as unity variables for comparison?
    }
}