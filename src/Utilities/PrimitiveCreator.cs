using System;
using UnityEngine;

namespace ColVis.Utilities
{
    [Flags]
    public enum PrimitiveCreationFlags
    {
        AutoParent = 1,
        ZeroTransform = 2,
        SetShader = 4,
        RemoveCollider = 8,
        AllFlags = 15,
    }

    public static class PrimitiveCreator
    {
        public static GameObject CreatePrimitive(PrimitiveType primitiveType, PrimitiveCreationFlags flags = PrimitiveCreationFlags.AllFlags)
        {
            GameObject primitive = GameObject.CreatePrimitive(primitiveType);
            if(flags.HasFlag(PrimitiveCreationFlags.SetShader)) primitive.GetComponent<Renderer>().material.shader = ColVis.Shader;
            if(flags.HasFlag(PrimitiveCreationFlags.RemoveCollider)) UnityEngine.Object.Destroy(primitive.GetComponent<Collider>());

            return primitive;
        }

        public static GameObject CreatePrimitive<T>(ColVisBase<T> colVisBase, PrimitiveCreationFlags flags = PrimitiveCreationFlags.AllFlags) where T : Collider
        {
            GameObject primitive = CreatePrimitive(colVisBase.PrimType);
            return primitive;
        }
    }
}