#ifdef GL_ES
precision mediump float;
#endif

uniform float time;
uniform vec2 resolution;
uniform float uni_sc;//0<1
uniform float uni_size;//0<10

void main(void) {
	float p = abs(uni_size * length((gl_FragCoord.xy - resolution.xy/2.0) / min(resolution.x, resolution.y)) - 1.);
	gl_FragColor = vec4(uni_sc /p * vec3(1.,1.,1.), 1.);
}