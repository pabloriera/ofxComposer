uniform float time;
uniform vec2 resolution;
uniform vec2 mouse;
uniform float uni_r;//0<100

const float pi = 3.1415926;

void main(void){

	vec2 xy = gl_FragCoord.xy;
	vec3 color = vec3(0.0);

	if( distance(xy,mouse) < uni_r)
		color = vec3(1.0);

	if( distance(xy,resolution/2.0) < uni_r)
		color = vec3(1.0);

 	gl_FragColor = vec4(color,1.0);

}