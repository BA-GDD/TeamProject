// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HW/Alphablend_Sword_Water"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin]_Sword_Texture("Sword_Texture", 2D) = "white" {}
		_Sword_UOffset("Sword_UOffset", Range( -1 , 1)) = 0
		_Sword_VOffset("Sword_VOffset", Float) = 0
		_Edge_Width("Edge_Width", Float) = 0.29
		[HDR]_Emi_Color("Emi_Color", Color) = (1,1,1,0)
		_Emi_Offset("Emi_Offset", Range( -1 , 1)) = -0.3225207
		_Eni_Inten("Eni_Inten", Range( 1 , 10)) = 1
		_Main_Texture("Main_Texture", 2D) = "white" {}
		[HDR]_Main_Color("Main_Color", Color) = (1,1,1,0)
		_Main_intencity("Main_intencity", Range( 1 , 10)) = 1
		_Main_power("Main_power", Float) = 1.34
		_Main_UPanner("Main_UPanner", Float) = 0.2
		_Main_VPanner("Main_VPanner", Float) = 0
		_Masked_Power("Masked_Power", Float) = 3.55
		_Noise_Texture("Noise_Texture", 2D) = "bump" {}
		_Noise_UPanner("Noise_UPanner", Float) = 0
		_Noise_VPanner("Noise_VPanner", Float) = 0
		_Noise_Strength("Noise_Strength", Range( 0 , 0.5)) = 0.2705882
		_Dissolve_Texture("Dissolve_Texture", 2D) = "white" {}
		_DissTex_UPanner("DissTex_UPanner", Float) = 0
		_DissTex_VPanner("DissTex_VPanner", Float) = 0
		[Toggle(USE_CUSTOM_ON)] Use_Custom("Use_Custom", Float) = 0
		_Dissolve_Value("Dissolve_Value", Range( -1 , 1)) = -1
		_Ceil_Count("Ceil_Count", Range( 1 , 20)) = 3.043608
		_Dissolve_Step("Dissolve_Step", Float) = 0.47
		_Head_power("Head_power", Range( 1 , 10)) = 1
		_Head_Ins("Head_Ins", Float) = 0
		[Toggle(_USE_EDGED_ON)] _Use_Edged("Use_Edged", Float) = 0
		_threshold("threshold", Range( 0 , 1)) = 0.5
		[Enum(UnityEngine.Rendering.CullMode)]_CULL("CULL", Float) = 0
		[Toggle(_USE_MASKTEX_ON)] _Use_MaskTex("Use_MaskTex", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[ASEEnd]_mask_pow("mask_pow", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}


		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25

		[HideInInspector] _QueueOffset("_QueueOffset", Float) = 0
        [HideInInspector] _QueueControl("_QueueControl", Float) = -1

        [HideInInspector][NoScaleOffset] unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset] unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset] unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" "UniversalMaterialType"="Unlit" }

		Cull [_CULL]
		AlphaToMask Off

		

		HLSLINCLUDE
		#pragma target 3.5
		#pragma prefer_hlslcc gles
		// ensure rendering platforms toggle list is visible

		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Filtering.hlsl"

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}

		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS
		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForwardOnly" }

			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA

			

			HLSLPROGRAM

			#pragma multi_compile_instancing
			#define _RECEIVE_SHADOWS_OFF 1
			#define _SURFACE_TYPE_TRANSPARENT 1
			#define _ALPHATEST_ON 1
			#define ASE_SRP_VERSION 140008


			#pragma instancing_options renderinglayer

			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma shader_feature _ _SAMPLE_GI
			#pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        	#pragma multi_compile_fragment _ DEBUG_DISPLAY
        	#pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
        	#pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS

			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_UNLIT

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Debug/Debugging3D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceData.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"

			#pragma shader_feature_local USE_CUSTOM_ON
			#pragma shader_feature_local _USE_EDGED_ON
			#pragma shader_feature_local _USE_MASKTEX_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD1;
				#endif
				#ifdef ASE_FOG
					float fogFactor : TEXCOORD2;
				#endif
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Main_Color;
			float4 _Dissolve_Texture_ST;
			float4 _Noise_Texture_ST;
			float4 _Main_Texture_ST;
			float4 _TextureSample0_ST;
			float4 _Emi_Color;
			float4 _Sword_Texture_ST;
			float _DissTex_VPanner;
			float _DissTex_UPanner;
			float _Dissolve_Step;
			float _mask_pow;
			float _Masked_Power;
			float _Edge_Width;
			float _Sword_VOffset;
			float _Sword_UOffset;
			float _Main_intencity;
			float _CULL;
			float _Dissolve_Value;
			float _Eni_Inten;
			float _Ceil_Count;
			float _Head_Ins;
			float _Head_power;
			float _Noise_Strength;
			float _Noise_VPanner;
			float _Noise_UPanner;
			float _Main_VPanner;
			float _Main_UPanner;
			float _Emi_Offset;
			float _Main_power;
			float _threshold;
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			sampler2D _Main_Texture;
			sampler2D _Noise_Texture;
			sampler2D _Sword_Texture;
			sampler2D _TextureSample0;
			sampler2D _Dissolve_Texture;


			
			VertexOutput VertexFunction ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord3.xy = v.ase_texcoord.xy;
				o.ase_texcoord4 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.zw = 0;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					o.worldPos = positionWS;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				#ifdef ASE_FOG
					o.fogFactor = ComputeFogFactor( positionCS.z );
				#endif

				o.clipPos = positionCS;

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag ( VertexOutput IN
				#ifdef _WRITE_RENDERING_LAYERS
				, out float4 outRenderingLayers : SV_Target1
				#endif
				 ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 WorldPosition = IN.worldPos;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 texCoord55 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef USE_CUSTOM_ON
				float staticSwitch73 = IN.ase_texcoord4.z;
				#else
				float staticSwitch73 = _Emi_Offset;
				#endif
				float temp_output_59_0 = saturate( ( saturate( ( 1.0 - texCoord55.x ) ) + staticSwitch73 ) );
				float2 appendResult15 = (float2(_Main_UPanner , _Main_VPanner));
				float2 uv_Noise_Texture = IN.ase_texcoord3.xy * _Noise_Texture_ST.xy + _Noise_Texture_ST.zw;
				float2 appendResult39 = (float2(_Noise_UPanner , _Noise_VPanner));
				float2 uv_Main_Texture = IN.ase_texcoord3.xy * _Main_Texture_ST.xy + _Main_Texture_ST.zw;
				float2 panner40 = ( 1.0 * _Time.y * appendResult39 + uv_Main_Texture);
				float2 panner12 = ( 1.0 * _Time.y * appendResult15 + ( uv_Noise_Texture + ( (UnpackNormalScale( tex2D( _Noise_Texture, panner40 ), 1.0f )).xy * _Noise_Strength ) ));
				float2 texCoord99 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_94_0 = ( ceil( ( ( tex2D( _Main_Texture, panner12 ).r + ( pow( texCoord99.y , _Head_power ) * _Head_Ins ) ) * _Ceil_Count ) ) / _Ceil_Count );
				#ifdef USE_CUSTOM_ON
				float staticSwitch76 = IN.ase_texcoord4.w;
				#else
				float staticSwitch76 = _Eni_Inten;
				#endif
				
				float2 uv_Sword_Texture = IN.ase_texcoord3.xy * _Sword_Texture_ST.xy + _Sword_Texture_ST.zw;
				#ifdef USE_CUSTOM_ON
				float staticSwitch74 = IN.ase_texcoord4.x;
				#else
				float staticSwitch74 = _Sword_UOffset;
				#endif
				float2 appendResult5 = (float2(staticSwitch74 , _Sword_VOffset));
				float4 tex2DNode1 = tex2D( _Sword_Texture, (uv_Sword_Texture*1.0 + appendResult5) );
				float2 texCoord105 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _USE_EDGED_ON
				float staticSwitch110 = ( tex2DNode1.r * saturate( step( ( 1.0 - texCoord105.y ) , _Edge_Width ) ) );
				#else
				float staticSwitch110 = tex2DNode1.r;
				#endif
				float2 texCoord16 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_TextureSample0 = IN.ase_texcoord3.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				#ifdef _USE_MASKTEX_ON
				float staticSwitch131 = saturate( pow( saturate( tex2D( _TextureSample0, uv_TextureSample0 ).r ) , _mask_pow ) );
				#else
				float staticSwitch131 = saturate( pow( saturate( ( ( texCoord16.x * saturate( ( 1.0 - texCoord16.x ) ) ) * 4.0 ) ) , _Masked_Power ) );
				#endif
				float2 appendResult83 = (float2(_DissTex_UPanner , _DissTex_VPanner));
				float2 uv_Dissolve_Texture = IN.ase_texcoord3.xy * _Dissolve_Texture_ST.xy + _Dissolve_Texture_ST.zw;
				float2 panner80 = ( 1.0 * _Time.y * appendResult83 + uv_Dissolve_Texture);
				#ifdef USE_CUSTOM_ON
				float staticSwitch75 = IN.ase_texcoord4.y;
				#else
				float staticSwitch75 = _Dissolve_Value;
				#endif
				
				float3 BakedAlbedo = 0;
				float3 BakedEmission = 0;
				float3 Color = ( ( saturate( ( saturate( pow( ( temp_output_59_0 * ( temp_output_59_0 + temp_output_94_0 ) ) , 4.0 ) ) * staticSwitch76 ) ) * _Emi_Color ) + ( ( _Main_Color * ( pow( temp_output_94_0 , _Main_power ) * _Main_intencity ) ) * IN.ase_color ) ).rgb;
				float Alpha = saturate( ( saturate( ( staticSwitch110 * saturate( staticSwitch131 ) ) ) * saturate( step( _Dissolve_Step , ( tex2D( _Dissolve_Texture, panner80 ).r + staticSwitch75 ) ) ) ) );
				float AlphaClipThreshold = _threshold;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef _ALPHATEST_ON
					clip( Alpha - AlphaClipThreshold );
				#endif

				#if defined(_DBUFFER)
					ApplyDecalToBaseColor(IN.clipPos, Color);
				#endif

				#if defined(_ALPHAPREMULTIPLY_ON)
				Color *= Alpha;
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODFadeCrossFade( IN.clipPos );
				#endif

				#ifdef ASE_FOG
					Color = MixFog( Color, IN.fogFactor );
				#endif

				#ifdef _WRITE_RENDERING_LAYERS
					uint renderingLayers = GetMeshRenderingLayer();
					outRenderingLayers = float4( EncodeMeshRenderingLayer( renderingLayers ), 0, 0, 0 );
				#endif

				return half4( Color, Alpha );
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthOnly"
			Tags { "LightMode"="DepthOnly" }

			ZWrite On
			ColorMask 0
			AlphaToMask Off

			HLSLPROGRAM

			#pragma multi_compile_instancing
			#define _RECEIVE_SHADOWS_OFF 1
			#define _SURFACE_TYPE_TRANSPARENT 1
			#define _ALPHATEST_ON 1
			#define ASE_SRP_VERSION 140008


			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"

			#pragma shader_feature_local _USE_EDGED_ON
			#pragma shader_feature_local USE_CUSTOM_ON
			#pragma shader_feature_local _USE_MASKTEX_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Main_Color;
			float4 _Dissolve_Texture_ST;
			float4 _Noise_Texture_ST;
			float4 _Main_Texture_ST;
			float4 _TextureSample0_ST;
			float4 _Emi_Color;
			float4 _Sword_Texture_ST;
			float _DissTex_VPanner;
			float _DissTex_UPanner;
			float _Dissolve_Step;
			float _mask_pow;
			float _Masked_Power;
			float _Edge_Width;
			float _Sword_VOffset;
			float _Sword_UOffset;
			float _Main_intencity;
			float _CULL;
			float _Dissolve_Value;
			float _Eni_Inten;
			float _Ceil_Count;
			float _Head_Ins;
			float _Head_power;
			float _Noise_Strength;
			float _Noise_VPanner;
			float _Noise_UPanner;
			float _Main_VPanner;
			float _Main_UPanner;
			float _Emi_Offset;
			float _Main_power;
			float _threshold;
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			sampler2D _Sword_Texture;
			sampler2D _TextureSample0;
			sampler2D _Dissolve_Texture;


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_texcoord3 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					o.worldPos = positionWS;
				#endif

				o.clipPos = TransformWorldToHClip( positionWS );
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = o.clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 WorldPosition = IN.worldPos;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 uv_Sword_Texture = IN.ase_texcoord2.xy * _Sword_Texture_ST.xy + _Sword_Texture_ST.zw;
				#ifdef USE_CUSTOM_ON
				float staticSwitch74 = IN.ase_texcoord3.x;
				#else
				float staticSwitch74 = _Sword_UOffset;
				#endif
				float2 appendResult5 = (float2(staticSwitch74 , _Sword_VOffset));
				float4 tex2DNode1 = tex2D( _Sword_Texture, (uv_Sword_Texture*1.0 + appendResult5) );
				float2 texCoord105 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _USE_EDGED_ON
				float staticSwitch110 = ( tex2DNode1.r * saturate( step( ( 1.0 - texCoord105.y ) , _Edge_Width ) ) );
				#else
				float staticSwitch110 = tex2DNode1.r;
				#endif
				float2 texCoord16 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_TextureSample0 = IN.ase_texcoord2.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				#ifdef _USE_MASKTEX_ON
				float staticSwitch131 = saturate( pow( saturate( tex2D( _TextureSample0, uv_TextureSample0 ).r ) , _mask_pow ) );
				#else
				float staticSwitch131 = saturate( pow( saturate( ( ( texCoord16.x * saturate( ( 1.0 - texCoord16.x ) ) ) * 4.0 ) ) , _Masked_Power ) );
				#endif
				float2 appendResult83 = (float2(_DissTex_UPanner , _DissTex_VPanner));
				float2 uv_Dissolve_Texture = IN.ase_texcoord2.xy * _Dissolve_Texture_ST.xy + _Dissolve_Texture_ST.zw;
				float2 panner80 = ( 1.0 * _Time.y * appendResult83 + uv_Dissolve_Texture);
				#ifdef USE_CUSTOM_ON
				float staticSwitch75 = IN.ase_texcoord3.y;
				#else
				float staticSwitch75 = _Dissolve_Value;
				#endif
				

				float Alpha = saturate( ( saturate( ( staticSwitch110 * saturate( staticSwitch131 ) ) ) * saturate( step( _Dissolve_Step , ( tex2D( _Dissolve_Texture, panner80 ).r + staticSwitch75 ) ) ) ) );
				float AlphaClipThreshold = _threshold;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODFadeCrossFade( IN.clipPos );
				#endif
				return 0;
			}
			ENDHLSL
		}

		
		Pass
		{
			
            Name "SceneSelectionPass"
            Tags { "LightMode"="SceneSelectionPass" }

			Cull Off

			HLSLPROGRAM

			#pragma multi_compile_instancing
			#define _RECEIVE_SHADOWS_OFF 1
			#define _SURFACE_TYPE_TRANSPARENT 1
			#define _ALPHATEST_ON 1
			#define ASE_SRP_VERSION 140008


			#pragma vertex vert
			#pragma fragment frag

			#define ATTRIBUTES_NEED_NORMAL
			#define ATTRIBUTES_NEED_TANGENT
			#define SHADERPASS SHADERPASS_DEPTHONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#pragma shader_feature_local _USE_EDGED_ON
			#pragma shader_feature_local USE_CUSTOM_ON
			#pragma shader_feature_local _USE_MASKTEX_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Main_Color;
			float4 _Dissolve_Texture_ST;
			float4 _Noise_Texture_ST;
			float4 _Main_Texture_ST;
			float4 _TextureSample0_ST;
			float4 _Emi_Color;
			float4 _Sword_Texture_ST;
			float _DissTex_VPanner;
			float _DissTex_UPanner;
			float _Dissolve_Step;
			float _mask_pow;
			float _Masked_Power;
			float _Edge_Width;
			float _Sword_VOffset;
			float _Sword_UOffset;
			float _Main_intencity;
			float _CULL;
			float _Dissolve_Value;
			float _Eni_Inten;
			float _Ceil_Count;
			float _Head_Ins;
			float _Head_power;
			float _Noise_Strength;
			float _Noise_VPanner;
			float _Noise_UPanner;
			float _Main_VPanner;
			float _Main_UPanner;
			float _Emi_Offset;
			float _Main_power;
			float _threshold;
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			sampler2D _Sword_Texture;
			sampler2D _TextureSample0;
			sampler2D _Dissolve_Texture;


			
			int _ObjectId;
			int _PassValue;

			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			VertexOutput VertexFunction(VertexInput v  )
			{
				VertexOutput o;
				ZERO_INITIALIZE(VertexOutput, o);

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				o.ase_texcoord1 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				o.clipPos = TransformWorldToHClip(positionWS);

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN ) : SV_TARGET
			{
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;

				float2 uv_Sword_Texture = IN.ase_texcoord.xy * _Sword_Texture_ST.xy + _Sword_Texture_ST.zw;
				#ifdef USE_CUSTOM_ON
				float staticSwitch74 = IN.ase_texcoord1.x;
				#else
				float staticSwitch74 = _Sword_UOffset;
				#endif
				float2 appendResult5 = (float2(staticSwitch74 , _Sword_VOffset));
				float4 tex2DNode1 = tex2D( _Sword_Texture, (uv_Sword_Texture*1.0 + appendResult5) );
				float2 texCoord105 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _USE_EDGED_ON
				float staticSwitch110 = ( tex2DNode1.r * saturate( step( ( 1.0 - texCoord105.y ) , _Edge_Width ) ) );
				#else
				float staticSwitch110 = tex2DNode1.r;
				#endif
				float2 texCoord16 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_TextureSample0 = IN.ase_texcoord.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				#ifdef _USE_MASKTEX_ON
				float staticSwitch131 = saturate( pow( saturate( tex2D( _TextureSample0, uv_TextureSample0 ).r ) , _mask_pow ) );
				#else
				float staticSwitch131 = saturate( pow( saturate( ( ( texCoord16.x * saturate( ( 1.0 - texCoord16.x ) ) ) * 4.0 ) ) , _Masked_Power ) );
				#endif
				float2 appendResult83 = (float2(_DissTex_UPanner , _DissTex_VPanner));
				float2 uv_Dissolve_Texture = IN.ase_texcoord.xy * _Dissolve_Texture_ST.xy + _Dissolve_Texture_ST.zw;
				float2 panner80 = ( 1.0 * _Time.y * appendResult83 + uv_Dissolve_Texture);
				#ifdef USE_CUSTOM_ON
				float staticSwitch75 = IN.ase_texcoord1.y;
				#else
				float staticSwitch75 = _Dissolve_Value;
				#endif
				

				surfaceDescription.Alpha = saturate( ( saturate( ( staticSwitch110 * saturate( staticSwitch131 ) ) ) * saturate( step( _Dissolve_Step , ( tex2D( _Dissolve_Texture, panner80 ).r + staticSwitch75 ) ) ) ) );
				surfaceDescription.AlphaClipThreshold = _threshold;

				#if _ALPHATEST_ON
					float alphaClipThreshold = 0.01f;
					#if ALPHA_CLIP_THRESHOLD
						alphaClipThreshold = surfaceDescription.AlphaClipThreshold;
					#endif
					clip(surfaceDescription.Alpha - alphaClipThreshold);
				#endif

				half4 outColor = half4(_ObjectId, _PassValue, 1.0, 1.0);
				return outColor;
			}
			ENDHLSL
		}

		
		Pass
		{
			
            Name "ScenePickingPass"
            Tags { "LightMode"="Picking" }

			HLSLPROGRAM

			#pragma multi_compile_instancing
			#define _RECEIVE_SHADOWS_OFF 1
			#define _SURFACE_TYPE_TRANSPARENT 1
			#define _ALPHATEST_ON 1
			#define ASE_SRP_VERSION 140008


			#pragma vertex vert
			#pragma fragment frag

			#define ATTRIBUTES_NEED_NORMAL
			#define ATTRIBUTES_NEED_TANGENT
			#define SHADERPASS SHADERPASS_DEPTHONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#pragma shader_feature_local _USE_EDGED_ON
			#pragma shader_feature_local USE_CUSTOM_ON
			#pragma shader_feature_local _USE_MASKTEX_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Main_Color;
			float4 _Dissolve_Texture_ST;
			float4 _Noise_Texture_ST;
			float4 _Main_Texture_ST;
			float4 _TextureSample0_ST;
			float4 _Emi_Color;
			float4 _Sword_Texture_ST;
			float _DissTex_VPanner;
			float _DissTex_UPanner;
			float _Dissolve_Step;
			float _mask_pow;
			float _Masked_Power;
			float _Edge_Width;
			float _Sword_VOffset;
			float _Sword_UOffset;
			float _Main_intencity;
			float _CULL;
			float _Dissolve_Value;
			float _Eni_Inten;
			float _Ceil_Count;
			float _Head_Ins;
			float _Head_power;
			float _Noise_Strength;
			float _Noise_VPanner;
			float _Noise_UPanner;
			float _Main_VPanner;
			float _Main_UPanner;
			float _Emi_Offset;
			float _Main_power;
			float _threshold;
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			sampler2D _Sword_Texture;
			sampler2D _TextureSample0;
			sampler2D _Dissolve_Texture;


			
			float4 _SelectionID;


			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			VertexOutput VertexFunction(VertexInput v  )
			{
				VertexOutput o;
				ZERO_INITIALIZE(VertexOutput, o);

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				o.ase_texcoord1 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				o.clipPos = TransformWorldToHClip(positionWS);
				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN ) : SV_TARGET
			{
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;

				float2 uv_Sword_Texture = IN.ase_texcoord.xy * _Sword_Texture_ST.xy + _Sword_Texture_ST.zw;
				#ifdef USE_CUSTOM_ON
				float staticSwitch74 = IN.ase_texcoord1.x;
				#else
				float staticSwitch74 = _Sword_UOffset;
				#endif
				float2 appendResult5 = (float2(staticSwitch74 , _Sword_VOffset));
				float4 tex2DNode1 = tex2D( _Sword_Texture, (uv_Sword_Texture*1.0 + appendResult5) );
				float2 texCoord105 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _USE_EDGED_ON
				float staticSwitch110 = ( tex2DNode1.r * saturate( step( ( 1.0 - texCoord105.y ) , _Edge_Width ) ) );
				#else
				float staticSwitch110 = tex2DNode1.r;
				#endif
				float2 texCoord16 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_TextureSample0 = IN.ase_texcoord.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				#ifdef _USE_MASKTEX_ON
				float staticSwitch131 = saturate( pow( saturate( tex2D( _TextureSample0, uv_TextureSample0 ).r ) , _mask_pow ) );
				#else
				float staticSwitch131 = saturate( pow( saturate( ( ( texCoord16.x * saturate( ( 1.0 - texCoord16.x ) ) ) * 4.0 ) ) , _Masked_Power ) );
				#endif
				float2 appendResult83 = (float2(_DissTex_UPanner , _DissTex_VPanner));
				float2 uv_Dissolve_Texture = IN.ase_texcoord.xy * _Dissolve_Texture_ST.xy + _Dissolve_Texture_ST.zw;
				float2 panner80 = ( 1.0 * _Time.y * appendResult83 + uv_Dissolve_Texture);
				#ifdef USE_CUSTOM_ON
				float staticSwitch75 = IN.ase_texcoord1.y;
				#else
				float staticSwitch75 = _Dissolve_Value;
				#endif
				

				surfaceDescription.Alpha = saturate( ( saturate( ( staticSwitch110 * saturate( staticSwitch131 ) ) ) * saturate( step( _Dissolve_Step , ( tex2D( _Dissolve_Texture, panner80 ).r + staticSwitch75 ) ) ) ) );
				surfaceDescription.AlphaClipThreshold = _threshold;

				#if _ALPHATEST_ON
					float alphaClipThreshold = 0.01f;
					#if ALPHA_CLIP_THRESHOLD
						alphaClipThreshold = surfaceDescription.AlphaClipThreshold;
					#endif
					clip(surfaceDescription.Alpha - alphaClipThreshold);
				#endif

				half4 outColor = 0;
				outColor = _SelectionID;

				return outColor;
			}

			ENDHLSL
		}

		
		Pass
		{
			
            Name "DepthNormals"
            Tags { "LightMode"="DepthNormalsOnly" }

			ZTest LEqual
			ZWrite On


			HLSLPROGRAM

			#pragma multi_compile_instancing
			#define _RECEIVE_SHADOWS_OFF 1
			#define _SURFACE_TYPE_TRANSPARENT 1
			#define _ALPHATEST_ON 1
			#define ASE_SRP_VERSION 140008


			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS
        	#pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT

			#define ATTRIBUTES_NEED_NORMAL
			#define ATTRIBUTES_NEED_TANGENT
			#define VARYINGS_NEED_NORMAL_WS

			#define SHADERPASS SHADERPASS_DEPTHNORMALSONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"

			#pragma shader_feature_local _USE_EDGED_ON
			#pragma shader_feature_local USE_CUSTOM_ON
			#pragma shader_feature_local _USE_MASKTEX_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float3 normalWS : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Main_Color;
			float4 _Dissolve_Texture_ST;
			float4 _Noise_Texture_ST;
			float4 _Main_Texture_ST;
			float4 _TextureSample0_ST;
			float4 _Emi_Color;
			float4 _Sword_Texture_ST;
			float _DissTex_VPanner;
			float _DissTex_UPanner;
			float _Dissolve_Step;
			float _mask_pow;
			float _Masked_Power;
			float _Edge_Width;
			float _Sword_VOffset;
			float _Sword_UOffset;
			float _Main_intencity;
			float _CULL;
			float _Dissolve_Value;
			float _Eni_Inten;
			float _Ceil_Count;
			float _Head_Ins;
			float _Head_power;
			float _Noise_Strength;
			float _Noise_VPanner;
			float _Noise_UPanner;
			float _Main_VPanner;
			float _Main_UPanner;
			float _Emi_Offset;
			float _Main_power;
			float _threshold;
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			sampler2D _Sword_Texture;
			sampler2D _TextureSample0;
			sampler2D _Dissolve_Texture;


			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			VertexOutput VertexFunction(VertexInput v  )
			{
				VertexOutput o;
				ZERO_INITIALIZE(VertexOutput, o);

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				o.ase_texcoord2 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float3 normalWS = TransformObjectToWorldNormal(v.ase_normal);

				o.clipPos = TransformWorldToHClip(positionWS);
				o.normalWS.xyz =  normalWS;

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			void frag( VertexOutput IN
				, out half4 outNormalWS : SV_Target0
			#ifdef _WRITE_RENDERING_LAYERS
				, out float4 outRenderingLayers : SV_Target1
			#endif
				 )
			{
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;

				float2 uv_Sword_Texture = IN.ase_texcoord1.xy * _Sword_Texture_ST.xy + _Sword_Texture_ST.zw;
				#ifdef USE_CUSTOM_ON
				float staticSwitch74 = IN.ase_texcoord2.x;
				#else
				float staticSwitch74 = _Sword_UOffset;
				#endif
				float2 appendResult5 = (float2(staticSwitch74 , _Sword_VOffset));
				float4 tex2DNode1 = tex2D( _Sword_Texture, (uv_Sword_Texture*1.0 + appendResult5) );
				float2 texCoord105 = IN.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _USE_EDGED_ON
				float staticSwitch110 = ( tex2DNode1.r * saturate( step( ( 1.0 - texCoord105.y ) , _Edge_Width ) ) );
				#else
				float staticSwitch110 = tex2DNode1.r;
				#endif
				float2 texCoord16 = IN.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_TextureSample0 = IN.ase_texcoord1.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				#ifdef _USE_MASKTEX_ON
				float staticSwitch131 = saturate( pow( saturate( tex2D( _TextureSample0, uv_TextureSample0 ).r ) , _mask_pow ) );
				#else
				float staticSwitch131 = saturate( pow( saturate( ( ( texCoord16.x * saturate( ( 1.0 - texCoord16.x ) ) ) * 4.0 ) ) , _Masked_Power ) );
				#endif
				float2 appendResult83 = (float2(_DissTex_UPanner , _DissTex_VPanner));
				float2 uv_Dissolve_Texture = IN.ase_texcoord1.xy * _Dissolve_Texture_ST.xy + _Dissolve_Texture_ST.zw;
				float2 panner80 = ( 1.0 * _Time.y * appendResult83 + uv_Dissolve_Texture);
				#ifdef USE_CUSTOM_ON
				float staticSwitch75 = IN.ase_texcoord2.y;
				#else
				float staticSwitch75 = _Dissolve_Value;
				#endif
				

				surfaceDescription.Alpha = saturate( ( saturate( ( staticSwitch110 * saturate( staticSwitch131 ) ) ) * saturate( step( _Dissolve_Step , ( tex2D( _Dissolve_Texture, panner80 ).r + staticSwitch75 ) ) ) ) );
				surfaceDescription.AlphaClipThreshold = _threshold;

				#if _ALPHATEST_ON
					clip(surfaceDescription.Alpha - surfaceDescription.AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODFadeCrossFade( IN.clipPos );
				#endif

				#if defined(_GBUFFER_NORMALS_OCT)
					float3 normalWS = normalize(IN.normalWS);
					float2 octNormalWS = PackNormalOctQuadEncode(normalWS);           // values between [-1, +1], must use fp32 on some platforms
					float2 remappedOctNormalWS = saturate(octNormalWS * 0.5 + 0.5);   // values between [ 0,  1]
					half3 packedNormalWS = PackFloat2To888(remappedOctNormalWS);      // values between [ 0,  1]
					outNormalWS = half4(packedNormalWS, 0.0);
				#else
					float3 normalWS = IN.normalWS;
					outNormalWS = half4(NormalizeNormalPerPixel(normalWS), 0.0);
				#endif

				#ifdef _WRITE_RENDERING_LAYERS
					uint renderingLayers = GetMeshRenderingLayer();
					outRenderingLayers = float4(EncodeMeshRenderingLayer(renderingLayers), 0, 0, 0);
				#endif
			}

			ENDHLSL
		}

	
	}
	
	CustomEditor "UnityEditor.ShaderGraphUnlitGUI"
	FallBack "Hidden/Shader Graph/FallbackError"
	
	Fallback Off
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.CommentaryNode;42;-3047.188,-431.2224;Inherit;False;1048.894;553.5909;Noise_Tex;9;36;40;39;37;38;41;44;45;46;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-3002.85,15.36858;Inherit;False;Property;_Noise_VPanner;Noise_VPanner;16;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-3002.049,-82.83142;Inherit;False;Property;_Noise_UPanner;Noise_UPanner;15;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;41;-3025.333,-375.2133;Inherit;True;0;9;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;39;-2826.851,-42.53138;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;40;-2736.484,-373.2645;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;36;-2597.775,-112.2848;Inherit;True;Property;_Noise_Texture;Noise_Texture;14;0;Create;True;0;0;0;False;0;False;-1;da5feafade73aad4eb052b907e1945ab;04018d4e5ecda6049b39579f207e2114;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;44;-2454.783,-388.1636;Inherit;True;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-2506.579,-194.9255;Inherit;False;Property;_Noise_Strength;Noise_Strength;18;0;Create;True;0;0;0;False;0;False;0.2705882;0;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;53;-1874.164,-270.0079;Inherit;False;2020.11;537.1064;Comment;20;9;12;15;47;13;48;14;93;50;69;10;51;2;49;94;95;96;99;100;104;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1818.512,43.87396;Inherit;False;Property;_Main_UPanner;Main_UPanner;11;0;Create;True;0;0;0;False;0;False;0.2;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;48;-1898.512,-228.1261;Inherit;False;0;36;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-1818.512,139.8739;Inherit;False;Property;_Main_VPanner;Main_VPanner;12;0;Create;True;0;0;0;False;0;False;0;0.97;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-2137.986,-11.10821;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-1658.512,-132.1261;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;15;-1642.512,59.87395;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;99;-1421.474,135.3022;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;101;-1458.674,248.3919;Inherit;False;Property;_Head_power;Head_power;25;0;Create;True;0;0;0;False;0;False;1;1;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;12;-1498.512,-36.12604;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;100;-1174.462,139.7663;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;103;-1124.777,238.2237;Inherit;False;Property;_Head_Ins;Head_Ins;26;0;Create;True;0;0;0;False;0;False;0;2.52;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;66;-1585.435,-796.7756;Inherit;False;1787.501;522.9459;Comment;17;55;56;57;58;59;60;61;63;62;65;68;90;91;73;76;124;127;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;9;-1303.804,-71.22139;Inherit;True;Property;_Main_Texture;Main_Texture;7;0;Create;True;0;0;0;False;0;False;-1;145143eb93006454baed654b5669d0c3;145143eb93006454baed654b5669d0c3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-991.4352,142.7423;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-1543.403,-526.2563;Inherit;False;Property;_Emi_Offset;Emi_Offset;5;0;Create;True;0;0;0;False;0;False;-0.3225207;-0.24;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;77;-2831.643,418.4769;Inherit;False;1;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;55;-1535.435,-746.7756;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;104;-812.7773,173.2237;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;23;-2134.12,947.8711;Inherit;False;1435.601;527.8115;Comment;12;16;18;17;19;20;21;22;111;129;132;134;137;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;54;-1807.41,346.9363;Inherit;False;1696.428;566.1379;Comment;17;109;24;108;107;106;105;1;3;88;52;4;5;74;7;110;125;126;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-2017.344,1012.71;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;87;-1165.808,1494.383;Inherit;False;1823;516.1472;Dissolve;12;86;84;75;78;85;80;83;79;81;82;97;98;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;-963.2912,-71.45915;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-2052.019,601.5586;Inherit;False;Property;_Sword_UOffset;Sword_UOffset;1;0;Create;True;0;0;0;False;0;False;0;0.39;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;56;-1131.403,-737.2563;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;-1115.808,1760.53;Inherit;False;Property;_DissTex_UPanner;DissTex_UPanner;20;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1695.797,789.1422;Inherit;False;Property;_Sword_VOffset;Sword_VOffset;2;0;Create;True;0;0;0;False;0;False;0;-0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;82;-1114.808,1833.53;Inherit;False;Property;_DissTex_VPanner;DissTex_VPanner;21;0;Create;True;0;0;0;False;0;False;0;0.36;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;18;-1791.127,1236.628;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;94;-691.2455,-91.83948;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;59;-919.4025,-697.8564;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;111;-1614.726,1256.106;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;5;-1503.797,714.1422;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;83;-918.808,1790.53;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;79;-1012.024,1564.104;Inherit;False;0;78;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;105;-1329.344,722.4567;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1634.997,425.1414;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;106;-1104.344,707.4567;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;108;-1152.151,825.5739;Inherit;False;Property;_Edge_Width;Edge_Width;3;0;Create;True;0;0;0;False;0;False;0.29;0.29;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;4;-1351.797,536.1413;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;60;-703.3741,-591.1747;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;85;-497.808,1780.53;Inherit;False;Property;_Dissolve_Value;Dissolve_Value;22;0;Create;True;0;0;0;False;0;False;-1;1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1545.573,1027.246;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;80;-804.808,1570.53;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-1141.797,504.1413;Inherit;True;Property;_Sword_Texture;Sword_Texture;0;0;Create;True;0;0;0;False;0;False;-1;e947430d99432ed4697b90e5e7a273ed;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;91;-596.8411,-377.4485;Inherit;False;Property;_Eni_Inten;Eni_Inten;6;0;Create;True;0;0;0;False;0;False;1;1;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1317.964,1291.022;Inherit;False;Property;_Masked_Power;Masked_Power;13;0;Create;True;0;0;0;False;0;False;3.55;-11.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;75;-226.107,1888.364;Inherit;False;Property;Use_Custom;Use_Custom;17;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-512.5336,-748.1833;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;78;-630.0795,1544.383;Inherit;True;Property;_Dissolve_Texture;Dissolve_Texture;19;0;Create;True;0;0;0;False;0;False;-1;04018d4e5ecda6049b39579f207e2114;145143eb93006454baed654b5669d0c3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;63;-426.6234,-513.7151;Inherit;False;Constant;_Float1;Float 1;16;0;Create;True;0;0;0;False;0;False;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;20;-1118.932,1027.246;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;62;-250.9386,-749.1566;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;76;-318.2809,-369.5087;Inherit;False;Property;Use_Custom;Use_Custom;17;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;84;-157.8081,1554.53;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;98;-110.4539,1465.635;Inherit;False;Property;_Dissolve_Step;Dissolve_Step;24;0;Create;True;0;0;0;False;0;False;0.47;0.55;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;49;-482.8136,-102.0079;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-842.8342,353.7576;Inherit;False;Property;_Main_intencity;Main_intencity;9;0;Create;True;0;0;0;False;0;False;1;1;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;22;-896.5193,1023.049;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;-7.241089,-665.8485;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;97;75.54614,1550.635;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-194.8136,49.9921;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;110;-604.8298,489.131;Inherit;False;Property;_Use_Edged;Use_Edged;27;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-433.9281,533.1801;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-18.81368,-14.0079;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;86;308.1919,1575.53;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;68;144.0248,-757.6142;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;69;-34.81367,-222.0079;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;65;66.3558,-508.5773;Inherit;False;Property;_Emi_Color;Emi_Color;4;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;313.0751,-765.096;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;340.2474,17.12819;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;67;639.2488,-393.4512;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;112;995.42,-79.98476;Float;False;False;-1;2;UnityEditor.ShaderGraphUnlitGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;False;False;False;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Unlit;True;3;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;0;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;113;995.42,-79.98476;Float;False;True;-1;2;UnityEditor.ShaderGraphUnlitGUI;0;13;HW/Alphablend_Sword_Water;2992e84f91cbeb14eab234972e07ea9d;True;Forward;0;1;Forward;8;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;True;True;0;True;_CULL;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;False;False;False;True;4;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;UniversalMaterialType=Unlit;True;3;True;12;all;0;True;True;2;5;False;;10;False;;2;5;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalForwardOnly;False;False;0;;0;0;Standard;23;Surface;1;638352989216638362;  Blend;0;0;Two Sided;1;0;Forward Only;0;0;Cast Shadows;0;638352972531868388;  Use Shadow Threshold;0;0;Receive Shadows;0;638352972538073930;GPU Instancing;1;0;LOD CrossFade;0;0;Built-in Fog;0;0;DOTS Instancing;0;0;Meta Pass;0;0;Extra Pre Pass;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,;0;  Type;0;0;  Tess;16,False,;0;  Min;10,False,;0;  Max;25,False,;0;  Edge Length;16,False,;0;  Max Displacement;25,False,;0;Vertex Position,InvertActionOnDeselection;1;0;0;10;False;True;False;True;False;False;True;True;True;False;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;114;995.42,-79.98476;Float;False;False;-1;2;UnityEditor.ShaderGraphUnlitGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;False;False;False;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Unlit;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=ShadowCaster;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;115;995.42,-79.98476;Float;False;False;-1;2;UnityEditor.ShaderGraphUnlitGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;False;False;False;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Unlit;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;False;False;True;1;LightMode=DepthOnly;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;116;995.42,-79.98476;Float;False;False;-1;2;UnityEditor.ShaderGraphUnlitGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;False;False;False;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Unlit;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;117;995.42,-79.98476;Float;False;False;-1;2;UnityEditor.ShaderGraphUnlitGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;Universal2D;0;5;Universal2D;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;False;False;False;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Unlit;True;3;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=Universal2D;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;118;995.42,-79.98476;Float;False;False;-1;2;UnityEditor.ShaderGraphUnlitGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;SceneSelectionPass;0;6;SceneSelectionPass;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;False;False;False;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Unlit;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=SceneSelectionPass;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;119;995.42,-79.98476;Float;False;False;-1;2;UnityEditor.ShaderGraphUnlitGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ScenePickingPass;0;7;ScenePickingPass;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;False;False;False;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Unlit;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Picking;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;120;995.42,-79.98476;Float;False;False;-1;2;UnityEditor.ShaderGraphUnlitGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;DepthNormals;0;8;DepthNormals;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;False;False;False;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Unlit;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=DepthNormalsOnly;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;121;995.42,-79.98476;Float;False;False;-1;2;UnityEditor.ShaderGraphUnlitGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;DepthNormalsOnly;0;9;DepthNormalsOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;False;False;False;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Unlit;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=DepthNormalsOnly;False;True;9;d3d11;metal;vulkan;xboxone;xboxseries;playstation;ps4;ps5;switch;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.RangedFloatNode;122;709.3663,242.1728;Inherit;False;Property;_threshold;threshold;28;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CeilOpNode;93;-726.7529,-119.3668;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-1157.094,108.8046;Inherit;False;Property;_Ceil_Count;Ceil_Count;23;0;Create;True;0;0;0;False;0;False;3.043608;20;1;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;72;10.0133,633.9033;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;-114.5044,636.4926;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;125;-233.5201,599.9304;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;-802.318,587.3336;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;107;-1012.495,794.0018;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;126;-878.5654,838.1815;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;73;-1303.289,-462.5515;Inherit;False;Property;Use_Custom;Use_Custom;22;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;57;-1421.464,-740.5427;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;127;-1256.44,-693.0187;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;124;-30.06309,-749.3621;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;-637.4868,168.0876;Inherit;False;Property;_Main_power;Main_power;10;0;Create;True;0;0;0;False;0;False;1.34;-2.47;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-190.6963,-232.3725;Inherit;False;Property;_Main_Color;Main_Color;8;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-1503.502,1035.275;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;4;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;129;-1287.289,1071.011;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;74;-1754.484,678.2972;Inherit;False;Property;Use_Custom;Use_Custom;17;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;130;1472.19,102.4734;Inherit;False;Property;_CULL;CULL;29;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.CullMode;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;131;-647.7153,1190.055;Inherit;False;Property;_Use_MaskTex;Use_MaskTex;30;0;Create;True;0;0;0;False;0;False;0;0;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;134;-819.4878,1258.792;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;133;-830.4878,1335.792;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;5.91;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;136;-358.8606,1214.106;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;135;-1387.488,1476.792;Inherit;False;Property;_mask_pow;mask_pow;32;0;Create;True;0;0;0;False;0;False;0;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;132;-1547.633,1341.55;Inherit;True;Property;_TextureSample0;Texture Sample 0;31;0;Create;True;0;0;0;False;0;False;-1;None;4896eaa343db47a4cb3d39cd99ac771e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;137;-1234.548,1404.293;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
WireConnection;39;0;37;0
WireConnection;39;1;38;0
WireConnection;40;0;41;0
WireConnection;40;2;39;0
WireConnection;36;1;40;0
WireConnection;44;0;36;0
WireConnection;46;0;44;0
WireConnection;46;1;45;0
WireConnection;47;0;48;0
WireConnection;47;1;46;0
WireConnection;15;0;14;0
WireConnection;15;1;13;0
WireConnection;12;0;47;0
WireConnection;12;2;15;0
WireConnection;100;0;99;2
WireConnection;100;1;101;0
WireConnection;9;1;12;0
WireConnection;102;0;100;0
WireConnection;102;1;103;0
WireConnection;104;0;9;1
WireConnection;104;1;102;0
WireConnection;96;0;104;0
WireConnection;96;1;95;0
WireConnection;56;0;127;0
WireConnection;56;1;73;0
WireConnection;18;0;16;1
WireConnection;94;0;93;0
WireConnection;94;1;95;0
WireConnection;59;0;56;0
WireConnection;111;0;18;0
WireConnection;5;0;74;0
WireConnection;5;1;7;0
WireConnection;83;0;81;0
WireConnection;83;1;82;0
WireConnection;106;0;105;2
WireConnection;4;0;3;0
WireConnection;4;2;5;0
WireConnection;60;0;59;0
WireConnection;60;1;94;0
WireConnection;17;0;16;1
WireConnection;17;1;111;0
WireConnection;80;0;79;0
WireConnection;80;2;83;0
WireConnection;1;1;4;0
WireConnection;75;1;85;0
WireConnection;75;0;77;2
WireConnection;61;0;59;0
WireConnection;61;1;60;0
WireConnection;78;1;80;0
WireConnection;20;0;129;0
WireConnection;20;1;21;0
WireConnection;62;0;61;0
WireConnection;62;1;63;0
WireConnection;76;1;91;0
WireConnection;76;0;77;4
WireConnection;84;0;78;1
WireConnection;84;1;75;0
WireConnection;49;0;94;0
WireConnection;49;1;50;0
WireConnection;22;0;20;0
WireConnection;90;0;124;0
WireConnection;90;1;76;0
WireConnection;97;0;98;0
WireConnection;97;1;84;0
WireConnection;51;0;49;0
WireConnection;51;1;52;0
WireConnection;110;1;1;1
WireConnection;110;0;109;0
WireConnection;24;0;110;0
WireConnection;24;1;136;0
WireConnection;10;0;2;0
WireConnection;10;1;51;0
WireConnection;86;0;97;0
WireConnection;68;0;90;0
WireConnection;64;0;68;0
WireConnection;64;1;65;0
WireConnection;92;0;10;0
WireConnection;92;1;69;0
WireConnection;67;0;64;0
WireConnection;67;1;92;0
WireConnection;113;2;67;0
WireConnection;113;3;72;0
WireConnection;113;4;122;0
WireConnection;93;0;96;0
WireConnection;72;0;88;0
WireConnection;88;0;125;0
WireConnection;88;1;86;0
WireConnection;125;0;24;0
WireConnection;109;0;1;1
WireConnection;109;1;126;0
WireConnection;107;0;106;0
WireConnection;107;1;108;0
WireConnection;126;0;107;0
WireConnection;73;1;58;0
WireConnection;73;0;77;3
WireConnection;57;0;55;1
WireConnection;127;0;57;0
WireConnection;124;0;62;0
WireConnection;19;0;17;0
WireConnection;129;0;19;0
WireConnection;74;1;6;0
WireConnection;74;0;77;1
WireConnection;131;1;22;0
WireConnection;131;0;134;0
WireConnection;134;0;133;0
WireConnection;133;0;137;0
WireConnection;133;1;135;0
WireConnection;136;0;131;0
WireConnection;137;0;132;1
ASEEND*/
//CHKSM=7C0960D1C34961E05EDBC0B069C3CEB5EE3F1D01