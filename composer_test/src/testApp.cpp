#include "testApp.h"

//-------------------------------------------------------------- SETING
void testApp::setup(){
    ofEnableAlphaBlending();
    ofEnableSmoothing();
    ofSetFrameRate(60);
    ofSetVerticalSync(false);

    composer.load("config0.xml");

}

//-------------------------------------------------------------- LOOP
void testApp::update(){

    composer.update();
    ofSetWindowTitle( ofToString( ofGetFrameRate()));
}


void testApp::draw(){
    ofBackgroundGradient(ofColor::gray, ofColor::black);
    ofBackground(ofColor::black);
    ofSetColor(255,255);
    composer.draw();
}


//-------------------------------------------------------------- EVENTS
void testApp::keyPressed(int key){



}

void testApp::keyReleased(int key){
}

void testApp::mouseMoved(int x, int y ){
}

void testApp::mouseDragged(int x, int y, int button){
}

void testApp::mousePressed(int x, int y, int button){

}

void testApp::mouseReleased(int x, int y, int button){

}

void testApp::windowResized(int w, int h){

}

void testApp::gotMessage(ofMessage msg){
}


void testApp::dragEvent(ofDragInfo dragInfo){

    if( dragInfo.files.size() > 0 ){
        cout << dragInfo.files[0] << endl;
		for(int i = 0; i < dragInfo.files.size(); i++){
            composer.addPatchFromFile( dragInfo.files[i], dragInfo.position );
		}
	}
}
