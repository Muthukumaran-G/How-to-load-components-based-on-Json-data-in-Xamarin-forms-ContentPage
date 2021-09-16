using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace JsonProject
{
    public class MainPageBehavior:Behavior<MainPage>
    {
        protected override void OnAttachedTo(MainPage bindable)
        {
            base.OnAttachedTo(bindable);
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("JsonProject.JSONData.json");
            using (StreamReader sr = new StreamReader(stream))
            {
                var jsonText = sr.ReadToEnd();
                var pageData = JsonConvert.DeserializeObject<JsonData>(jsonText);
                bindable.Title = pageData.Pages[0].PageTitle;
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

                var frame = new Frame() { Padding = 10, Margin = 10, CornerRadius = 10, BackgroundColor = Color.White, WidthRequest = 200, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                frame.Content = grid;
                bindable.Content = frame;
            }
        }

        protected override void OnDetachingFrom(MainPage bindable)
        {
            base.OnDetachingFrom(bindable);
        }
    }
}
