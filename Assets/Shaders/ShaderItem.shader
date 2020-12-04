Shader "Custom/ShaderItem"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Scale ("Scale", Float) = 0.3
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "AlphaTest" "IgnoreProjector" = "True" "DisableBatching" = "True" }
        Blend SrcAlpha OneMinusSrcAlpha
LOD 200

        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            uniform float _Scale;

            struct VertexInput
            {
                float4 vertex: POSITION;
                float2 uv: TEXCOORD0;
            };

            struct VertexOutput
            {
                float2 uv: TEXCOORD0;
                float4 vertex: SV_POSITION;
            };

            VertexOutput vert(VertexInput input)
            {
                VertexOutput o;
                o.vertex = mul(UNITY_MATRIX_P,
                    float4(UnityObjectToViewPos(float4(0.0, 0.0, 0.0, 1.0)), 1.0) +
                    float4(input.vertex.xz, 0.0, 0.0) *
                    float4(_Scale, _Scale, 1.0, 1.0)
                );
                o.uv = input.uv;

                return o;
            }

            float4 frag(VertexOutput input): COLOR
            {
                return tex2D(_MainTex, input.uv * float2(-1.0, -1.0) + float2(1.0, 1.0));
            }
            ENDCG
        }
    }
}
