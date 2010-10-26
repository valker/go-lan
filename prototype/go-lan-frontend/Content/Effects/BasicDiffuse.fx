float4x4 World;
float4x4 View;
float4x4 Projection;

float3 lightDir1 = float3(1,1,1);
float3 lightDir2 = float3(-1,-1,-1);

texture TextureMap;
sampler2D diffuseMapSampler = sampler_state
{
	Texture = <TextureMap>;
    MipFilter = ANISOTROPIC;
    MinFilter = ANISOTROPIC;
    MagFilter = ANISOTROPIC;
}; 

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD;
	float3 Normal : NORMAL0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 texCoord : TEXCOORD0;
	float3 Normal : TEXCOORD1;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	output.Normal = mul(input.Normal,World);
	output.texCoord = input.TexCoord;

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 outColor = tex2D(diffuseMapSampler,input.texCoord);
	float diffuse = saturate(dot(input.Normal,normalize(lightDir1))) +
					saturate(dot(input.Normal,normalize(lightDir2)));
    return outColor * diffuse;
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_1_1 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
