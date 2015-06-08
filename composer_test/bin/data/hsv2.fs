uniform float time;
uniform vec2 resolution;
uniform vec2 size;
uniform vec2 mouse;
uniform float uni_r;//0<1
uniform float uni_g;//0<1
uniform float uni_b;//0<1
uniform float uni_curve;//0<6
uniform float uni_gain;//0<20

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

void main(void){

vec2 st = gl_FragCoord.xy;
int x = st.x-resolution.x/2.0;
int y = st.y-resolution.y/2.0;

vec3 rgb = vec3(uni_r,uni_g,uni_b);

vec4 color = texture2DRect(tex0,st);
vec3 hsv = rgb2hsv(color.rgb);

float d = uni_gain*distance(rgb,color);


hsv.y = hsv.y + 1/pow(d+0.1,uni_curve);
vec3 colo2 = hsv2rgb(hsv);

// gl_FragColor = vec4(color.r,color.g,color.b,d*uni_gain);
gl_FragColor = vec4(colo2,1.0);


}