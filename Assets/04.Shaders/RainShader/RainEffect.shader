Shader "Custom/RainEffect"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _TimeMultiplier("Time Multiplier", Range(0.1, 2.0)) = 0.5
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t
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
                float4 _MainTex_ST;
                float _TimeMultiplier;
                float _Time;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    // 중심을 기준으로 원형으로 확대되는 효과 생성
                    float2 center = float2(0.5, 0.5);
                    float radius = _Time * _TimeMultiplier;
                    float dist = distance(i.uv, center);

                    half4 col = tex2D(_MainTex, i.uv);

                    // 원형 범위 내의 픽셀에만 색상을 적용
                    if (dist < radius)
                    {
                        // 중심에서 멀어질수록 투명도 감소
                        col.a *= 1.0 - smoothstep(0.0, radius, dist);
                    }

                    return col;
                }
                ENDCG
            }
        }
}
