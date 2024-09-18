shader_type canvas_item;

// mipmap is neaded for textureLod
uniform sampler2D screen_texture: hint_screen_texture, repeat_disable, filter_nearest_mipmap;
// works better as a normal with warping
uniform sampler2D warp_texture: repeat_enable;

uniform float intensity: hint_range(0.0, 0.3) = 0.01;
uniform vec4 tint_color: source_color = vec4(0.0, 0.0, 0.0, 1.0);
uniform float tint_amount: hint_range(0.0, 1.0) = 0.4;

void fragment() {
    // get our normal warp
    vec2 warp = texture(warp_texture, UV).xy - 0.5;
    // sample based on warp and intensity and blur based on intensity
    vec4 screen = textureLod(screen_texture, UV + warp * intensity, intensity * 4.0);
    // tint our image
    screen = mix(screen, tint_color, tint_amount);
    // get a random-ish value for some speckle noise
    float noise = fract(sin(dot(UV, vec2(12.9898, 78.233))) * 43758.5453);
    // light diffusion for glass shape highlights
    float diff = max(dot(warp, normalize(vec2(1.0, 1.0))), 0.0);
    // apply diffusion based on intensity
    screen += diff * intensity;
    // apply speckle noise based on intensity
    screen += noise * intensity;
    // yarp
    COLOR = screen;
}