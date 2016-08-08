Shader "Unlit/Dissolve"
{
	Properties
	{
		_MainTex("MainTex",2D)="white"{}
		_NoiseTex("NoiseTex",2D)="white"{}
		_DissolveSpeed("DissolveSpeed",Float)=1
		_EdgeWidth("EdgeWidth",Range(0,0.5))=0.1
		_EdgeColor("EdgeColor",Color)=(1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			float _DissolveSpeed;
			fixed4 _EdgeColor;
			float _EdgeWidth;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float DissolveFactor=saturate(_Time.y/_DissolveSpeed);
				float noiseValue=tex2D(_NoiseTex,i.uv).r;
				if(noiseValue<=DissolveFactor)
				{
					discard;
				}
				fixed4 texColor = tex2D(_MainTex, i.uv);
				float EdgeFactor=saturate((noiseValue-DissolveFactor)/(_EdgeWidth*DissolveFactor));
				float4 BlendColor=texColor*_EdgeColor;
				return lerp(texColor,BlendColor,1-EdgeFactor);
			}
			ENDCG
		}
	}
}
