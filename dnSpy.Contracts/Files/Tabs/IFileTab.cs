﻿/*
    Copyright (C) 2014-2015 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace dnSpy.Contracts.Files.Tabs {
	/// <summary>
	/// A tab
	/// </summary>
	public interface IFileTab {
		/// <summary>
		/// Current <see cref="IFileTabContent"/> instance
		/// </summary>
		IFileTabContent FileTabContent { get; }

		/// <summary>
		/// Current <see cref="IFileTabUIContext"/>
		/// </summary>
		IFileTabUIContext UIContext { get; }

		/// <summary>
		/// Gets the <see cref="IFileTabManager"/> owner
		/// </summary>
		IFileTabManager FileTabManager { get; }

		/// <summary>
		/// true if this is the active tab
		/// </summary>
		bool IsActiveTab { get; }

		/// <summary>
		/// true if <see cref="NavigateBackward()"/> can execute
		/// </summary>
		bool CanNavigateBackward { get; }

		/// <summary>
		/// Navigates backward in history
		/// </summary>
		void NavigateBackward();

		/// <summary>
		/// true if <see cref="NavigateForward()"/> can execute
		/// </summary>
		bool CanNavigateForward { get; }

		/// <summary>
		/// Navigates forward in history
		/// </summary>
		void NavigateForward();

		/// <summary>
		/// Follows a reference
		/// </summary>
		/// <param name="ref">Reference</param>
		/// <param name="sourceContent">Source content or null</param>
		void FollowReference(object @ref, IFileTabContent sourceContent = null);

		/// <summary>
		/// Shows the tab content
		/// </summary>
		/// <param name="tabContent">Tab content</param>
		/// <param name="serializedUI">Serialized UI data or null</param>
		/// <param name="onShown">Called after the output has been shown on the screen</param>
		void Show(IFileTabContent tabContent, object serializedUI, Action<ShowTabContentEventArgs> onShown);

		/// <summary>
		/// Sets focus to the focused element if this is the active tab
		/// </summary>
		void SetFocus();
	}

	/// <summary>
	/// Extension methods
	/// </summary>
	public static class FileTabExtensionMethods {
		/// <summary>
		/// Follows a reference in a new tab
		/// </summary>
		/// <param name="self">This</param>
		/// <param name="ref">Reference</param>
		public static void FollowReferenceNewTab(this IFileTab self, object @ref) {
			var tab = self.FileTabManager.OpenEmptyTab();
			tab.FollowReference(@ref, self.FileTabContent);
			self.FileTabManager.SetFocus(tab);
		}

		/// <summary>
		/// Follows a reference
		/// </summary>
		/// <param name="self">This</param>
		/// <param name="ref">Reference</param>
		/// <param name="newTab">true to open a new tab</param>
		public static void FollowReference(this IFileTab self, object @ref, bool newTab) {
			if (newTab)
				self.FollowReferenceNewTab(@ref);
			else
				self.FollowReference(@ref);
		}
	}
}
