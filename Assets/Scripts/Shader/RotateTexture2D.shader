Shader "Custom/RotateTexture2D"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RotationX ("RotationX", Range(-360, 360)) = 0
        _RotationY ("RotationY", Range(-360, 360)) = 0
        _RotationZ ("RotationZ", Range(-360, 360)) = 0
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
            float _RotationX;
            float _RotationY;
            float _RotationZ;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float2 uv = v.uv;
                uv -= 0.5;
                float angleX = _RotationX * UNITY_PI / 180;
                float angleY = _RotationY * UNITY_PI / 180;
                float angleZ = _RotationZ * UNITY_PI / 180;
                float sX = sin(angleX);
                float cX = cos(angleX);
                float sY = sin(angleY);
                float cY = cos(angleY);
                float sZ = sin(angleZ);
                float cZ = cos(angleZ);
                uv = float2(uv.x * cX - uv.y * sY, uv.x * sZ + uv.y * cZ);
                uv += 0.5;

                o.uv = uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}