Shader "Custom/BRIP_Fresnel_Reflection"
{
    
    Properties
        {
            _BaseColor ("_BaseColor", Color) = (0, 0, 0, 1)
            _MainTex ("Texture", 2D) = "white" {}

            _Smoothness ("Smoothness", Range(0, 1)) = 0
            _Metallic ("Metalness", Range(0, 1)) = 0

            _RimColor ("_RimColor", Color) = (1,1,1,1)
            [PowerSlider(4)] _RimPower ("_RimPower", Range(0.25, 10)) = 1

            // --- Custom Env Cubemap controls (OFF by default so current look is unchanged) ---
            [Toggle(_USE_ENVCUBE)] _UseEnvCube ("Use Custom Env Cubemap", Float) = 0
            [NoScaleOffset] _EnvCube ("Env Cubemap (HDRI/Cube)", CUBE) = "" {}
            _EnvCubeIntensity ("Env Cubemap Intensity", Range(0, 4)) = 1
            _EnvCubeBlend ("Env Cubemap Blend", Range(0, 1)) = 1
            // Adjust to match your cubemap mip count (if mips enabled)
            _EnvCubeMipCount ("Env Cubemap MipCount", Range(1, 12)) = 7
        }

        SubShader
        {
            Tags { "RenderType"="Opaque" }
            LOD 200
            Cull Off

            CGPROGRAM
            #if !defined(UNITY_USES_HDRP) && !defined(UNITY_USES_URP)
            #pragma surface surf Standard fullforwardshadows
            #pragma target 3.0

            // Feature toggle
            #pragma shader_feature _USE_ENVCUBE

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _BaseColor;
            half _Smoothness;
            half _Metallic;
            float3 _RimColor;
            float _RimPower;

            // Declare cubemap using Unity macro (no separate SAMPLER macro needed)
            #if defined(_USE_ENVCUBE)
                UNITY_DECLARE_TEXCUBE(_EnvCube);
                half _EnvCubeIntensity;
                half _EnvCubeBlend;
                half _EnvCubeMipCount;
            #endif

            struct Input
            {
                float2 uv_MainTex;
                float3 worldNormal;
                float3 viewDir;
                INTERNAL_DATA
            };

            // Optional (kept from your original)
            half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten)
            {
                half NdotL = dot (s.Normal, lightDir);
                half4 c;
                c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
                c.a = s.Alpha;
                return c;
            }

            // Fresnel (Schlick)
            inline half3 FresnelSchlick(half cosTheta, half3 F0)
            {
                return F0 + (1.0h - F0) * pow(saturate(1.0h - cosTheta), 5.0h);
            }

            // Sample the cubemap; use explicit LOD where supported
            inline half3 SampleEnvCube(UNITY_ARGS_TEXCUBE(tex), half3 R, half perceptualRoughness, half mipCount)
            {
                // Convert roughness to lod
                half lod = saturate(perceptualRoughness) * mipCount;

                // Platforms with explicit LOD support (D3D11/Metal/GLCore, etc.)
                #if defined(SHADER_API_D3D11) || defined(SHADER_API_METAL) || defined(SHADER_API_GLCORE) || defined(SHADER_API_PSSL) || defined(SHADER_API_VULKAN)
                    return UNITY_SAMPLE_TEXCUBE_LOD(tex, R, lod).rgb;
                #else
                    // Fallback: no LOD (looks sharper if Smoothness < 1)
                    return UNITY_SAMPLE_TEXCUBE(tex, R).rgb;
                #endif
            }

            void surf (Input i, inout SurfaceOutputStandard o)
            {
                // Albedo * tint
                fixed4 col = tex2D(_MainTex, i.uv_MainTex);
                col *= _BaseColor;
                o.Albedo = col.rgb;

                // PBR params
                o.Metallic   = _Metallic;
                o.Smoothness = _Smoothness;

                // Original Fresnel rim glow (unchanged)
                float3 n = normalize(i.worldNormal);
                float3 v = normalize(i.viewDir);
                float  vf = saturate(dot(n, v));
                float  rim = pow(saturate(1.0 - vf), _RimPower);
                float3 rimColor = rim * _RimColor;
                o.Emission = rimColor;

                // Optional: add custom cubemap reflection to emission
                #if defined(_USE_ENVCUBE)
                {
                    half3 R = reflect(-v, n);
                    half rough = saturate(1.0h - _Smoothness);

                    half3 env = SampleEnvCube(UNITY_PASS_TEXCUBE(_EnvCube), R, rough, _EnvCubeMipCount);

                    // F0: 0.04 for dielectrics, albedo for metals; steel -> high metallic
                    half3 F0 = lerp(0.04h.xxx, o.Albedo, _Metallic);
                    half3 F  = FresnelSchlick(vf, F0);

                    half metalW = saturate(_Metallic);
                    half glossW = saturate(_Smoothness);

                    half3 envTerm = env * F * metalW * glossW * _EnvCubeIntensity;

                    // Add to emission so we don't disturb Standard lighting
                    o.Emission += envTerm * _EnvCubeBlend;
                }
                #endif
            }
            #endif
            ENDCG
        }

        FallBack "Diffuse"
        FallBack "Standard"



}
