using UnityEngine;

namespace Utility.Math
{
    public enum Ease
    {
        EaseOut,
        EaseIn,
    }
    
    public class EasingFunctions : MonoBehaviour
    {
        
        private static float EaseOut(float t)
        {
            return 1 - Mathf.Pow(1 - t, 2);
        }
        
        private static float EaseIn(float t)
        {
            return Mathf.Pow(t, 2);
        }

        public static Vector3 LerpWithEase(Vector3 start, Vector3 end, float t, Ease ease)
        {
            // Use the specified easing function
            float easedT = ease switch
            {
                Ease.EaseOut => EaseOut(t),
                Ease.EaseIn => EaseIn(t),
                _ => t // Default to linear interpolation if the easing type is not recognized
            };

            return Vector3.Lerp(start, end, easedT);
        }
    }
}
