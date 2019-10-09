// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
 
 
Shader "Trolltunga/LowPolyWaves 2.0"
{
    Properties
    {
        _Color("Color", Color) = (1,0,0,1)
        _SpecColor("Specular Material Color", Color) = (1,1,1,1)
        _Shininess("Shininess", Float) = 1.0
        _WaveLength("Wave length", Float) = 0.5
        _WaveHeight("Wave height", Float) = 0.5
        _WaveSpeed("Wave speed", Float) = 1.0
        _RandomHeight("Random height", Float) = 0.5
        _RandomSpeed("Random Speed", Float) = 0.5
    }
        SubShader
    {
 
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Fog{ Mode Off }
 
        Pass
    {
 
        CGPROGRAM
#include "UnityCG.cginc"
#pragma target 5.0
#pragma vertex vert
#pragma geometry geom
#pragma fragment frag
 
        float rand(float3 co)
    {
        return frac(sin(dot(co.xyz ,float3(12.9898,78.233,45.5432))) * 43758.5453);
    }
 
    float rand2(float3 co)
    {
        return frac(sin(dot(co.xyz ,float3(19.9128,75.2,34.5122))) * 12765.5213);
    }
 
    float _WaveLength;
    float _WaveHeight;
    float _WaveSpeed;
    float _RandomHeight;
    float _RandomSpeed;
 
    uniform float4 _LightColor0;
 
    uniform half unity_FogDensity;
 
    uniform float4 _Color;
    uniform float4 _SpecColor;
    uniform float _Shininess;
 
    struct v2g
    {
        float4  pos : SV_POSITION;
        float3    norm : NORMAL;
        float2  uv : TEXCOORD0;
        half2 fogDepth: TEXCOORD3;
    };
 
    struct g2f
    {
        float4  pos : SV_POSITION;
        float3  norm : NORMAL;
        float2  uv : TEXCOORD0;
        float3 diffuseColor : TEXCOORD1;
        float3 specularColor : TEXCOORD2;
        half2 fogDepth: TEXCOORD3;
    };
 
    v2g vert(appdata_full v)
    {
        float3 v0 = mul(unity_ObjectToWorld, v.vertex).xyz;
 
        float phase0 = (_WaveHeight)* sin((_Time[1] * _WaveSpeed) + (v0.x * _WaveLength) + (v0.z * _WaveLength) + rand2(v0.xzz));
        float phase0_1 = (_RandomHeight)*sin(cos(rand(v0.xzz) * _RandomHeight * cos(_Time[1] * _RandomSpeed * sin(rand(v0.xxz)))));
 
        v0.y += phase0 + phase0_1;
 
        v.vertex.xyz = mul((float3x3)unity_WorldToObject, v0);
 //v.vertex.x=0;
        v2g OUT;
        OUT.pos = v.vertex;
        OUT.norm = v.normal;
        OUT.uv = v.texcoord;
 
        OUT.fogDepth.x = length(mul(UNITY_MATRIX_MV, v.vertex).xyz);
        OUT.fogDepth.y = OUT.fogDepth.x * unity_FogDensity;
 
        return OUT;
    }
 
    [maxvertexcount(3)]
    void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
    {
        float3 v0 = IN[0].pos.xyz;
        float3 v1 = IN[1].pos.xyz;
        float3 v2 = IN[2].pos.xyz;
 
        float3 centerPos = (v0 + v1 + v2) / 3.0;
 
        float3 vn = normalize(cross(v1 - v0, v2 - v0));
 
        float4x4 modelMatrix = unity_ObjectToWorld;
        float4x4 modelMatrixInverse = unity_WorldToObject;
 
        float3 normalDirection = normalize(
            mul(float4(vn, 0.0), modelMatrixInverse).xyz);
        float3 viewDirection = normalize(_WorldSpaceCameraPos
            - mul(modelMatrix, float4(centerPos, 0.0)).xyz);
        float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
        float attenuation = 1.0;
 
        float3 ambientLighting =
            UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;
 
        float3 diffuseReflection =
            attenuation * _LightColor0.rgb * _Color.rgb
            * max(0.0, dot(normalDirection, lightDirection));
 
        float3 specularReflection;
        if (dot(normalDirection, lightDirection) < 0.0)
        {
            specularReflection = float3(0.0, 0.0, 0.0);
        }
        else
        {
            specularReflection = attenuation * _LightColor0.rgb
                * _SpecColor.rgb * pow(max(0.0, dot(
                    reflect(-lightDirection, normalDirection),
                    viewDirection)), _Shininess);
        }
 
        g2f OUT;
        OUT.pos = UnityObjectToClipPos(IN[0].pos);
        OUT.norm = vn;
        OUT.uv = IN[0].uv;
        OUT.diffuseColor = ambientLighting + diffuseReflection;
        OUT.specularColor = specularReflection;
        OUT.fogDepth = IN[0].fogDepth;
        triStream.Append(OUT);
 
        OUT.pos = UnityObjectToClipPos(IN[1].pos);
        OUT.norm = vn;
        OUT.uv = IN[1].uv;
        OUT.diffuseColor = ambientLighting + diffuseReflection;
        OUT.specularColor = specularReflection;
        OUT.fogDepth = IN[1].fogDepth;
        triStream.Append(OUT);
 
        OUT.pos = UnityObjectToClipPos(IN[2].pos);
        OUT.norm = vn;
        OUT.uv = IN[2].uv;
        OUT.diffuseColor = ambientLighting + diffuseReflection;
        OUT.specularColor = specularReflection;
        OUT.fogDepth = IN[2].fogDepth;
        triStream.Append(OUT);
 
    }
 
    fixed4 frag(g2f IN) : COLOR
    {
        fixed3 clr = IN.specularColor + IN.diffuseColor;
        float fogAmt = IN.fogDepth.y * IN.fogDepth.y;
        fogAmt = exp(-fogAmt);
        fixed4 fogCol = unity_FogColor;
        clr = lerp(fogCol, clr, fogAmt);
 
        return fixed4(
            clr,
            1.0
        );
    }
 
        ENDCG
 
    }
    }
}