using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libQREncoder.a", LinkTarget.Simulator | LinkTarget.ArmV7, ForceLoad = true)]
