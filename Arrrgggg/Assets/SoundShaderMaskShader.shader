Shader "Custom/SoundShader SuperMask" {
	Properties
 	{
     	_Strength("_Strength", Range(0,1) ) = 1.5
     	
     	_ClearRadius("Clear Vision Radius", Float) = 0
 	}
	SubShader 
	{
    	Tags
    	{
        	"Queue"="Transparent"
        	"RenderType"="Transparent" 
    	}
    	
		GrabPass{ }
		
		Cull Off
		//ZWrite Off
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed _Speed;
		fixed _Strength;
		half _ClearRadius;
		sampler2D _GrabTexture;
		
		float4 _playerPos;

		struct Input {
			float4 screenPos;
			float3 worldPos;
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

			#if UNITY_UV_STARTS_AT_TOP
			if (_ProjectionParams.x > 0)
			{
				screenUV.y = 1 - screenUV.y;
				_playerPos.y = 1 - _playerPos.y;
			}
            #endif

			float centeryness = 0;
			float distFromCenter = length((screenUV - _playerPos.xy) * (_ScreenParams.xy / _ScreenParams.y));

			// set the colours
			//o.Albedo = 0.0;
			o.Emission = tex2D(_GrabTexture, screenUV) + max(0, (distFromCenter-_ClearRadius) * 1.1);
			//o.Normal = fixed3(0.0,0.0,1.0);
			
			if((distFromCenter-_ClearRadius) * 1.1 < 1)
			{
				clip(-1);
			}	
		}
		ENDCG
	} 
}
