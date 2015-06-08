#include "ofMain.h"
#include "testApp.h"
#include "ofAppGlutWindow.h"
//#include "ofAppGLFWWindow.h"

//========================================================================
int main( ){
//
    ofAppGlutWindow window;

	ofSetupOpenGL(&window, 1024, 768, OF_WINDOW);			// <-------- setup the GL context
//	ofSetupOpenGL(&window, 1680, 1050, OF_GAME_MODE);	    // <-------- setup the GL context

	// this kicks off the running of my app
	// can be OF_WINDOW or OF_FULLSCREEN
	// pass in width and height too:
	ofRunApp( new testApp());

//	ofAppGLFWWindow win;
//	win.setMultiDisplayFullscreen(true); //this makes the fullscreen window span across all your monitors
//
//	ofSetupOpenGL(&win, 800,500, OF_FULLSCREEN);
//	ofRunApp(new testApp());
}
