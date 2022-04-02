Shader "Unlit/DissolveVehicle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DissolveTex ("Dissolve texture", 2D) = "white" {}
        _Dissolve ("Dissolve", Range(0,1)) = 0
        _DissolveWidth ("Dissolve width", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        
        Blend SrcAlpha OneMinusSrcAlpha 

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
                float2 uv1 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _DissolveTex;
            float4 _DissolveTex_ST;
            float _Dissolve;
            float _DissolveWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv1 = TRANSFORM_TEX(v.uv, _DissolveTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                _Dissolve = _Dissolve * (1 + _DissolveWidth) - _DissolveWidth;
                // sample the texture
                float mask = smoothstep(_Dissolve, _Dissolve + _DissolveWidth, tex2D(_DissolveTex, i.uv1).r);
                fixed4 col = tex2D(_MainTex, i.uv);
                col.a *= mask;
                return col;
            }
            ENDCG
        }
    }
}
