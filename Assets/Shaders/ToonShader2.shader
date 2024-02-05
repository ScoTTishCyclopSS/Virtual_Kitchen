// This shader fills the mesh shape with a color predefined in the code.
Shader "Example/URPUnlitShaderBasic"
{
    // The properties block of the Unity shader. In this example this block is empty
    // because the output color is predefined in the fragment shader code.
    Properties
    { 
        _Color("Color", Color) = (0.5, 0.65, 1, 1)
        _MainTex("Main Texture", 2D) = "white" {}	 
    }

    // The SubShader block containing the Shader code. 
    SubShader
    {
        // SubShader Tags define when and under which conditions a SubShader block or
        // a pass is executed.
        //Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
        Tags
		{
		    "RenderType" = "Opaque"
			"LightMode" = "ForwardBase"
			"PassFlags" = "OnlyDirectional"
		    "RenderPipeline" = "UniversalRenderPipeline"
		}
        Pass
        {
            // The HLSL code block. Unity SRP uses the HLSL language.
            HLSLPROGRAM
            // This line defines the name of the vertex shader. 
            #pragma vertex vert
            // This line defines the name of the fragment shader. 
            #pragma fragment frag

            // The Core.hlsl file contains definitions of frequently used HLSL
            // macros and functions, and also contains #include references to other
            // HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            //#include "UnityCG.cginc"

            // The structure definition defines which variables it contains.
            // This example uses the Attributes structure as an input structure in
            // the vertex shader.
            struct Attributes
            {
                // The positionOS variable contains the vertex positions in object
                // space.
                float4 positionOS   : POSITION;
                float3 normal : NORMAL;
                float4 uv : TEXCOORD0;
            };

            struct Varyings
            {
                // The positions in this struct must have the SV_POSITION semantic.
                float4 positionHCS  : SV_POSITION;
                float3 worldNormal : NORMAL;
                float2 uv : TEXCOORD0;
            };            

            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            // The vertex shader definition with properties defined in the Varyings 
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                // Declaring the output object (OUT) with the Varyings struct.
                Varyings OUT;
                // The TransformObjectToHClip function transforms vertex positions
                // from object space to homogenous space
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.worldNormal = TransformObjectToWorldNormal(IN.normal);
                //OUT.worldNormal = UnityObjectToWorldNormal(IN.normal);
                // Returning the output.
                return OUT;
            }

            // The fragment shader definition.            
            half4 frag(Varyings i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
				//float NdotL = dot(_WorldSpaceLightPos0, normal);
                float NdotL = dot(_MainLightPosition, normal);
                // Defining the color variable and returning it.
                // half4 customColor;
                // customColor = half4(0.5, 0, 0, 1);
                // return customColor;
				float4 sample = tex2D(_MainTex, i.uv);
				
				return _Color * sample * NdotL;
            }
            ENDHLSL
        }
    }
}


//Shader "Unlit/ToonShader2"
//{
//	Properties
//	{
//		_Color("Color", Color) = (0.5, 0.65, 1, 1)
//		_MainTex("Main Texture", 2D) = "white" {}	
//	}
//	SubShader
//	{
//		Pass
//		{
//			Tags
//			{
//				"LightMode" = "ForwardBase"
//				"PassFlags" = "OnlyDirectional"
//			}
//			CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag
//			
//			#include "UnityCG.cginc"
//
//			struct appdata
//			{
//				float4 vertex : POSITION;				
//				float4 uv : TEXCOORD0;
//				float3 normal : NORMAL;
//			};
//
//			struct v2f
//			{
//				float4 pos : SV_POSITION;
//				float2 uv : TEXCOORD0;
//				float3 worldNormal : NORMAL;
//			};
//
//			sampler2D _MainTex;
//			float4 _MainTex_ST;
//			
//			v2f vert (appdata v)
//			{
//				v2f o;
//				o.pos = UnityObjectToClipPos(v.vertex);
//				o.worldNormal = UnityObjectToWorldNormal(v.normal);
//				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//				return o;
//			}
//			
//			float4 _Color;
//
//			float4 frag (v2f i) : SV_Target
//			{
//				float3 normal = normalize(i.worldNormal);
//				float NdotL = dot(_WorldSpaceLightPos0, normal);
//				float4 sample = tex2D(_MainTex, i.uv);
//				
//				return _Color * sample * NdotL;
//			}
//			ENDCG
//		}
//	}
//}
