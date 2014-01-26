Shader "Custom/SoundShader" {
	Properties
 	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
 	
     	_Speed("_Speed", Range(0,5) ) = 0.5
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

			//float worldDistFromCenter = distance(IN.worldPos, IN.screenPos.xyz);

			/*
			// calculate texture1 coords
			half timeOffset = half(_Time.x) * _Speed;
			half2 texCoords = float2( IN.uv_Texture1.x , IN.uv_Texture1.y + timeOffset );
			texCoords *= _Tile.xx;
			
			// calculate texture2 coords
			half2 texCoords2 = float2( IN.uv_Texture2.x + timeOffset, IN.uv_Texture2.y);
			texCoords2 *= _Tile.xx;
			*/

			float centeryness = 0;
			float distFromCenter = length((screenUV - _playerPos.xy) * (_ScreenParams.xy / _ScreenParams.y));
			if(distFromCenter > _ClearRadius)
			{
				centeryness = min((distFromCenter - _ClearRadius) * 0.3 , 1) * _Strength;
			}

			float2 texCoords = IN.uv_MainTex;
			float2 offset = tex2D(_MainTex, texCoords).r - 0.5;
			//offset += tex2D(_Texture2, texCoords2).rg - 0.5;

			// set the colours
			//o.Albedo = 0.0;
			o.Emission = tex2D(_GrabTexture, screenUV + offset * centeryness) + max(0, (distFromCenter-_ClearRadius) * 1.1);
			//o.Normal = fixed3(0.0,0.0,1.0);
		}
		ENDCG
	} 
}
