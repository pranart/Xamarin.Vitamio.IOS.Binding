using System;
using VitamioIOSBinding;
using UIKit;
using AVFoundation;
using Foundation;

namespace VitamioIOSBindingDemo
{
	public partial class ViewController : UIViewController
	{
		public VitamioIOSBinding.VMediaPlayer Player { get; set; }
		public ViewController (IntPtr handle)
			: base (handle)
		{
			Player = VMediaPlayer.SharedInstance ();
			Player.SetupPlayerWithCarrierView (this.View, new VdoDelegate (this));
			Player.SetDataSource (new NSUrl("http://192.168.1.121:1935/vod/mp4:sample.mp4/playlist.m3u8"));
			Player.PrepareAsync ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
	public class VdoDelegate : VitamioIOSBinding.VMediaPlayerDelegate
	{
		ViewController Controller { get; set; }

		public VdoDelegate(ViewController controller)
		{
			Controller = controller;
		}
 
		public override void DidPrepared(VMediaPlayer player,NSObject obj)
		{
			Controller.Player.Start ();
		}

		public override void PlaybackCompleted(VMediaPlayer player,NSObject obj)
		{
		}

		public override void Error(VMediaPlayer player,NSObject obj)
		{
		}

	}
}

