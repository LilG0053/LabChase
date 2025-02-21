Shader "Custom/TransparentMaskShader"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1, 1, 1, 0)
        _Transparency("Transparency", Range(0, 1)) = 0.5
        _BlockPosition("Block Position", Vector) = (0, 0, 0, 0)
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            Tags { "LightMode" = "UniversalForward" }

            ZWrite On
            ZTest LEqual
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 position : POSITION;
            };

            struct Varyings
            {
                float4 position : POSITION;
            };

            float4 _BaseColor;
            float _Transparency;
            float4 _BlockPosition;

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.position = TransformObjectToHClip(v.position);
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                // Check if the object is behind the block (block's Z position is in world space)
                if (i.position.z > _BlockPosition.z)
                {
                    // If behind the block, make it fully transparent
                    discard;
                }

            // Otherwise, return the base color and transparency
            return half4(_BaseColor.rgb, _Transparency);
        }
        ENDCG
    }
    }
        FallBack "Diffuse"
}
