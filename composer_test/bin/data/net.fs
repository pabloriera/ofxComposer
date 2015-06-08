#ifdef GL_ES
precision mediump float;
#endif

uniform float u_time;
uniform vec2 u_mouse;
uniform vec2 u_resolution;

float lengthsq(vec2 p) { return dot(p, p); }

float noise(vec2 p){
	return fract(sin(fract(sin(p.x) * (43.13311)) + p.y) * 31.001);
}


float worley(vec2 p) {	
	// Initialise distance to a large value
	float d = 20.0;
	for (int xo = -2; xo <= 2; xo++) {
		for (int yo = -2; yo <= 2; yo++) {
			// Test all surrounding cells to see if distance is smaller.
			vec2 test_cell = floor(p) + vec2(xo, yo);
			// Update distance if smaller.
			float n0 = noise(test_cell);
			float n1 = noise(test_cell + vec2(100.0,8413.0));
			
			float ox = mix( n0, n1, sin(u_time*1.2) );
			float oy = mix( n0, n1, cos(u_time) );
			
			vec2 c = test_cell + vec2(ox,oy);
			
			d = min(d, 
				lengthsq(p - c)
			       );

		}
	}
	return d;
}

void main() {
	vec2 uv = gl_FragCoord.xy;
	float z = u_mouse.x/u_resolution.x*100.0;
	float t = 0.9 * worley(gl_FragCoord.xy / z);
	t = sqrt(t);
	gl_FragColor = vec4(vec3(t,t,t), 1.0);
	

}