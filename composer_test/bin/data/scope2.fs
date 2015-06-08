#ifdef GL_ES
precision mediump float;
#endif

uniform sampler2DRect backbuffer;

uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;
uniform float uni_c;//0<1
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
uniform float uni_th;//0<20
uniform float uni_d;//0<3
//main
vec4 scope(float Y,vec4 color)
{
	vec2 st = gl_FragCoord.st;
	if(distance(st,vec2(resolution.x-10.0,Y))<uni_th)
		return color;
	else
		return vec4(0.0);
}

float[14] r;

void main(void) {
	
	vec2 st = gl_FragCoord.st;
	vec4 data = vec4(0.0);

	r[0] = uni_b0;
	r[1] = uni_b1;
	r[2] = uni_b2;
	r[3] = uni_b3;
	r[4] = uni_b4;
	r[5] = uni_b5;
	r[6] = uni_b6;
	r[7] = uni_b7;
	r[8] = uni_b8;
	r[9] = uni_b9;
	r[10] = uni_b10;
	r[11] = uni_b11;
	r[12] = uni_b12;
	r[13] = uni_b13;

	data = texture2DRect(backbuffer,st+vec2( (int)uni_d,0.0));	

	for(int i = 0;i<14;i++)
	{
		data += scope((r[i]+i)*480/14,vec4(1.0,1.0,1.0,1.0));
		// data += scope((r[i]+i)*480/14,vec4(0.5,0.5,0.2,1.0));
	}
	
	gl_FragColor = data*uni_c;

}