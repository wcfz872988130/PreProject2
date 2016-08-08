	Shader "Custom/Texgen_CubeR_FragMine" {
       Properties {
            _Reflectivity ("Reflectivity", Range (0,1)) = 0.5
            _MainTex("Base", 2D) = "white"
            _Environment ("Environment", Cube) = "white"
        }
        SubShader {
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
                sampler2D _MainTex;
                samplerCUBE _Environment;
                float4 _MainTex_ST;
                float _Reflectivity;

                struct v2f {
                    float4  pos : SV_POSITION;
                    float2  uv : TEXCOORD0;
                    float3  R:TEXCOORD1;
                } ;
                float3 reflect(float3 I,float3 N)
                {
                    return I - 2.0*N *dot(N,I);
                }
                v2f vert (appdata_base v)
                {
                    v2f o;
                    o.pos = mul(UNITY_MATRIX_MVP,v.vertex);
                    o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
                    
                    float3 posW = mul(_Object2World,v.vertex).xyz;
                    float3 I = posW -_WorldSpaceCameraPos.xyz;
                    float3 N = mul((float3x3)_Object2World,v.normal);
                    N = normalize(N);
                    o.R = reflect(I,N);
                    return o;
                }
                float4 frag (v2f i) : COLOR
                {
                    float4 reflectiveColor = texCUBE(_Environment,i.R);
                    float4 decalColor = tex2D(_MainTex,i.uv);
                    float4 outp = lerp(decalColor,reflectiveColor,_Reflectivity);
                    return outp;
                }
                ENDCG
            }
        }
    }

