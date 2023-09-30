shader_type canvas_item;
uniform sampler2D noise;
uniform float percent;

void fragment() {
	COLOR.rgb = COLOR.rgb * (1.0 - percent) + percent * vec3(1,0,0);
	COLOR.a *= max(0, texture(noise, UV).r + (1.0 - percent * 2.0));
}
