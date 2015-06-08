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
uniform float uni_w;//0<10
uniform float uni_gain;//0<1

uniform sampler2DRect backbuffer; // Previus frame for PingPong

uniform sampler2DRect tex0; // First dot on the left of the patch

const float pi = 3.1415926;

//main
float lookup(vec2 p, float dx, float dy)
{
    float d = uni_w; // kernel offset
    vec2 uv = (p.xy + vec2(dx * d, dy * d));
    vec4 c = texture2DRect(tex0, uv.xy);
	
	// return as luma
    return 0.2126*c.r + 0.7152*c.g + 0.0722*c.b;
}
float[14] b;

void main(void)
{

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
    

    vec2 p = gl_FragCoord.xy/uni_scale;
    
	// simple sobel edge detectpggion
    float gx = 0.0;
    gx += -1.0 * lookup(p, -1.0, -1.0)*b[0];
    gx += -2.0 * lookup(p, -1.0,  0.0)*b[1];
    gx += -1.0 * lookup(p, -1.0,  1.0)*b[2];
    gx +=  1.0 * lookup(p,  1.0, -1.0)*b[3];
    gx +=  2.0 * lookup(p,  1.0,  0.0)*b[4];
    gx +=  1.0 * lookup(p,  1.0,  1.0)*b[5];
    
    float gy = 0.0;
    gy += -1.0 * lookup(p, -1.0, -1.0)*b[6];
    gy += -2.0 * lookup(p,  0.0, -1.0)*b[7];
    gy += -1.0 * lookup(p,  1.0, -1.0)*b[8];
    gy +=  1.0 * lookup(p, -1.0,  1.0)*b[9];
    gy +=  2.0 * lookup(p,  0.0,  1.0)*b[10];
    gy +=  1.0 * lookup(p,  1.0,  1.0)*b[11];
    
	// hack: use g^2 to conceal noise in the video
    float g = gx*gx + gy*gy;
    
    vec4 col = texture2DRect(tex0, p);
    col = uni_gain*col + vec4(g, g, g, 1.0);
    
    gl_FragColor = col;
}