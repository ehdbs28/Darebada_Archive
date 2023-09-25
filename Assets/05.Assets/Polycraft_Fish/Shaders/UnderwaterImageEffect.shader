﻿Shader "Unlit/UnderwaterImageEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		
		_NoiseScale ("Noise Scale", float) = 1
		_NoiseFrequency ("Noise Frequency", float) = 1
		_NoiseSpeed ("Noise Speed", float) = 1
		_PixelOffset ("Pixel Offset", float) = 0.005
		_DepthStart("Depth Start", float) = 1
		_DepthDistance("Depth Distance", float) = 1
	}
		SubShader
	{
		// no culling or depyh
		Cull off Zwrite off Ztest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "noiseSimplex.cginc"
			#define M_PI 3.1415926535897932384626433832795

			uniform float _NoiseScale, _NoiseFrequency,	_NoiseSpeed, _PixelOffset;
			float _DepthStart, _DepthDistance;
			sampler2D _CameraDepthTexture;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 scrPos : TEXCOORD1;
			};

			//sampler2D _MainTex;
			//float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.scrPos = ComputeScreenPos(o.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			
			fixed4 frag (v2f i) : COLOR
			{
				float depthValue = Linear01Depth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r) * _ProjectionParams.z;
				depthValue = 1 - saturate((depthValue - _DepthStart) / _DepthDistance);

				float3 spos = float3(i.scrPos.x, i.scrPos.y, 0) * _NoiseFrequency;
				spos.z += _Time.x * _NoiseSpeed;
				float noise = _NoiseScale * ((snoise(spos) + 1) / 2);
				float4 noiseToDirection = float4(cos(noise*M_PI*2), sin(noise*M_PI*2),0,0);
				//float4 col = fixed4(noise, noise, noise, 1);
				float4 col = tex2Dproj(_MainTex, i.scrPos + (normalize(noiseToDirection) * _PixelOffset * depthValue));
				return col;
			}
			ENDCG
		}
	}
}