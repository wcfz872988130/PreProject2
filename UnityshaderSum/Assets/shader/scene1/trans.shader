Shader "Custom/trans" {  
    Properties {  
        _Scale("Scale",Range(1,8)) = 2  
    }  
      
    SubShader {  
        tags{"queue" = "transparent" }  
          
        pass{  
            blend srcAlpha oneMinussrcalpha  
            zwrite off  //写入的z轴深度关掉  
          
            CGPROGRAM  
            #pragma vertex vert  
            #pragma fragment frag  
  
            #include "UnityCG.cginc"  
  
            struct v2f{  
                float4 pos : POSITION;  
                float4 col : COLOR;  
                float3 normal:TEXCOORD0;  
                float4 vertex:TEXCOORD1;  
            };  
  
            v2f vert(appdata_base v){  
                v2f o;  
                  
                o.pos = mul(UNITY_MATRIX_MVP,v.vertex);  
                o.col = fixed4(0,1,1,1);  
                o.vertex = v.vertex;  
                o.normal = v.normal;  
                return o;  
            }  
              
            float _Scale;  
              
            fixed4 frag(v2f v):COLOR  
            {  
                float3 N = mul((float3x3)_World2Object,v.normal);  
                N = normalize(N);  
                  
                float4 wpos = mul(_Object2World,v.vertex);  
                float3 V = _WorldSpaceCameraPos.xyz - wpos.xyz;  
                V = normalize(V);  
                  
                float bright = 1 -saturate(dot(N,V));  
                bright = pow(bright,_Scale);  
                  
                return fixed4(1,1,1,1)*bright;  
            }  
  
            ENDCG  
              
        }  
    }         
    FallBack "Diffuse"  
} 