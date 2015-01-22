# AppCreator

This library contains several Xamarin University-backed best practices & time savers:

- MVVM
- Fody
- Simple ViewModels w/ PropertyChanged.Fody
- Several controls
- JSON libraries
- Retry policies w/ Polly
- JSON API client generation w/ ReFit
- Error handling w/ AsyncErrorHandler.Fody


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
	xmlns:acc="clr-namespace:AppCreator.Controls;assembly=AppCreator"
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
