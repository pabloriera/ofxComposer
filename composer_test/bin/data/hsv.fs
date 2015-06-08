uniform float time;
uniform vec2 resolution;
uniform vec2 size;
uniform vec2 mouse;
uniform float uni_b0;//0<1
uniform float uni_b1;//0<1
uniform float uni_b2;//0<1
uniform float uni_b3;//0<1
uniform float uni_b4;//0<1
uniform float uni_b5;//0<1
uniform float uni_b6;//0<1
uniform float uni_b7;//0<1
uniform float uni_b8;//0<1
uniform float uni_b9;//0<1
uniform float uni_b10;//0<1
uniform float uni_b11;//0<1
uniform float uni_b12;//0<1
uniform float uni_b13;//0<1
uniform float uni_scale;//0<2
uniform float uni_curve;//0<2
uniform float uni_gain;//0<10

uniform sampler2DRect backbuffer; // Previus frame for PingPong

uniform sampler2DRect tex0; // First dot on the left of the patch

const float pi = 3.1415926;


vec3 rgb2hsv(vec3 c)
{
    vec4 K = vec4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    vec4 p = mix(vec4(c.bg, K.wz), vec4(c.gb, K.xy), step(c.b, c.g));
    vec4 q = mix(vec4(p.xyw, c.r), vec4(c.r, p.yzx), step(p.x, c.r));

    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return vec3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}
vec3 hsv2rgb(vec3 c)
{
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

float[14] b;

void main(void){

vec2 st = gl_FragCoord.xy/uni_scale;

int N = 14;

b[0] = uni_b0;
b[1] = uni_b1;
b[2] = uni_b2;
b[3] = uni_b3;
b[4] = uni_b4;
b[5] = uni_b5;
b[6] = uni_b6;
b[7] = uni_b7;
b[8] = uni_b8;
b[9] = uni_b9;
b[10] = uni_b10;
b[11] = uni_b11;
b[12] = uni_b12;
b[13] = uni_b13;

vec4 color = texture2DRect(tex0,st);
vec3 hsv = rgb2hsv(color.rgb);

float d=0.0;
for(int i = 0;i<N;i++)
{
	 d = max(d,b[i]*(1.0 - pow( abs(hsv.x-(i+1)/(float)(N+1) ),uni_curve) ) );
}

	hsv.y = d*uni_gain;
	vec3 colo2 = hsv2rgb(hsv);

	// gl_FragColor = vec4(color.r,color.g,color.b,d*uni_gain);
	gl_FragColor = vec4(colo2,1.0);


}