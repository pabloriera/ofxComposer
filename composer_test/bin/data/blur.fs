// Empty Shader Patch for ofxComposer 
// by http://PatricioGonzalezVivo.com 
//
// For quick GLSL prototyping this Patch have the next native variables
//
uniform sampler2DRect backbuffer; // Previus frameBuffer 
uniform sampler2DRect tex0; // Input texture number 0 
 // You can add as many as you want
 // just type name them &apos;tex&apos;+ N

uniform vec2 size0; // tex0 resolution
uniform vec2 resolution; // Patch resolution
uniform vec2 window; // Window resolution
uniform vec2 screen; // Screen resolution
uniform vec2 mouse; // Mouse position on the screen
uniform float time; // seconds 
uniform int pass;
// 

float kernel[9];
vec2 offset[9];

void main(void){
    vec2  st = gl_TexCoord[0].st;
    

    offset[0] = vec2(-1.0, -1.0);
    offset[1] = vec2(0.0, -1.0);
    offset[2] = vec2(1.0, -1.0);

    offset[3] = vec2(-1.0, 0.0);
    offset[4] = vec2(0.0, 0.0);
    offset[5] = vec2(1.0, 0.0);

    offset[6] = vec2(-1.0, 1.0);
    offset[7] = vec2(0.0, 1.0);
    offset[8] = vec2(1.0, 1.0);

    kernel[0] = 1.0/16.0;   kernel[1] = 2.0/16.0;   kernel[2] = 1.0/16.0;
    kernel[3] = 2.0/16.0;   kernel[4] = 4.0/16.0;   kernel[5] = 2.0/16.0;
    kernel[6] = 1.0/16.0;   kernel[7] = 2.0/16.0;   kernel[8] = 1.0/16.0;

    vec4 previousValue = texture2DRect(backbuffer, st);
    
    
    if (pass==0)
    {
        vec4 t0 = texture2DRect(tex0, st);
        gl_FragColor = t0;
    }
    else
    {   
        vec4 sum = vec4(0.0);
        int i = 0;
        for (i = 0; i < 9; i++){
            vec4 tmp = texture2DRect(backbuffer, st + offset[i]);
            sum += tmp * kernel[i];
        }
    gl_FragColor = vec4(sum.rgb, 1.0);
    }
           
    }
