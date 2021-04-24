// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LH/Fire"
{
	Properties
	{
		_FireMask("FireMask", 2D) = "white" {}
		_FireNoise("FireNoise", 2D) = "white" {}
		_OuterFlameBottomColor("OuterFlameBottomColor", Color) = (0.945098,0.1385157,0,0)
		_OuterFlameTopColor("OuterFlameTopColor", Color) = (1,0.542323,0,0)
		_OutlineColor("OutlineColor", Color) = (0.3207547,0.03758844,0,0)
		[HDR]_InnerFlameColor("InnerFlameColor", Color) = (0.8584906,0.6730857,0.1660288,0)
		_InnerFlameStep("InnerFlameStep", Range( 0 , 1)) = 0.3
		_ColorGradientOffset("ColorGradientOffset", Range( 0 , 1)) = 0
		_FlameStep("FlameStep", Range( 0.01 , 1)) = 0.52
		_MaskOpacity("MaskOpacity", Range( 0 , 1)) = 1
		_Opacity("Opacity", Range( 0 , 1)) = 1
		_Speed("Speed", Float) = 0.7
		_EmissionMultiplier("Emission Multiplier", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float4 _OuterFlameBottomColor;
		uniform float _ColorGradientOffset;
		uniform float4 _OuterFlameTopColor;
		uniform float _FlameStep;
		uniform sampler2D _FireMask;
		SamplerState sampler_FireMask;
		uniform float4 _FireMask_ST;
		uniform sampler2D _FireNoise;
		uniform float _Speed;
		uniform float _InnerFlameStep;
		uniform float4 _InnerFlameColor;
		uniform float4 _OutlineColor;
		uniform float _EmissionMultiplier;
		uniform float _Opacity;
		uniform float _MaskOpacity;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 break154 = i.vertexColor;
			float4 appendResult155 = (float4(break154.r , break154.g , break154.b , 0.0));
			float smoothstepResult119 = smoothstep( _ColorGradientOffset , 1.0 , i.uv_texcoord.y);
			float2 uv_FireMask = i.uv_texcoord * _FireMask_ST.xy + _FireMask_ST.zw;
			float4 tex2DNode88 = tex2D( _FireMask, uv_FireMask );
			float temp_output_178_0 = ( _Speed * -1.0 );
			float2 appendResult175 = (float2(0.0 , temp_output_178_0));
			float2 uv_TexCoord76 = i.uv_texcoord * float2( 1.5,1 );
			float2 panner75 = ( 1.0 * _Time.y * appendResult175 + uv_TexCoord76);
			float2 appendResult176 = (float2(0.0 , ( temp_output_178_0 * 0.7 )));
			float2 uv_TexCoord81 = i.uv_texcoord * float2( 0.8,0.5 );
			float2 panner79 = ( 1.0 * _Time.y * appendResult176 + uv_TexCoord81);
			float temp_output_90_0 = ( tex2DNode88.r * ( ( tex2D( _FireNoise, panner75 ).r * tex2D( _FireNoise, panner79 ).r ) + tex2DNode88.r ) );
			float temp_output_91_0 = step( _FlameStep , temp_output_90_0 );
			float temp_output_93_0 = step( (2.0 + (_InnerFlameStep - 0.0) * (0.5 - 2.0) / (1.0 - 0.0)) , temp_output_90_0 );
			float temp_output_130_0 = ( temp_output_91_0 - step( ( _FlameStep + 0.12 ) , temp_output_90_0 ) );
			float4 break148 = ( ( ( ( _OuterFlameBottomColor * ( 1.0 - smoothstepResult119 ) ) + ( smoothstepResult119 * _OuterFlameTopColor ) ) * ( ( temp_output_91_0 - temp_output_93_0 ) - temp_output_130_0 ) ) + ( temp_output_93_0 * _InnerFlameColor ) + ( temp_output_130_0 * _OutlineColor ) );
			float4 appendResult149 = (float4(break148.r , break148.g , break148.b , 0.0));
			o.Emission = ( ( appendResult155 * appendResult149 ) * _EmissionMultiplier ).xyz;
			o.Alpha = ( _Opacity * saturate( ( temp_output_90_0 + (1.0 + (_MaskOpacity - 0.0) * (0.0 - 1.0) / (1.0 - 0.0)) ) ) * break154.a * temp_output_91_0 );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				half4 color : COLOR0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.color = v.color;
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.vertexColor = IN.color;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18600
-1920;13;1307;1007;2735.958;202.8348;2.503652;True;False
Node;AmplifyShaderEditor.RangedFloatNode;172;-4324,1003;Inherit;False;Property;_Speed;Speed;11;0;Create;True;0;0;False;0;False;0.7;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;178;-4110.876,1159.939;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;144;-3634,462;Inherit;False;1949;1509;;13;83;82;90;88;78;74;137;75;81;79;76;175;176;Flame Pattern;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;173;-3872,1152;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.7;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;81;-3584,1280;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.8,0.5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;76;-3584,512;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1.5,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;176;-3584,1568;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;175;-3584,864;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;79;-3328,1408;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;137;-3328,1152;Inherit;True;Property;_FireNoise;FireNoise;1;0;Create;True;0;0;False;0;False;1cd398b18d465b9488bac74ba57fa84b;1cd398b18d465b9488bac74ba57fa84b;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.PannerNode;75;-3328,768;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;78;-2944,1408;Inherit;True;Property;_TextureSample2;Texture Sample 2;2;0;Create;True;0;0;False;0;False;-1;1cd398b18d465b9488bac74ba57fa84b;1cd398b18d465b9488bac74ba57fa84b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;74;-2944,768;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;False;-1;1cd398b18d465b9488bac74ba57fa84b;1cd398b18d465b9488bac74ba57fa84b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;88;-2560,1280;Inherit;True;Property;_FireMask;FireMask;0;0;Create;True;0;0;False;0;False;-1;3ea43d14329ffd540b13cd1209e621b1;3ea43d14329ffd540b13cd1209e621b1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;-2560,1024;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;143;-3394.628,-641.427;Inherit;False;1693;944;;9;125;119;120;127;126;123;97;141;118;Outer Flame Gradient Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;82;-2176,1024;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;92;-1311.147,514.4814;Inherit;False;Property;_FlameStep;FlameStep;8;0;Create;True;0;0;False;0;False;0.52;0.57;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;94;-1289.711,1291.509;Inherit;False;Property;_InnerFlameStep;InnerFlameStep;6;0;Create;True;0;0;False;0;False;0.3;0.67;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;118;-3344.628,-463.4269;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;120;-3344.628,-79.42696;Inherit;False;Property;_ColorGradientOffset;ColorGradientOffset;7;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;129;-768,512;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.12;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;-1920,1024;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;119;-2832.628,-207.427;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;181;-978.7043,1295.421;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;2;False;4;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;128;-512,896;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;141;-2832.628,48.57304;Inherit;False;Property;_OuterFlameTopColor;OuterFlameTopColor;3;0;Create;True;0;0;False;0;False;1,0.542323,0,0;0,0.8962264,0.187448,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;91;-802.8734,891.0181;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;93;-640,1152;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;123;-2576.628,-335.4269;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;97;-2576.628,-591.427;Inherit;False;Property;_OuterFlameBottomColor;OuterFlameBottomColor;2;0;Create;True;0;0;False;0;False;0.945098,0.1385157,0,0;0,1,0.4023905,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;125;-2320.627,48.57304;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;95;-256,896;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-2320.627,-207.427;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;130;-256,640;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;99;-256,1280;Inherit;False;Property;_InnerFlameColor;InnerFlameColor;5;1;[HDR];Create;True;0;0;False;0;False;0.8584906,0.6730857,0.1660288,0;0.5758495,2.141756,0.7551433,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;139;-256,384;Inherit;False;Property;_OutlineColor;OutlineColor;4;0;Create;True;0;0;False;0;False;0.3207547,0.03758844,0,0;0,0.2924528,0.1948032,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;127;-1936.627,48.57304;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;131;0,896;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;0,1152;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;384,512;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;132;0,512;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;153;931.5221,127.5171;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;101;640,512;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;150;343.1588,1304.853;Inherit;False;Property;_MaskOpacity;MaskOpacity;9;0;Create;True;0;0;False;0;False;1;0.01;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;154;1227.463,107.3859;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.TFHCRemapNode;166;705.5379,1302.728;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;148;906.7982,588.8123;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.DynamicAppendNode;155;1522.463,118.3859;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;165;1013.1,1210.27;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;149;1200.136,486.183;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;152;1695.771,346.3041;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;179;1706.321,583.8987;Inherit;False;Property;_EmissionMultiplier;Emission Multiplier;12;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;167;1280,1024;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;159;1280,768;Inherit;False;Property;_Opacity;Opacity;10;0;Create;True;0;0;False;0;False;1;0.266;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;180;1917.321,476.8987;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;156;1735.684,830.9174;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2163.825,621.3688;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;LH/Fire;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;178;0;172;0
WireConnection;173;0;178;0
WireConnection;176;1;173;0
WireConnection;175;1;178;0
WireConnection;79;0;81;0
WireConnection;79;2;176;0
WireConnection;75;0;76;0
WireConnection;75;2;175;0
WireConnection;78;0;137;0
WireConnection;78;1;79;0
WireConnection;74;0;137;0
WireConnection;74;1;75;0
WireConnection;83;0;74;1
WireConnection;83;1;78;1
WireConnection;82;0;83;0
WireConnection;82;1;88;1
WireConnection;129;0;92;0
WireConnection;90;0;88;1
WireConnection;90;1;82;0
WireConnection;119;0;118;2
WireConnection;119;1;120;0
WireConnection;181;0;94;0
WireConnection;128;0;129;0
WireConnection;128;1;90;0
WireConnection;91;0;92;0
WireConnection;91;1;90;0
WireConnection;93;0;181;0
WireConnection;93;1;90;0
WireConnection;123;0;119;0
WireConnection;125;0;119;0
WireConnection;125;1;141;0
WireConnection;95;0;91;0
WireConnection;95;1;93;0
WireConnection;126;0;97;0
WireConnection;126;1;123;0
WireConnection;130;0;91;0
WireConnection;130;1;128;0
WireConnection;127;0;126;0
WireConnection;127;1;125;0
WireConnection;131;0;95;0
WireConnection;131;1;130;0
WireConnection;100;0;93;0
WireConnection;100;1;99;0
WireConnection;98;0;127;0
WireConnection;98;1;131;0
WireConnection;132;0;130;0
WireConnection;132;1;139;0
WireConnection;101;0;98;0
WireConnection;101;1;100;0
WireConnection;101;2;132;0
WireConnection;154;0;153;0
WireConnection;166;0;150;0
WireConnection;148;0;101;0
WireConnection;155;0;154;0
WireConnection;155;1;154;1
WireConnection;155;2;154;2
WireConnection;165;0;90;0
WireConnection;165;1;166;0
WireConnection;149;0;148;0
WireConnection;149;1;148;1
WireConnection;149;2;148;2
WireConnection;152;0;155;0
WireConnection;152;1;149;0
WireConnection;167;0;165;0
WireConnection;180;0;152;0
WireConnection;180;1;179;0
WireConnection;156;0;159;0
WireConnection;156;1;167;0
WireConnection;156;2;154;3
WireConnection;156;3;91;0
WireConnection;0;2;180;0
WireConnection;0;9;156;0
ASEEND*/
//CHKSM=78E321558691965CDC797FC1C5EB1D9A7BE2E2AD