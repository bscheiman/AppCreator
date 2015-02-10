# AppCreator

This library contains several Xamarin University-backed best practices & time savers:

- MVVM
- Simple ViewModels w/ [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged)
- Several controls
- JSON libraries
- Retry policies w/ [Polly](https://github.com/michael-wolfenden/Polly)
- JSON API client generation w/ [Refit](https://github.com/paulcbetts/refit)
- Error handling w/ [AsyncErrorHandler.Fody](https://github.com/Fody/AsyncErrorHandler)

You'd usually use this library after using [my tools](http://tools.bscheiman.org), but you can just drop it into a Xamarin.Forms project.

# Base page

This is what a normal AppCreator XAML page looks like:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<acp:BasePage
	BackgroundColor="Blue"
	x:TypeArguments="acvm:BaseViewModel"
	xmlns="http://xamarin.com/schemas/2014/forms"
	xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
	xmlns:acp="clr-namespace:AppCreator.Pages;assembly=AppCreator"
	xmlns:acc="clr-namespace:AppCreator.UI.Controls;assembly=AppCreator.UI"
	xmlns:acvm="clr-namespace:AppCreator.ViewModels;assembly=AppCreator"
	x:Class="App.Pages.DemoPage">
	<ScrollView>
		<StackLayout
			Orientation="Vertical"
			Padding="0"
			Spacing="5">
			<!-- Your content here -->
		</StackLayout>
	</ScrollView>
</acp:BasePage>
```
```csharp
namespace App.Pages {
	public partial class DemoPage {
		public DemoPage() {
			InitializeComponent();
		}
	}
}
```

# Setup

Once created, your app needs to add the `AppCreator` and `AppCreator.UI` packages via NuGet. This will add a crapload of references to the main PCL project.


# Post setup

- Add `AppCreator.UI` package to iOS/Android
- Add `[assembly: Dependency(typeof(UserDialogService))]` to `AppDelegate.cs` and `MainActivity.cs`
- Add reference to `System.Net.Http` (not NuGet, just the native reference)
