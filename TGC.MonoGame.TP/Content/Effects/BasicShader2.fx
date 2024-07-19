#if OPENGL
    #define SV_POSITION POSITION
    #define VS_SHADERMODEL vs_3_0
    #define PS_SHADERMODEL ps_3_0
#else
    #define VS_SHADERMODEL vs_4_0_level_9_1
    #define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Custom Effects - https://docs.monogame.net/articles/content/custom_effects.html
// High-level shader language (HLSL) - https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl
// Programming guide for HLSL - https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-pguide
// Reference for HLSL - https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-reference
// HLSL Semantics - https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-semantics

float4x4 World;
float4x4 View;
float4x4 Projection;

float3 DiffuseColor;
float Time = 0;

Texture2D ColorTexture;
SamplerState ColorSampler;

Texture2D NormalMap;
SamplerState NormalSampler;

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float2 TexCoord : TEXCOORD0;
    float3 Normal : NORMAL;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;

    // Model space to World space
    float4 worldPosition = mul(input.Position, World);
    // World space to View space
    float4 viewPosition = mul(worldPosition, View);
    // View space to Projection space
    output.Position = mul(viewPosition, Projection);

    // Pass through texture coordinates
    output.TexCoord = input.TexCoord;

    // Calculate normal (for simplicity, just pass through the normal from the vertex data)
    // In a real application, you would typically transform the normal by the World matrix.
    output.Normal = mul(float4(0, 0, 1, 0), World).xyz; 

    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    // Sample the color texture
    float4 color = ColorTexture.Sample(ColorSampler, input.TexCoord);
    
    // Sample the normal map (assuming normal map is in tangent space)
    float3 normal = NormalMap.Sample(NormalSampler, input.TexCoord).xyz * 2.0 - 1.0;

    // Combine the sampled color with the diffuse color
    float4 finalColor = float4(color.rgb * DiffuseColor, color.a);

    return finalColor;
}

technique BasicColorDrawing
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};