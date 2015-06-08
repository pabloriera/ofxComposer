uniform float time;
uniform vec2 resolution;
uniform vec2 mouse;
uniform float uni_c;//-1<1
uniform sampler2DRect backbuffer; // Previus frame for PingPong

uniform sampler2DRect tex0; // First dot on the left of the patch

const float pi = 3.1415926;

void main(void){
vec2 st = gl_FragCoord.xy;
vec4 color = texture2DRect(tex0, st);

float c = uni_c*(color.r-color.g);

 gl_FragColor = vec4(c,c,c,1.0);
}