Shader "Unlit/miaobian"
{
        Properties {
            _NotVisibleColor ("NotVisibleColor (RGB)", Color) = (0.3,0.3,0.3,1)
            _MainTex ("Base (RGB)", 2D) = "white" {}
        }
        SubShader {
            Tags { "Queue" = "Geometry+500" "RenderType"="Opaque" }
            LOD 200

            Pass {
                ZTest Greater
                Lighting Off
                ZWrite Off
                Color [_NotVisibleColor]
            }

            Pass {
                ZTest LEqual
                Material {
                    Diffuse (1,1,1,1)
                    Ambient (1,1,1,1)
                }
                Lighting On
                SetTexture [_MainTex] { combine texture }
            }

        }
        FallBack "Diffuse"
}
