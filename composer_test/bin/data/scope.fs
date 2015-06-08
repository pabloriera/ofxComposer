#ifdef GL_ES
precision mediump float;
#endif

uniform sampler2DRect backbuffer;

uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;
uniform float uni_c;//0<1
uniform float uni_Y0;//0<480
uniform float uni_Y1;//0<480
uniform float uni_Y2;//0<480
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

void main(void) {
	
	vec2 st = gl_FragCoord.st;
	vec4 data = vec4(0.0);

	data = texture2DRect(backbuffer,st+vec2( (int)uni_d,0.0));	

	data += scope(uni_Y0,vec4(0.5,0.5,0.2,1.0));

	data += scope(uni_Y1,vec4(0.0,0.5,0.2,1.0));

	data += scope(uni_Y2,vec4(0.8,0.2,0.3,1.0));

	gl_FragColor = data*uni_c;

}