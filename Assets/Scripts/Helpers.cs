using UnityEngine;

public static partial class Helpers {
    public static Vector3 ReplaceX(Vector3 vec, float x) {
        return new Vector3(x, vec.y, vec.z);
    }

    public static Vector3 ReplaceY(Vector3 vec, float y) {
        return new Vector3(vec.x, y, vec.z);
    }

    public static Vector3 ReplaceZ(Vector3 vec, float z) {
        return new Vector3(vec.x, vec.y, z);
    }
}