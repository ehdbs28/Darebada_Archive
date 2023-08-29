#ifndef BACKFACEOUTLINES_INCLUDED
#define BACKFACEOUTLINES_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Attributes {
    float4 positionOS       : POSITION;
    float3 normalOS         : NORMAL;
    #ifdef USE_PRECALCULATED_OUTLINE_NORMALS
    float3 smoothNormalOS   : TEXCOORD1;
    #endif
};

struct VertexOutput {
    float4 positionCS   : SV_POSITION;
};

// Properties
float _Thickness;
float4 _Color;
float _DepthOffset;

float3 GetWorldScaleFromModelMatrix(float4x4 modelMatrix)
{
    float3 scale;
    scale.x = length(modelMatrix[0].xyz);
    scale.y = length(modelMatrix[1].xyz);
    scale.z = length(modelMatrix[2].xyz);
    return scale;
}

VertexOutput Vertex(Attributes input) {
    VertexOutput output = (VertexOutput)0;

    float3 normalOS = input.normalOS;
    #ifdef USE_PRECALCULATED_OUTLINE_NORMALS
    normalOS = input.smoothNormalOS;
    #else
    normalOS = input.normalOS;
    #endif

    float3 worldScale = GetWorldScaleFromModelMatrix(unity_ObjectToWorld);

    float3 posOS = input.positionOS.xyz + normalOS * _Thickness / worldScale;
    output.positionCS = GetVertexPositionInputs(posOS).positionCS;

    float depthOffset = _DepthOffset;

#ifndef UNITY_REVERSED_Z
    depthOffset = -depthOffset;
#endif
    output.positionCS.z += depthOffset;

    return output;
}

float4 Fragment(VertexOutput input) : SV_Target{
    return _Color;
}

#endif