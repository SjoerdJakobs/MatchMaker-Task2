Shader "UnityChan/ground - Transparent"
{
	Properties
	{
		_Color ("Main Color", Color) = (1, 1, 1, 1)
		_ShadowColor ("Shadow Color", Color) = (0.8, 0.8, 1, 1)
	
		_MainTex ("Diffuse", 2D) = "white" {}
		_FalloffSampler ("Falloff Control", 2D) = "white" {}
		_RimLightSampler ("RimLight Control", 2D) = "white" {}
		_EnvMapSampler ("Environment Map", 2D) = "" {} 
		_BumpMap ("Normal texture", 2D) = "bump" {} 

	}

	SubShader
	{
	  
		Blend SrcAlpha OneMinusSrcAlpha, One One 
		Tags
		{



			"RenderType"="Opaque"
			"Queue"="Geometry"
			"LightMode"="ForwardBase"


			"Queue"="Geometry+2"
			// "IgnoreProjector"="True"
			"RenderType"="Overlay"
			"LightMode"="ForwardBase"


		}

	
		Pass
		{
			Cull Back
			ZTest LEqual
CGPROGRAM
#pragma multi_compile_fwdbase
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "AutoLight.cginc"
#include "CharaSkin.cg"

ENDCG
		}
	}

	FallBack "Transparent/Cutout/Diffuse"
}
