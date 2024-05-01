Shader "Unlit/NewUnlitShader"
{
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Speed ("Speed", Float) = 0.1
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Speed;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Animate UVs here in the vertex shader
                float offset = _Time.y * _Speed;
                o.uv = v.uv + float2(-offset, offset);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Sample the texture at the animated UVs
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}