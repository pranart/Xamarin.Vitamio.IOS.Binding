using System;
using ObjCRuntime;

[assembly: LinkWith ("libVitamio.a", LinkTarget.Simulator | LinkTarget.ArmV7 | LinkTarget.Arm64, SmartLink = true, ForceLoad = true
	,Frameworks="AVFoundation AudioToolbox CoreGraphics CoreMedia CoreVideo Foundation MediaPlayer OpenGLES QuartzCore UIKit",
	LinkerFlags="-lz  -lbz2 -lstdc++ -liconv -ObjC")]
