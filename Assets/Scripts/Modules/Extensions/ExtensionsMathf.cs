using UnityEngine;

namespace Extension.Mathf
{
    public static class ExtensionsMathf
    {
        public static Vector2 NormalizedToOne(Vector2 vector)
        {
            return vector.magnitude > 1 ? vector.normalized : vector;
        }
    }
}
