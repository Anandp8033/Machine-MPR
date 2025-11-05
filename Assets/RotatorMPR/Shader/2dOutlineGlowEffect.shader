Shader "Custom/Unlit/2dOutlineGlowEffect"
{

    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Glow Color", Color) = (0, 1, 1, 1)
        _GlowSize ("Glow Size", Range(0, 0.1)) = 0.05
        _GlowIntensity ("Glow Intensity", Range(0, 5)) = 2
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _GlowSize;
            float _GlowIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 col = tex2D(_MainTex, uv);

                // Original alpha
                float alpha = col.a;

                // Glow calculation
                float glow = 0.0;
                int samples = 8; // More samples = smoother glow

                for (int x = -samples; x <= samples; x++)
                {
                    for (int y = -samples; y <= samples; y++)
                    {
                        float2 offset = float2(x, y) * _GlowSize;
                        glow += tex2D(_MainTex, uv + offset).a;
                    }
                }

                glow = saturate(glow / (samples * samples)) * _GlowIntensity;

                // Combine glow and original sprite
                fixed4 glowColor = _OutlineColor * glow;
                return col + glowColor * (1 - alpha);
            }
            ENDCG
        }
    }

}
