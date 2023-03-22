float3 HueShift(float3 In, float Offset)
{
    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float4 P = lerp(float4(In.bg, K.wz), float4(In.gb, K.xy), step(In.b, In.g));
    float4 Q = lerp(float4(P.xyw, In.r), float4(In.r, P.yzx), step(P.x, In.r));
    float D = Q.x - min(Q.w, Q.y);
    float E = 1e-10;
    float3 hsv = float3(abs(Q.z + (Q.w - Q.y) / (6.0 * D + E)), D / (Q.x + E), Q.x);

    float hue = hsv.x + Offset;
    hsv.x = (hue < 0)
        ? hue + 1
        : (hue > 1)
        ? hue - 1
        : hue;

    // HSV to RGB
    float4 K2 = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    float3 P2 = abs(frac(hsv.xxx + K2.xyz) * 6.0 - K2.www);
    return hsv.z * lerp(K2.xxx, saturate(P2 - K2.xxx), hsv.y);
}


void SkinSelector_float(float4 BaseTexture, float Mask, float CurrentColor, float4 Hues, bool HueAsTint, float4 Special01, float4 Special02, float4 Special03, out float3 Color)
{
	Color = (1, 1, 1);
    float3 BaseTint = float3(1, 0, 0);

	if (CurrentColor == 0)
	{
		Color = BaseTexture;
	}
	else if (CurrentColor == 1)
	{
        if (HueAsTint)
        {
            Color = lerp(BaseTexture, HueShift(BaseTexture * BaseTint, Hues.y), Mask);
        }
        else
        {
            Color = lerp(BaseTexture, HueShift(BaseTexture, Hues.y), Mask);
        }
	}
    else if (CurrentColor == 2)
    {
        if (HueAsTint)
        {
            Color = lerp(BaseTexture, HueShift(BaseTexture * BaseTint, Hues.z), Mask);
        }
        else
        {
            Color = lerp(BaseTexture, HueShift(BaseTexture, Hues.z), Mask);
        }
    }
    else if (CurrentColor == 3)
    {
        if (HueAsTint)
        {
            Color = lerp(BaseTexture, HueShift(BaseTexture * BaseTint, Hues.w), Mask);
        }
        else
        {
            Color = lerp(BaseTexture, HueShift(BaseTexture, Hues.w), Mask);
        }
    }
    else if (CurrentColor == 4)
    {
        Color = lerp(BaseTexture, Special01, Mask);
    }
    else if (CurrentColor == 5)
    {
        Color = lerp(BaseTexture, Special02, Mask);
    }
    else if (CurrentColor == 6)
    {
        Color = lerp(BaseTexture, Special03, Mask);
    }
}