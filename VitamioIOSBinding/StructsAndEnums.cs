using System;

namespace VitamioIOSBinding
{
	public enum DecodingScheme {
		VMDecodingSchemeQuickTime = 0, 	/// Support apple quick time medias, e.g. mp4 or mov.
		VMDecodingSchemeSoftware,		/// Support almost all format.
		VMDecodingSchemeHardware,		/// Support H.264 & MPEG4.
	};

	/**
 * Define the media track location.
 */
	public enum LocationType {
		VMLocationInternal = 0,		/// Contain in media stream.
		VMLocationExternal,			/// Exist in external file.
	};

	/**
 * Define the quality of media player can switch.
 */
	public enum VideoQuality {
		Low = -16,	/// Low quality, high speed.
		Medium = 0,	/// Normal.
		High = 16,	/// Hight quality.
	};

	/**
 * Define the video fill mode support by VMediaPlayer playback view.
 */
	public enum FillMode {
		Unknown,		/// Not use.
		Fit,			/// Fit to playback view(carrier view).
		Origin,			/// Use the video original size.
		Crop,		/// Crop video picture to fill with playback view(carrier view).
		Stretch,		/// Stretch video picture to fill with playback view(carrier view).
	};

}

