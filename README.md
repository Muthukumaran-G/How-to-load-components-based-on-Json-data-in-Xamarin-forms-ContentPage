# How-to-load-components-based-on-Json-data-in-Xamarin-forms-ContentPage

StreamReader allows to read the files from local path. Read the Json file and deserialize the contents using Newtonsoft.JsonConvert(You need to install the **Newtonsoft.Json** package). StreamReader is often used with the 'using' statement. This helps to dispose of system resources. Check with the below code usage.

```
var assembly = typeof(MainPage).GetTypeInfo().Assembly;
Stream stream = assembly.GetManifestResourceStream("JsonProject.JSONData.json");
using (StreamReader sr = new StreamReader(stream))
{
  var jsonText = sr.ReadToEnd();
  var data = JsonConvert.DeserializeObject<JsonData>(jsonText);
}
```

Now use the 'switch' statement to create elements based on the acquired Json data to populate content for ContentPage.

```
var controls = pageData.Pages[0].Controls;
var grid = new Grid();
foreach (var control in controls)
{
  grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
  switch (control.UIType)
  {
    case "TEXT":
                var stacklayout = new StackLayout() { IsVisible = !control.IsHidden };
                var title = new Label() { Text = control.DisplayName };
                var value = new Entry() { Text = control.Value };
                stacklayout.Children.Add(title);
                stacklayout.Children.Add(value);
                grid.Children.Add(stacklayout);
                Grid.SetRow(stacklayout, control.DisplayOrder - 1);
                break;
    case "TEXTAREA":
                var stacklayoutTEXTAREA = new StackLayout() { IsVisible = !control.IsHidden };
                var titleTEXTAREA = new Label() { Text = control.DisplayName };
                var valueTEXTAREA = new Editor() { Text = control.Value };
                stacklayoutTEXTAREA.Children.Add(titleTEXTAREA);
                stacklayoutTEXTAREA.Children.Add(valueTEXTAREA);
                grid.Children.Add(stacklayoutTEXTAREA);
                Grid.SetRow(stacklayoutTEXTAREA, control.DisplayOrder - 1);
                break;
    case "DATE":
                var stacklayoutDATE = new StackLayout() { IsVisible = !control.IsHidden };
                var titleDATE = new Label() { Text = control.DisplayName };
                var valueDATE = new DatePicker();
                stacklayoutDATE.Children.Add(titleDATE);
                stacklayoutDATE.Children.Add(valueDATE);
                grid.Children.Add(stacklayoutDATE);
                Grid.SetRow(stacklayoutDATE, control.DisplayOrder - 1);
                break;
    case "SELECTION":
                var stacklayoutSELECTION = new StackLayout() { IsVisible = !control.IsHidden };
                var titleSELECTION = new Label() { Text = control.DisplayName };
                var valueSELECTION = new Picker() { ItemsSource = control.ValueList, SelectedItem = control.Value };
                stacklayoutSELECTION.Children.Add(titleSELECTION);
                stacklayoutSELECTION.Children.Add(valueSELECTION);
                grid.Children.Add(stacklayoutSELECTION);
                Grid.SetRow(stacklayoutSELECTION, control.DisplayOrder - 1);
                break;
     }
 }

var frame = new Frame();
frame.Content = grid;
MainPage.Content = frame;
```

Make sure to use Behavior to follow MVVM.

```
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:jsonproject="clr-namespace:JsonProject"
             x:Class="JsonProject.MainPage" BackgroundColor="Beige">

    <ContentPage.Behaviors>
        <jsonproject:MainPageBehavior/>
    </ContentPage.Behaviors>

</ContentPage>
```

That's it. Viola!

![image](https://user-images.githubusercontent.com/26808947/133564969-4aedd638-4997-4ace-8be7-61ae89a2d8ac.png)
