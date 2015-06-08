#ifdef GL_ES
precision mediump float;
#endif

uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;

uniform float uni_w;//0<50
uniform float uni_b0;//0<200
uniform float uni_b1;//0<200
uniform float uni_b2;//0<200
uniform float uni_b3;//0<200
uniform float uni_b4;//0<200
uniform float uni_b5;//0<200
uniform float uni_b6;//0<200
uniform float uni_b7;//0<200
uniform float uni_b8;//0<200
uniform float uni_b9;//0<200
uniform float uni_b10;//0<200
uniform float uni_b11;//0<200
uniform float uni_b12;//0<200
uniform float uni_b13;//0<200
uniform float uni_pi;//0<6.28

#define PI 3.1415
#define TAU PI * 2.0

mat4 perspective (float FOV, float aspectRatio, float near, float far){
	float f = 1.0 / tan(FOV / 2.0);
	float nf = near - far;
	
	return mat4(f / aspectRatio, 0, 0, 0,
	0, f, 0, 0,
	0, 0, (far + near) / nf, -1,
	0, 0, (2.0 * far * near) / nf, 0);
}

mat4 veiw = perspective(PI / 2.0, 1.0, 0.1, 1000.0);

float getStripe (float arg){
	//like clip space paramaters, -1 to 1
	float pos = mod(arg, 10.0) / 5.0 - 1.0;
	
	float porabola = 1.0 - pos * pos;
	
	
	return porabola;
}

float generateCircle (vec2 pos, vec2 pos2, float radius){
	float len = length(pos2 - pos);
	
	if (len < radius){
		float m = len / radius;
		
		return 1.0 - m * m;
	}
	
	return 0.0;
}

float distLine(vec2 p0,vec2 p1,vec2 uv){
	vec2 dir = normalize(p1 - p0);
	
	uv = (uv - p0) * mat2(dir.x, dir.y, -dir.y, dir.x);
	
	return distance(uv, clamp(uv, vec2(0.0), vec2(distance(p0, p1), 0.0)));   
}

float generateLine (vec2 from, vec2 to, vec2 pos, float radius){
	
	vec2 dir = normalize(to - from); //Normalized line direction
	
	pos = (pos - from) * mat2(dir.x, dir.y, -dir.y, dir.x); //Make "pos" relative to "from" and rotate it to match "dir"
	
	float dist = distance(pos, clamp(pos, vec2(0.0), vec2(distance(from, to), 0.0))); //Get the distance between "pos" and "pos" clamped to the line.
	
	return smoothstep(radius, radius - 0.75, dist); //Make the pixel white if it's inside the radius (with some smoothing).

}



vec2 spin (vec2 center, float radius, float angle){
	return center + vec2(sin (angle) * radius, cos (angle) * radius);
}

vec2 spin (vec2 radius, float angle){
	return radius * vec2(sin(angle), cos(angle));
}

vec2 ispin (vec2 radius, float angle){
	return radius * vec2(cos(angle), sin(angle));
}

vec3 spin (vec3 radius, float angle){
	return radius * vec3(sin(angle), cos(angle), tan(angle) / sin(angle));
}

vec3 ispin (vec3 radius, float angle){
	return radius * vec3(cos(angle), sin(angle), cos(angle) / sin(angle));
}

float[14] r;

void main( void ) {
	vec2 center = gl_FragCoord.xy - resolution / 2.0;
	
	vec3 color = vec3(0);
	
	//color.x = getStripe(gl_FragCoord.y + u_time * 10.0);
	//color.y = getStripe(gl_FragCoord.x + u_time * 10.0);	
	//color.z = getStripe(sqrt(gl_FragCoord.x + gl_FragCoord.y) + u_time * 10.0);
	
	//color *= 0.5;
	
	/*color += vec3(1, 0, 0) * generateCircle(spin(resolution.xy / 2.0, 300.0, u_time), gl_FragCoord.xy, 60.0);
	color += vec3(1, 0, 0) * generateCircle(spin(resolution.xy / 2.0, 300.0, u_time - PI), gl_FragCoord.xy, 60.0);
	color += vec3(1, 0, 0) * generateCircle(spin(resolution.xy / 2.0, 300.0, u_time - PI / 2.0), gl_FragCoord.xy, 60.0);
	color += vec3(1, 0, 0) * generateCircle(spin(resolution.xy / 2.0, 300.0, u_time + PI / 2.0), gl_FragCoord.xy, 60.0);
	
	color += vec3(0, 0.8, 0) * generateCircle(spin(resolution.xy / 2.0, 200.0, -u_time), gl_FragCoord.xy, 30.0);
	color += vec3(0, 0.8, 0) * generateCircle(spin(resolution.xy / 2.0, 200.0, -u_time - PI), gl_FragCoord.xy, 30.0);
	color += vec3(0, 0.8, 0) * generateCircle(spin(resolution.xy / 2.0, 200.0, -u_time - PI / 2.0), gl_FragCoord.xy, 30.0);
	color += vec3(0, 0.8, 0) * generateCircle(spin(resolution.xy / 2.0, 200.0, -u_time + PI / 2.0), gl_FragCoord.xy, 30.0);
	
	color += vec3(0, 0, 0.6) * generateCircle(spin(resolution.xy / 2.0, 100.0, u_time), gl_FragCoord.xy, 20.0);
	color += vec3(0, 0, 0.6) * generateCircle(spin(resolution.xy / 2.0, 100.0, u_time - PI), gl_FragCoord.xy, 20.0);
	color += vec3(0, 0, 0.6) * generateCircle(spin(resolution.xy / 2.0, 100.0, u_time - PI / 2.0), gl_FragCoord.xy, 20.0);
	color += vec3(0, 0, 0.6) * generateCircle(spin(resolution.xy / 2.0, 100.0, u_time + PI / 2.0), gl_FragCoord.xy, 20.0);*/
	
	//the position of the box
	vec2 pos = center;
	
	// color += vec3(1, 1, 1) * generateLine(spin(vec2(-100, -100), u_time), ispin(vec2( 100, -100), u_time), pos, 2.0);
	// color += vec3(1, 1, 1) * generateLine(spin(vec2(-100, -100), u_time), ispin(vec2(-100,  100), u_time), pos, 2.0);
	// color += vec3(1, 1, 1) * generateLine(spin(vec2( 100,  100), u_time), ispin(vec2( 100, -100), u_time), pos, 2.0);
	// color += vec3(1, 1, 1) * generateLine(spin(vec2( 100,  100), u_time), ispin(vec2(-100,  100), u_time), pos, 2.0);
	
	// color += vec3(1, 1, 1) * generateLine(spin(vec2(-60, -60), u_time), ispin(vec2( 60, -60), u_time), pos, 2.0);
	// color += vec3(1, 1, 1) * generateLine(spin(vec2(-60, -60), u_time), ispin(vec2(-60,  60), u_time), pos, 2.0);
	// color += vec3(1, 1, 1) * generateLine(spin(vec2( 60,  60), u_time), ispin(vec2( 60, -60), u_time), pos, 2.0);
	// color += vec3(1, 1, 1) * generateLine(spin(vec2( 60,  60), u_time), ispin(vec2(-60,  60), u_time), pos, 2.0);
	
	// color += vec3(1, 1, 1) * generateLine(spin(vec2(-60, -60), u_time), spin(vec2(-100, -100), u_time), pos, 2.0);
	// color += vec3(1, 1, 1) * generateLine(spin(vec2( 60,  60), u_time), spin(vec2( 100,  100), u_time), pos, 2.0);
	// color += vec3(1, 1, 1) * generateLine(ispin(vec2(-60,  60), u_time), ispin(vec2(-100, 100), u_time), pos, 2.0);
	// color += vec3(1, 1, 1) * generateLine(ispin(vec2( 60, -60), u_time), ispin(vec2( 100, -100), u_time), pos, 2.0);
	int N = 14;

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
	

	for(int i = 0; i<N;i++)
	{
		float th = (i+0.499)/(float)N*2*PI+uni_pi;
		vec2 xy = vec2(r[i]*sin(th),r[i]*cos(th));
		color += vec3(1, 1, 1) * generateLine(vec2(0.0,0.0) ,xy, pos, uni_w);	

	}
	

	gl_FragColor = vec4(color, 1);
}