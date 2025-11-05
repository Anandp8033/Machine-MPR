Shader "Custom/HighlightShaderWithAlphaRange"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (1,1,0,1)
        _OutlineThickness ("Outline Thickness", Float) = 0.02
        _EnableOutline ("Enable Outline", Float) = 1.0

        _FillColor ("Fill Color", Color) = (1,0,0,1)
        _MinAlpha ("Min Transparency", Range(0,1)) = 0.3
        _MaxAlpha ("Max Transparency", Range(0,1)) = 0.8
        _BlinkSpeed ("Blink Speed", Float) = 2.0
        _BlinkIntensity ("Blink Intensity", Range(0,1)) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        // Outline Pass
        Pass
        {
            Name "OUTLINE"
            Cull Front
            ZWrite On
            ZTest Less
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _OutlineColor;
            float _OutlineThickness;
            float _EnableOutline;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                float3 norm = normalize(v.normal);
                v.vertex.xyz += norm * _OutlineThickness;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                if (_EnableOutline < 0.5)
                    discard;
                return _OutlineColor;
            }
            ENDCG
        }

        // Fill Pass with Blink and Alpha Range
        Tags { "LightMode"="ForwardBase" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        CGPROGRAM
        #pragma surface surf Lambert alpha

        fixed4 _FillColor;
        float _MinAlpha;
        float _MaxAlpha;
        float _BlinkSpeed;
        float _BlinkIntensity;

        struct Input {
            float dummy;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float blink = abs(sin(_Time.y * _BlinkSpeed)) * _BlinkIntensity;
            float alpha = lerp(_MinAlpha, _MaxAlpha, blink);
            o.Albedo = _FillColor.rgb * blink;
            o.Emission = _FillColor.rgb * blink; // Glow effect
            o.Alpha = alpha;
        }
        ENDCG
    }

    FallBack "Transparent/Diffuse"
}
