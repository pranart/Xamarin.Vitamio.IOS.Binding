using System;
using ObjCRuntime;

[assembly: LinkWith ("libffmpeg.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s| LinkTarget.Arm64, SmartLink = true, ForceLoad = true
	,Frameworks="AVFoundation AudioToolbox CoreGraphics CoreMedia CoreVideo Foundation MediaPlayer OpenGLES QuartzCore UIKit",
	LinkerFlags="-lz -lbz2 -lstdc++ -liconv -ObjC")]