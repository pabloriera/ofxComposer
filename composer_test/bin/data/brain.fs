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
uniform float uni_sigma;//0<0.5
uniform float uni_gain;//0<2

uniform sampler2DRect backbuffer; // Previus frame for PingPong

uniform sampler2DRect tex0; // First dot on the left of the patch

const float pi = 3.1415926;

//main

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

float patch(vec2 xy,vec2 xy0,float sig)
{
    vec2 aux = vec2(-xy0.y,-xy0.x);
    float d = distance(xy,aux);
    return exp(-pow(d,2.0)/sig);
}

// Number  labels  theta   radius  X   Y   Z   sph_theta   sph_phi type    
// 1   AF3 -23 0.411   0.885   0.376   0.276   23  16  1   1 
// 2   F7  -54 0.511   0.587   0.809   -0.0349 54  -2  1   2 
// 3   F3  -39 0.333   0.673   0.545   0.5 39  30  1   3 
// 4   FC5 -69 0.394   0.339   0.883   0.326   69  19  1   4 
// 5   T7  -90 0.511   6.12e-017 0.999 -0.0349 90  -2  1   5 
// 6   P7  -126    0.511   -0.587  0.809   -0.0349 126 -2  1   6 
// 7   O1  -162    0.511   -0.95   0.309   -0.0349 162 -2  1   7 
// 8   O2  162 0.511   -0.95   -0.309  -0.0349 -162    -2  1   8 
// 9   P8  126 0.511   -0.587  -0.809  -0.0349 -126    -2  1   9 
// 10  T8  90  0.511   6.12e-017 0.999 -0.0349 -90 -2  1   10 
// 11  FC6 69  0.394   0.339   -0.883  0.326   -69 19  1   11 
// 12  F4  39  0.333   0.673   -0.545  0.5 -39 30  1   12 
// 13  F8  54  0.511   0.587   -0.809  -0.0349 -54 -2  1   13 
// 14  AF4 23  0.411   0.885   -0.376  0.276   -23 16  1   14 

vec2[14] xy0;

float[14] b;

void main(void){

// vec2 st = gl_FragCoord.xy/uni_scale;
vec2 st = (gl_FragCoord.xy - resolution.xy/2.0) / min(resolution.x, resolution.y)/uni_scale;
int N = 14;

xy0[0] = vec2(0.885,   0.376);
xy0[1] = vec2(0.587,   0.809);
xy0[2] = vec2(0.673,   0.545);
xy0[3] = vec2(0.339,   0.883); 
xy0[4] = vec2(6.12e-017, 0.999); 
xy0[5] = vec2(-0.587,  0.809); 
xy0[6] = vec2(-0.95 ,  0.309); 
xy0[7] = vec2(-0.95 ,  -0.309);  
xy0[8] = vec2(-0.587,  -0.809); 
xy0[9] = vec2(6.12e-017, -0.999); 
xy0[10] = vec2(0.339 ,  -0.883);  
xy0[11] = vec2(0.673 ,  -0.545);  
xy0[12] = vec2(0.587 ,  -0.809);  
xy0[13] = vec2(0.885 ,  -0.376);  

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

// vec4 color = texture2DRect(tex0,st);


float color = 0.0;

for(int i = 0; i<N;i++)
    {
        // float th = (i+0.499)/(float)N*2*pi+pi;
        // vec2 xy =5.0*vec2(sin(th),cos(th));

        color += b[i]*patch(st,xy0[i],uni_sigma);
    }


// float b = 1.0;
// if (color<uni_gain/100.0);
//     b = 0.0;

color *= uni_gain;

vec3 hsv = hsv2rgb( vec3( clamp(color,0.0,1.0),0.8,1.0) );
 
// gl_FragColor = vec4(color.r,color.g,color.b,d*uni_gain);

gl_FragColor = vec4(hsv,1.0);


}