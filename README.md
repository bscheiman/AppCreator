[![Stories in Ready](https://badge.waffle.io/bscheiman/AppCreator.png?label=ready&title=Ready)](https://waffle.io/bscheiman/AppCreator)
# AppCreator

This library contains several Xamarin University-backed best practices & time savers:

- MVVM
- Simple ViewModels w/ [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged)
- Several controls
- JSON libraries
- Retry policies w/ [Polly](https://github.com/michael-wolfenden/Polly)
- Error handling w/ [AsyncErrorHandler.Fody](https://github.com/Fody/AsyncErrorHandler)

You'd usually use this library after using [my tools](http://tools.bscheiman.org), but you can just drop it into a Xamarin.Forms project.

# Base page

This is what a normal AppCreator XAML page looks like:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<acp:BasePage
	x:TypeArguments="acvm:BaseViewModel"
	xmlns="http://xamarin.com/schemas/2014/forms"
	xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
	xmlns:acp="clr-namespace:AppCreator.Pages;assembly=AppCreator"
	xmlns:acc="clr-namespace:AppCreator.UI.Controls;assembly=AppCreator.UI"
	xmlns:acvm="clr-namespace:AppCreator.ViewModels;assembly=AppCreator"
	xmlns:accu="clr-namespace:AppCreator.Custom;assembly=AppCreator"
	xmlns:am="clr-namespace:AppCreator.Markup;assembly=AppCreator"
	xmlns:e="clr-namespace:AppCreator.Behaviors;assembly=AppCreator"
	xmlns:v="clr-namespace:XXX.ViewModels;assembly=XXX"
	x:Class="App.Pages.DemoPage">
	<ScrollView>
		<StackLayout
			Orientation="Vertical"
			Padding="0"
			Spacing="0">
			<!-- Your content here -->
		</StackLayout>
	</ScrollView>
</acp:BasePage>
```

```csharp
namespace App.ViewModels {
	public partial class DemoPage : BasePage<BaseViewModel> { // You can remove the BasePage part as it's inferred from the other partial file, but you lose Intellisense.
		public DemoPage() {
			InitializeComponent();
		}
	}
}
```

# View Models

(TODO)

# JSON

(TODO)

# Setup

Once created, your app needs to add the `AppCreator` and `AppCreator.UI` packages via NuGet. This will add a crapload of references to the main PCL project.

# Updates

Do NOT update Fody past 1.29.0
Only update AppCreator.UI - this will take care of everything else.
