Shader "Custom/Focus Effect" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_FocusTex ("Base (RGB)", 2D) = "black" {}
	_FocusFactor ("Focus Factor", Range(0, 1) ) = 1
	_GreyscaleRamp ("Greyscale Ramp", Range(0, 1) ) = 0
}

SubShader {
	
	Pass {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
				
CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest 
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform sampler2D _FocusTex;
uniform float _FocusFactor;
uniform float _GreyscaleRamp;

fixed4 frag (v2f_img i) : SV_Target
{
	fixed4 original = tex2D(_MainTex, i.uv);
	fixed4 focusMask = tex2D(_FocusTex, i.uv);
	fixed greyscale = Luminance( original.rgb ) - _GreyscaleRamp;
	fixed4 output = fixed4( greyscale, greyscale, greyscale, 1);
	output.rgb = lerp( output.rgb, original.rgb, max( Luminance( focusMask.rgb ).r, _FocusFactor ) );
	output.a = original.a;
	return output;
}
ENDCG

	}
}

Fallback off

}