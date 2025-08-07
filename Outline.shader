Shader "Unlit/Outline"
{
    Properties
    {
        _Color ("Outline Color", Color) = (1,1,0,1)
        _Outline_Width("Outline Width", Range(1.0, 1.05)) = 1.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Front // This is the magic trick: it renders the back faces of the mesh.

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float _Outline_Width;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz *= _Outline_Width;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}