/*

2024-05-22, Robert Warnestam, rwa@logos.se

While converting another project from Xamarin (.Net Framework) to MAUI (.Net 8.0) I encountered some bugs that needs to be addressed


If having a ListView that binds to an OnbservableCollection. Each item is encapsulated in a <Frame> for UI purposes. The user is able to add and remove items from the list.

--------------------------------------------
Problem 1. 

On Android the app craches with the error
	"System.ObjectDisposedException
	Message=Cannot access a disposed object.
	Object name: 'Microsoft.Maui.Controls.Handlers.Compatibility.FrameRenderer'."

On Windows it works fine, status on iOS is unknown.

Work around:

I found that the problem was that there where a <Frame> element direct under  <ViewCell>
		
--------------------------------------------
Problem 2.

The function which change a single object (by adding or removing it from a selected status) works fine on Windows. But on Android, the view always lacks behind the actual state of the object.


Work around: No acceptable found. Manually refresh list after every change?

--------------------------------------------


*/