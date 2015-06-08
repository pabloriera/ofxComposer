uniform float time;
uniform vec2 resolution;
uniform vec2 mouse;
uniform float uni_c;//0<1
uniform sampler2DRect backbuffer; // Previus frame for PingPong

uniform sampler2DRect tex0; // First dot on the left of the patch
uniform sampler2DRect tex1; // Second dot on the left of the patch

const float pi = 3.1415926;

void main(void){
vec2 st = gl_FragCoord.xy;
vec4 A = texture2DRect(tex0, st);
vec4 B = texture2DRect(tex1, st);

 gl_FragColor = mix(A,B,uni_c);
}