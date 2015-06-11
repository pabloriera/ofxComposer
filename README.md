# ofxComposer

Fork from original addon (https://github.com/patriciogonzalezvivo/ofxComposer).

This versión has been improved with several stuff.

* GUI to control uniforms in shaders
* Automatic receive OSC messages to modify the GUI controls
* Kinect option
* Lots of new commands, check the help screen (F1)

## Installation

* Download [openFrameworks](openframeworks.cc)
* Clone ofxComposer into addons folder
* Overwrite the openFrameworks/addons/ofxGUI and ofxOsc with the ones in the ofxComposer

## Shaders

Shaders have a set of default uniforms:

* uniform float time
* uniform vec2 mouse // normalized position
* uniform vec2 resolution
* uniform sampler2DRect backbuffer // previus texture for pingpong dinamic )
* uniform sampler2DRect tex0, tex1, tex2, ... // each time you add one of this ones you will get a input

And custom uniforms:
        
* uniform float uni_gain//0<1
        
Uniforms defined with uni_NAME//MIN<MAX will appear in the GUI controls.

