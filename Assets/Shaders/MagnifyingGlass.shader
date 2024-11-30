Shader "Custom/MagnifyingGlass"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MagnifyCenter ("Magnify Center", Vector) = (0.5, 0.5, 0, 0)
        _MagnifyRadius ("Magnify Radius", Float) = 0.2
        _MagnifyPower ("Magnify Power", Float) = 1.5
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

            // Propriétés du shader
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MagnifyCenter;
            float _MagnifyRadius;
            float _MagnifyPower;

            // Structure d'entrée pour le vertex shader
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Structure de sortie pour le fragment shader
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Vertex shader : Transformation de la position
            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            // Fonction de zoom pour les UV avec normalisation
            float2 MagnifyUV(float2 uv, float2 center, float radius, float power)
            {
                // Calcule la différence entre le point actuel et le centre de la loupe
                float2 delta = uv - center;
                float dist = length(delta);

                // Si le point est à l'intérieur du rayon, on applique un zoom
                if (dist < radius)
                {
                    float percent = (radius - dist) / radius;
                    uv -= delta * percent * power;
                }

                // Assurez-vous que les coordonnées UV restent dans les limites valides (0,1)
                uv = clamp(uv, 0.0, 1.0);

                return uv;
            }

            // Fragment shader : Application de l'effet de loupe
            fixed4 frag(v2f i) : SV_Target
            {
                // Paramètres de la loupe
                float2 magnifyCenter = _MagnifyCenter.xy;
                float magnifyRadius = _MagnifyRadius;
                float magnifyPower = _MagnifyPower;

                // Modification des coordonnées UV en fonction de l'effet de loupe
                float2 uv = i.uv;
                uv = MagnifyUV(uv, magnifyCenter, magnifyRadius, magnifyPower);

                // Échantillonnage de la texture modifiée
                fixed4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDCG
        }
    }
}
