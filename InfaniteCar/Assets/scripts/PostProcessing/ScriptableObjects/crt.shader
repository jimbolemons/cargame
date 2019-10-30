Shader "Hidden/crt"
{
    Properties
    {
      //  _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
		// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct appdata members texCoord2,texCoord3,color1,color2,color3)
			#pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				
				// Offset the main texture coordinates.
				float2 texCoord2 = texCoord + DisplacementScroll;
				float2 texCoord3 = texCoord - DisplacementScroll;

				float4 color1 = tex2D(TextureSampler, texCoord);
				float4 color2 = tex2D(TextureSampler, texCoord2);
				float4 color3 = tex2D(TextureSampler, texCoord3);

				// Look up into the main texture.
				return float4(color1.r, color2.g, color3.b, color1.a);
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
