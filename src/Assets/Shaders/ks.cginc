void pix_shader_float(UnityTexture2D texture_in, const float2 uv, float time_in, const int scroll_speed_factor,
                      const float zoom,
                      float4 back_star_color,
                      float4 front_star_color,
                      out float4 o)
{
    o = float4(0, 0, 0, 1);
    float volumetric_layer_fade = 1;
    for (int i = 0; i < 12; ++i)
    {
        const float time = (time_in * pow(volumetric_layer_fade, 2.0) * 3.0) / 1000;
        // const float time = time_in / volumetric_layer_fade;
        float2 p = uv * zoom;

        p.y += 1.5;

        // Perform scrolling behaviors. Each layer should scroll a bit slower than the previous one, to give an illusion of 3D.
        p += float2(time * scroll_speed_factor, time * scroll_speed_factor);
        p /= volumetric_layer_fade;
        float total_change = tex2D(texture_in, p).r;

        float3 layer_color = lerp(front_star_color.rgb, back_star_color.rgb, i / 12.0);
        o.rgb += layer_color * total_change * volumetric_layer_fade;

        // Make the next layer exponentially weaker in intensity.
        volumetric_layer_fade *= 0.9;
    }
    o.rgb = pow(o.rgb * 1.010714, 1.3) * 1;
    // o = tex2D(texture_in, uv + 0.1);
}
