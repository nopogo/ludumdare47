using UnityEngine;
using System.Text.RegularExpressions;

namespace com.nopogo.Utils {
    public static class ConvertUtil {
        
        #region Color

        public static int ToInt(Color color) {
            return (Mathf.RoundToInt(color.a * 255) << 24) +
                   (Mathf.RoundToInt(color.r * 255) << 16) +
                   (Mathf.RoundToInt(color.g * 255) << 8) +
                   Mathf.RoundToInt(color.b * 255);
        }

        public static Color ToColor(int value) {
            float a = (value >> 24 & 0xFF) / 255f;
            float r = (value >> 16 & 0xFF) / 255f;
            float g = (value >> 8 & 0xFF) / 255f;
            float b = (value & 0xFF) / 255f;
            return new Color(r, g, b, a);
        }
        #endregion
    }
    
}

