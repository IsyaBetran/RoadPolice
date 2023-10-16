Shader "Unlit/ToonShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline Width", Range(0, 0.1)) = 0.01
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }

            Pass
            {
                ZWrite On
                ZTest LEqual
                Cull Off
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
                fixed4 _OutlineColor;
                float _OutlineWidth;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float2 ddx_uv = ddx(i.uv);
                    float2 ddy_uv = ddy(i.uv);
                    float2 gradient = float2(length(ddx_uv), length(ddy_uv));

                    float2 outlineUV = _OutlineWidth * gradient;
                    float outline = saturate(fwidth(gradient) - outlineUV);

                    fixed4 outlineColor = _OutlineColor;

                    fixed4 mainColor = tex2D(_MainTex, i.uv);

                    return lerp(outlineColor, mainColor, outline);
                }
                ENDCG
            }
        }
}