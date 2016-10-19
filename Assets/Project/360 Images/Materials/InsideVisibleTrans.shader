Shader "InsideVisibleTrans"
{
	
	Properties{
		_MainTex("Base (RGB), Alpha (A)", 2D) = "white" {}
		_Color("Texture Color and Transparency",Color) = (1,1,1,1)
	}

		SubShader{
		Tags{ "Queue"="Transparent" "RenderType" = "Transparent" }
		Cull front    // ADDED BY BERNIE, TO FLIP THE SURFACES
		LOD 100
		

		Pass{
		Blend SrcAlpha One
		
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

	uniform	sampler2D _MainTex;
	uniform	float4 _MainTex_ST;

	uniform half4 _Color;

	struct appdata_t {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		half2 texcoord : TEXCOORD0;
		half4 color : COLOR;
	};

	

	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		// ADDED BY BERNIE:
		v.texcoord.x = 1 - v.texcoord.x;
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.color = _Color;
		return o;
	}

	half4 frag(v2f i) : SV_Target
	{
		half4 col = tex2D(_MainTex, i.texcoord);
		col.a = i.color.a;
	//return float4(col*_Color);
		return col;
	}
		ENDCG
	}
	}

}