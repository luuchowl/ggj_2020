Shader "Unlit/SwordSlash"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_RampTex("Ramp Tex", 2D) = "white" {}
		_NoiseTex("Noise Tex", 2D) = "gray" {}
		_Clip("Culling Transparency", Range(-1, 1)) = 0
		_ClipNoise("Culling Noise", Range(0, 1)) = 1
		_FallOffLeft("FallOffLeft", Float) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Zwrite Off
			Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D _RampTex;
			sampler2D _NoiseTex;
			float _Clip;
			float _ClipNoise;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				i.uv += fixed2(_Clip, 0);
                fixed4 col = tex2D(_MainTex, saturate( i.uv + tex2D(_NoiseTex, frac(i.uv - half2(_Time.x * 30, 0))  * (i.uv.y * 0.2))));
				
                //UNITY_APPLY_FOG(i.fogCoord, col);
				col.a =	saturate(_ClipNoise-i.uv.x - tex2D(_NoiseTex, i.uv * 0.2 - half2(_Time.x, _Time.x) * 3));
				
                //return tex2D(_RampTex, fixed2(col.r, 0));
				return col * 2;
				//return tex2D(_RampTex, i.uv);
            }
            ENDCG
        }
    }
}

