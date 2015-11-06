using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;
using System.Security;
using System.IO;
using CoreImage;

namespace VitamioIOSBinding
{
	[BaseType(typeof(NSObject))]
	public partial interface VSingleton
	{
		[Static]
		[Export("createSharedInstance:")]
		void CreateSharedInstance(out VSingleton singleton);
	}

	[BaseType(typeof(VSingleton))]
	public partial interface VMediaPlayer
	{
		[Static]
		[Export("sharedInstance")]
		VMediaPlayer SharedInstance();

		/** Setup the media player to work with the given view and the given `VMediaPlayerDelegate` implementor.
		 *
		 * @param carrier The view of video picture will rendering to.
		 * @param delegate The protocol to setup.
		 * @return Returns YES or NO if setup fails.
		 * @see unSetupPlayer
		 */
		[Export("setupPlayerWithCarrierView:withDelegate:")]
		bool SetupPlayerWithCarrierView(UIView carrier,VMediaPlayerDelegate dlg);

		/** Unsetup the media player.
		 *
		 * @return Returns YES or NO if the media player have not ever setup yet.
		 * @see setupPlayerWithCarrierView:withDelegate:
		 */
		[Export("unSetupPlayer")]
		bool UnSetupPlayer();


		/** Specifies whether auto switch decoding scheme when media player prepared failed
		 * with the hint of `decodingSchemeHint`, default is YES.
		 *
		 * @see emVMDecodingScheme
		 * @see decodingSchemeHint
		 */
		[Export("autoSwitchDecodingScheme",ArgumentSemantic.UnsafeUnretained)]
		bool AutoSwitchDecodingScheme { get; set; }

		/** Specifies the hint of decoding scheme, default is `VMDecodingSchemeSoftware`.
		 *
		 * @see emVMDecodingScheme
		 * @see autoSwitchDecodingScheme
		 */

		[Export("decodingSchemeHint",ArgumentSemantic.UnsafeUnretained)]
		DecodingScheme DecodingSchemeHint { get; set; }

		/** The decoding scheme of media player using at this time.
		 *
		 * @see emVMDecodingScheme
		 * @see decodingSchemeHint
		 */

		[Export("decodingSchemeUsing",ArgumentSemantic.UnsafeUnretained)]
		DecodingScheme DecodingSchemeUsing { get;  }

		/** Specifies whether enable cache for online media stream, default is NO.
		 *
		 * @see clearCache
		 */
		[Export("useCache",ArgumentSemantic.UnsafeUnretained)]
		bool UseCache { get; set;}

		/** Set the media url to media player.
		 *
		 * @param mediaURL The url of media want to play.
		 * @see setDataSource:header:
		 * @see prepareAsync
		 */
		[Export("setDataSource:")]
		void SetDataSource(NSUrl mediaURL);

		/** Set the media url to media player, and also pass protocol header to it.
		 *
		 * This method is provide for some online media stream, the parameter *header* is usefull for
		 * media player on open and prepare the media stream, but it is not necessary. optionally, You
		 * can use setOptionsWithKeys:withValues: method to do that.
		 *
		 * @param mediaURL The url of media want to play.
		 * @param header The protocol header, e.g. HTTP header.
		 * @see setDataSource:
		 * @see setOptionsWithKeys:withValues:
		 * @see prepareAsync
		 */
		[Export("setDataSource:header:")]
		void SetDataSource(NSUrl mediaURL,string header);

		/** Set the media segment urls to media player.
		 *
		 * This method is provide for some media stream, which contain by many segments.
		 *
		 * @param baseURL The base url to locate steam file list. Can be nil.
		 * @param list The segments list.
		 * @see setDataSource:
		 * @see prepareAsync
		 */
		[Export("setDataSegmentsSource:fileList:")]
		void SetDataSegmentsSource(string baseUrl,NSArray fileList);

		/** Set the diretory for cache data to store.
		 *
		 * @param directory
		 */
		[Export("setCacheDirectory:")]
		void SetCacheDirectory(string directory);

		/** Pass options to media.
		 *
		 * @param keys
		 * @param values
		 * @see setDataSource:
		 * @see prepareAsync
		 */
		[Export("setOptionsWithKeys:withValues:")]
		void SetOptionsWithKeys(NSArray keys,NSArray values);

		/** Prepares the player for playback, asynchronously.
		 *
		 * After setting the datasource , you need to call prepareAsync, which returns immediately.
		 * It will trigger the protocol `mediaPlayer:didPrepared:` when player prepared successfully
		 * or `mediaPlayer:erro:` if failed.
		 *
		 * @see setDataSource:
		 * @see setDataSource:header:
		 */
		[Export("prepareAsync")]
		void PrepareAsync();

		/** Starts or resumes playback.
		 *
		 * If playback had previously been paused,
		 * playback will continue from where it was paused. If playback had been
		 * stopped, or never started before, playback will start at the beginning.
		 */
		[Export("start")]
		void Start();

		/** Pauses playback.
		 *
		 * Call `start` to resume.
		 */
		[Export("pause")]
		void Pause();

		/** Checks whether the VMediaPlayer is playing.
		 *
		 * @return YES if currently playing, NO otherwise.
		 */
		[Export("isPlaying")]
		bool IsPlaying();

		/** Resets the VMediaPlayer to its uninitialized state.
		 *
		 * After calling this
		 * method, you will have to initialize it again by setting the data source `setDataSource:` and
		 * calling `prepareAsync`.
		 */
		[Export("reset")]
		void Reset();

		/**
		 * Gets the duration of the media.
		 *
		 * @return Returns the duration in milliseconds, or -1 if error occur.
		 */
		[Export("getDuration")]
		long GetDuration();

		/**
		 * Gets the current playback position.
		 *
		 * @return Returns the current position in milliseconds, or -1 if orror occur.
		 */
		[Export("getCurrentPosition")]
		long GetCurrentPosition();

		/**
		 * Seeks to specified time position.
		 *
		 * @param msec the offset in milliseconds from the start to seek to.
		 */
		[Export("seekTo:")]
		void SeekTo(long msec);

		/**
		 * Set video and audio playback speed.
		 *
		 * @param speed e.g. 0.8 or 2.0, default to 1.0, range in [0.5-2]
		 */
		[Export("setPlaybackSpeed:")]
		void SetPlaybackSpeed(nfloat speed);

		/**
		 * Adaptive streaming support, default is NO.
		 *
		 * @param adaptive YES if wanna adaptive steam.
		 */
		[Export("setAdaptiveStream:")]
		bool SetAdaptiveStream(bool adaptive);

		/**
		 * Checks whether the VMediaPlayer is using hardware decoding.
		 *
		 * @return Returns YES if it is using hardware decoding, NO otherwise.
		 */
		[Export("isUsingHardwareDecoding")]
		bool isUsingHardwareDecoding();

		/**
		 * Set the encoding VMediaPlayer will use to determine the metadata.
		 *
		 * @param encoding e.g. "UTF-8"
		 */
		[Export("setMetaEncoding:")]
		void SetMetaEncoding(string encoding);

		/**
		 * Get the encoding if haven't set with `setMetaEncoding:`
		 *
		 * @return Returns the encoding of meta data, or nil if error occurs.
		 */    	
		[Export("getMetaEncoding")]
		string GetMetaEncoding();

		/**
		 * Gets the media metadata.
		 *
		 * @return Returns the metadata, possibly empty, or nil if errors occurred.
		 */
		[Export("getMetadata")]
		NSDictionary GetMetaData();

		/**
		 * Gets the size on disk of the media.
		 *
		 * @return Return the size in bytes, or -1 if error occurs.
		 */
		[Export("getDiskSize")]
		ulong GetDiskSize();
	
		/**
		* Tell the VMediaPlayer whether to show video.
		*
		* @param shown YES if wanna show
			*/
		[Export("setVideoShown:")]
		void SetVideoShown(bool shown);
		/**
		 * Returns an array of video tracks information.
		 *
		 * The usage see in `getAudioTracksArray`.

		 * @return Returns Array of video track info. The total number of tracks is the array length;
		 *         if error occurs, nil is returned.
		 */


		[Export("getVideoTracksArray")]
		NSArray GetVideoTracksArray();
		
		/**
		 * Switch to a new video track.
		 *
		 * Tell VMediaPlayer switch the video track to the new track indicate by *index*.
		 *
		 * @param index One of the indexes of array return by `getVideoTracksArray`.
		 * @return Returns YES for success or NO if error occur.
		 * @see getVideoTracksArray
		 */
		[Export("setVideoTrackWithArrayIndex:")]
		bool SetVideoTrackWithArrayIndex(nint index);

		/**
		 * Get the video track index of player play currently.
		 *
		 * @return Returns the video track index play currently, or -1 if error occurs.
		 * @see getVideoTracksArray
		 */

		[Export("getVideoTrackCurrentArrayIndex")]
		nint GetVideoTrackCurrentArrayIndex();

		/**
		 * Set the quality when play video.
		 *
		 * If the video is too lag, you may try
		 * VIDEOQUALITY_LOW, default is VIDEOQUALITY_LOW.
		 *
		 * @param quality The quality(`emVMVideoQuality`) want to set.
		 * @see emVMVideoQuality
		 */			
		[Export("setVideoQuality:")]
		void SetVideoQuality(VideoQuality quality);

		/**
		* Returns the width of the video.
		*
		* @return The width of the video, or 0 if there is no video, or the width has
		*         not been determined yet.
		*/
		[Export("getVideoWidth")]
		nint GetVideoWidth();

		/**
		 * Returns the height of the video.
		 *
		 * @return The height of the video, or 0 if there is no video, or the height has
		 *         not been determined yet.
		 */
		[Export("getVideoHeight")]
		nint GetVideoHeight();

		/**
		 * Returns the aspect ratio of the video.
		 *
		 * @return The aspect ratio of the video, or 0 if there is no video, or the
		 *         width and height is not available.
		 */
		[Export("getVideoAspectRatio")]
		nfloat GetVideoAspectRatio();

		/**
		 * Set if should deinterlace the video picture.
		 *
		 * @param deinterlace Pass YES if need deinterlace, NO if not.
		 */
		[Export("setDeinterlace:")]
		void SetDeinterlace(bool deintelace);


		///---------------------------------------------------------------------------------------
		/// @name Audio Control
		///---------------------------------------------------------------------------------------

		/**
		 * Get the current video frame.
		 *
		 * @return Return the UIImage object, or nil if error occurs.
		 */
		[Export("getCurrentFrame")]
		UIImage getCurrentFrame();

		/**
		 * Returns an array of audio tracks information.
		 *
		 * The return array is contained by NSDictionary. Every track infomation map to a dictionary.
		 * You can use the key of `VMMediaTrackLocationType` or `VMMediaTrackId` etc. to get the
		 * detail. Here's an example of simple usage:
		 *
		 *	NSArray *tracks = [player getAudioTracksArray];
		 *	for (NSDictionary *track in tracks) {
		 * 		NSLog(@"LocationType: %d, id: %d, title: %@, exPath: %@",
		 *			 [track[VMMediaTrackLocationType] intValue],
		 *			 track[VMMediaTrackId] ? [track[VMMediaTrackId] intValue] : -1,
		 *			 track[VMMediaTrackTitle] ? track[VMMediaTrackTitle] : @"(none)",
		 *			 track[VMMediaTrackFilePath] ? track[VMMediaTrackFilePath] : @"(none)"
		 * 		);
		 *	 }
		 *
		 * @return Returns array of audio track info. The total number of tracks is the array length;
		 *         returns nil if the current decoding scheme is not support get audio track info.
		 */
		[Export("getAudioTracksArray")]
		NSArray GetAudioTracksArray();

		/**
		 * Switch to a new audio track.
		 *
		 * Tell VMediaPlayer switch the audio track to the new track indicate by *index*.
		 *
		 * @param index One of the indexes of array return by `getAudioTracksArray`.
		 * @return Returns YES for success or NO if error occur.
		 * @see getAudioTracksArray
		 */
		[Export("setAudioTrackWithArrayIndex:")]
		bool SetAudioTrackWithArrayIndex(nint index);

		/**
		 * Get the audio track index of player play currently.
		 *
		 * @return Returns the audio track index play currently, or -1 if error occurs.
		 * @see getAudioTracksArray
		 */

		[Export("getAudioTrackCurrentArrayIndex")]
		int GetAudioTrackCUrrentArrayIndex();


		/**
		 * Set the media player output volume.
		 *
		 * @param volume The volume value, range in [0.0-1.0].
		 */		
		[Export("setVolume:")]
		void SetVolume(nfloat volume);

		/**
		 * Get the media player output volume value.
		 *
		 * @return Returns the current media player volume.
		 */
		[Export("getVolume")]
		nfloat GetVolume();

		/**
		 * Set the left/right volume balance for a stereo audio.
		 *
		 * @param left The left balance, range in [0.0-1.0].
		 * @param right The right balance, range in [0.0-1.0].
		 */
		[Export("setChannelVolumeLeft:right:")]
		void SetChannelVolume(nfloat left,nfloat right);

		/**
		 * Get the left/right volume balance for a stereo audio.
		 *
		 * @param left The left balance return store in *left.
		 * @param right The right balance return store in *left.
		 */
		[Export("getChannelVolumeLeft:right:")]
		void GetChannelVolume(out nfloat left,out nfloat right);

		/**
		 * Amplify audio
		 *
		 * @param ratio  e.g. 3.5
		 */
		[Export("setAudioAmplify:")]
		bool SetAudioAmplify(nfloat ratio);

		/**
		 * Tell the VMediaPlayer whether to show timed text.
		 *
		 * @param shown YES if wanna show
		 */
		[Export("setSubShown:")]
		void SetSubShown(bool shown);

		/**
		 * Returns an array of subtitle tracks information.
		 *
		 * The usage see in `getAudioTracksArray`.
		 *
		 * @return Return array of subtitle track info, The total number of tracks is the array length;
		 *         if error occurs, nil is returned.
		 */
		[Export("getSubTracksArray")]
		NSArray GetSubTracksArray();

		/**
		 * Switch to a new subtitle track.
		 *
		 * Tell VMediaPlayer switch the subtitle track to the new track indicate by *index*.
		 *
		 * @param index One of the indexes of array return by `getSubTracksArray`.
		 * @return Returns YES for success or NO if error occur.
		 * @see getSubTracksArray
		 */
		[Export("setSubTrackWithArrayIndex:")]
		int SetSubTrackWithArrayIndex(int index);

		/**
		 * Get the subtitle track index of player play currently.
		 *
		 * @return Returns the subtitle track index play currently, -1 if error occurs.
		 * @see getSubTracksArray
		 */
		[Export("getSubTrackCurrentArrayIndex")]
		int GetSubTrackCurrentArrayIndex();

		/**
		 * Add a new subtitle track in exteranl subtitle file to the subtitle array.
		 * It will contain in the array return by follow called `getSubTracksArray`.
		 *
		 * @param path The path of external subtitle to add.
		 * @return Returns YES or NO if error occurs.
		 * @see getSubTracksArray
		 */
		[Export("addSubTrackToArrayWithPath:")]
		bool AddSubTrackToArrayWithPath(string path);

		/**
		 * Switch to a new subtitle track by external subtitle.
		 *
		 * @param path The path of external subtitle to use.
		 */
		[Export("setSubTrackWithPath:")]
		void SetSubTrackWithPath(string path);

		/**
		 * Get the timed text at current time.
		 *
		 * @return Returns timed text.
		 */
		[Export("getCurSubText")]
		string GetCurrentSubText();

		 /**
		 * Set the encoding to display timed text.
		 *
		 * @param encoding VMediaPlayer will detet it if nil.
		 */
		[Export("setSubEncoding:")]
		void SetSubEncoding(string encoding);

		/**
		 * The buffer to fill before playback, default is 1024KB
		 *
		 * @param bufSize buffer size in Byte
		 */
		[Export("setBufferSize:")]
		void SetBufferSize(int bufSize);

		/**
		 * Checks whether the buffer is filled
		 *
		 * @return NO if buffer is filled
		 */
		[Export("isBuffering")]
		bool IsBuffering();

		/**
		 * Get buffer progress.
		 *
		 * @return the percent
		 */
		[Export("getBufferProgress")]
		nint GetBufferProgressPercent();


		/**
		 * Clear online stream cache.
		 *
		 * @see useCache
		 */
		[Export("clearCache")]
		void ClearCache();

		/**
		 * Tell media playback view which video fill mode to use.
		 *
		 * @param fillMode The mode in `emVMVideoFillMode`
		 * @see emVMVideoFillMode
		 */
		[Export("setVideoFillMode:")]
		void SetVideoFillMode(FillMode fillMode);

		/**
		 * Get video fill mode of playback view using.
		 *
		 * @return The mode using now.
		 * @see emVMVideoFillMode
		 */
		[Export("getVideoFillMode")]
		FillMode GetVideoFillMode();

		/**
		 * Set the playback view scale, default 1.0
		 *
		 * @param scale e.g. 0.5
		 */
		[Export("setVideoFillScale:")]
		void SetVideoFillScale(nfloat scale);

		/**
		 * Get the current playback view scale.
		 *
		 * @return The playback scale.
		 */
		[Export("getVideoFillScale")]
		nfloat GetVideoFillScale();

		/**
		 * Set the playback view aspect ratio.
		 *
		 * The aspect ratio of video is auto detect by VMediaPlayer, but it may be failed,
		 * so you need use this method to set the right aspect ratio for media player.
		 *
		 * @param ratio aspect ratio
		 */
		[Export("setVideoFillAspectRatio:")]
		void SetVideoFillAspectRatio(nfloat ratio);

	}

	[Model, Protocol, BaseType (typeof(NSObject))]
	public partial interface VMediaPlayerDelegate
	{
		[Abstract]
		[Export("mediaPlayer:didPrepared:")]
		void DidPrepared(VMediaPlayer player,NSObject obj);

		[Abstract]
		[Export("mediaPlayer:playbackComplete:")]
		void PlaybackCompleted(VMediaPlayer player,NSObject obj);

		[Abstract]
		[Export("mediaPlayer:setupManagerPreference:")]
		void Error(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:setupPlayerPreference:")]
		void SetupPlayerPreference(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:decodingSchemeChanged:")]
		void DecodingSchemeChanged(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:seekComplete:")]
		void SeekComplete(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:notSeekable:")]
		void notSeekable(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:videoTrackLagging:")]
		void videoTrackLagging(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:downloadRate:")]
		void downloadRate(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:info:")]
		void info(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:bufferingStart:")]
		void bufferingStart(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:bufferingEnd:")]
		void bufferingEnd(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:cacheNotAvailable:")]
		void cacheNotAvailable(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:cacheStart:")]
		void cacheStart(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:cacheUpdate:")]
		void cacheUpdate(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:cacheSpeed:")]
		void cacheSpeed(VMediaPlayer player,NSObject obj);

		[Export("mediaPlayer:cacheComplete:")]
		void cacheComplete(VMediaPlayer player,NSObject obj);

	}

}

