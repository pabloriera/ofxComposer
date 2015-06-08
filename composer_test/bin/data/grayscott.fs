
uniform sampler2DRect backbuffer;
uniform sampler2DRect tex0;

uniform float uni_diffU;//0.0<0.3
uniform float uni_diffV;//0.0<0.3
uniform float uni_F;//0.0<0.08
uniform float uni_K;//0.0<0.12
uniform float uni_dt;//0.01<1.5
uniform float uni_uov;//0.0<1.0

float kernel[9];
vec2 offset[9];

   void main(void){
       vec2 st   = gl_FragCoord.xy;
       kernel[0] = 0.707106781;
       kernel[1] = 1.0;
       kernel[2] = 0.707106781;
       kernel[3] = 1.0;       
       kernel[4] = -6.82842712;
       kernel[5] = 1.0;
       kernel[6] = 0.707106781;
       kernel[7] = 1.0;
       kernel[8] = 0.707106781;

       offset[0] = vec2( -1.0, -1.0);
       offset[1] = vec2(  0.0, -1.0);
       offset[2] = vec2(  1.0, -1.0);

       offset[3] = vec2( -1.0, 0.0);
       offset[4] = vec2(  0.0, 0.0);
       offset[5] = vec2(  1.0, 0.0);

       offset[6] = vec2( -1.0, 1.0);
       offset[7] = vec2(  0.0, 1.0);
       offset[8] = vec2(  1.0, 1.0);

       vec2 texColor = texture2DRect( backbuffer, st ).rg;
       vec2 srcTexColor = texture2DRect( tex0, st ).rg;

       vec2 lap = vec2( 0.0, 0.0 );

       for( int i=0; i < 9; i++ ){
           vec2 tmp = texture2DRect( backbuffer, st + offset[i] ).rg;
           lap += tmp * kernel[i];
       }

       float u  = texColor.r + srcTexColor.r * uni_uov * 0.1;
       float v  = texColor.g + srcTexColor.g * (1.0-uni_uov) * 0.1;

       float uvv = u * v * v;

       float du = uni_diffU * lap.r - uvv + uni_F * (1.0 - u);
       float dv = uni_diffV * lap.g + uvv - (uni_F + uni_K) * v;

       u += du * uni_dt;
       v += dv * uni_dt;

       gl_FragColor = vec4(clamp( u, 0.0, 1.0 ), clamp( v, 0.0, 1.0 ),0.0, 1.0);


   }
