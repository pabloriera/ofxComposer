uniform float time;
uniform vec2 resolution;
uniform vec2 size;
uniform vec2 mouse;
uniform float uni_a;//0<100
uniform float uni_b;//0<100
uniform sampler2DRect backbuffer; // Previus frame for PingPong

uniform sampler2DRect tex0; // First dot on the left of the patch

const float pi = 3.1415926;

void main(void){

vec2 st = gl_FragCoord.xy;
int x = st.x-resolution.x/2.0;
int y = st.y-resolution.y/2.0;

float c=0.0;

int a = uni_a;
int b = uni_b;

if( x+y<a && x-y<a && (x+200)>a )
	c = x;

gl_FragColor = vec4(c,c,c,1.0);

}