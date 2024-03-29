﻿using HackerNews.WPF.Core;
using System;
using System.Windows;

namespace Hackernews.WPF.Helpers
{
	/// <summary>
	/// https://michaelscodingspot.com/wpf-complete-guide-themes-skins/
	/// </summary>
	public class SkinResourceDictionary : ResourceDictionary
	{
		private Uri _lightSource;
		public Uri LightSource
		{
			get { return _lightSource; }
			// Called when set in XAML, so at the start of the app.
			set { _lightSource = value; UpdateSource(); }
		}

		private Uri _darkSource;

		public Uri DarkSource
		{
			get { return _darkSource; }
			set { _darkSource = value; UpdateSource(); }
		}


		public void UpdateSource()
		{
			var val = App.Skin == Skin.Light ? LightSource : DarkSource;
			if (val != null && base.Source != val)
				base.Source = val; // Update the ResourceDictionaries source, therefore updating the theme that is applied.
		}
	}
}
