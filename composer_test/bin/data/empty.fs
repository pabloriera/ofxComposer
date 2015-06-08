// Empty Shader Patch for ofxComposer 
// by http://PatricioGonzalezVivo.com 
//
// For quick GLSL prototyping this Patch have the next native variables
//
uniform sampler2DRect backbuffer; // Previus frameBuffer 
uniform sampler2DRect tex0; // Input texture number 0 
uniform sampler2DRect tex1; // Input texture number 0 
 // You can add as many as you want
 // just type name them &apos;tex&apos;+ N

uniform vec2 size0; // tex0 resolution
uniform vec2 resolution; // Patch resolution
uniform vec2 window; // Window resolution
uniform vec2 screen; // Screen resolution
uniform vec2 mouse; // Mouse position on the screen
uniform float time; // seconds 

void main( void ){
 vec2 st = gl_TexCoord[0].st; // gl_FragCoord.st;
 vec4 t0 = texture2DRect(tex0, st);
 vec4 t1 = texture2DRect(tex1, st);

 vec4 diferencia = abs (t0 - t1);
 gl_FragColor = vec4(2.0*diferencia.rgb, 1.0);

}

