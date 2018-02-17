using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GlazerCalc
{
    public sealed partial class MainPage : Page
    {
        double width, height, woodLength, glassArea;

        private bool ValidateDimensions(object sender, RoutedEventArgs e)
        {
            bool isEmpty = string.IsNullOrWhiteSpace(WidthInput.Text) && string.IsNullOrWhiteSpace(HeightInput.Text);
            if(isEmpty)
            {
                DisplayInvalidDimensionsDialog();
                return false;
            }

            width = Convert.ToDouble(WidthInput.Text);
            height = Convert.ToDouble(HeightInput.Text);
            if ((width <= 5.0 && width >= 0.5) && (height <= 3.0 && height >= 0.75))
            {
                return true;
            }
            else
            {
                DisplayInvalidDimensionsDialog();
                return false;
            }
        }

        private bool ValidateTintSelection(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(BlackTint.IsChecked) || Convert.ToBoolean(BlueTint.IsChecked) ||
                Convert.ToBoolean(BrownTint.IsChecked))
            {
                return true;
            }
            else
            {
                DisplayNoSelectionDialog();
                return false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDimensions(sender, e) && ValidateTintSelection(sender, e))
            {
                width = Convert.ToDouble(WidthInput.Text);
                height = Convert.ToDouble(HeightInput.Text);
                woodLength = 2 * (width + height) * 3.25;
                glassArea = 2 * (width * height);
                String tintColor;
                if (Convert.ToBoolean(BlackTint.IsChecked))
                {
                    tintColor = Convert.ToString(BlackTint.Content);
                }
                else if (Convert.ToBoolean(BlueTint.IsChecked))
                {
                    tintColor = Convert.ToString(BlueTint.Content);
                }
                else
                {
                    tintColor = Convert.ToString(BrownTint.Content);
                }
                int quantity = Convert.ToInt32(quantitySlider.Value);

                Glazer glazer = new Glazer(width, height, woodLength, glassArea, tintColor, quantity);
                glazer.DisplayResults(ResultsHeader, Results);
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        void widthKeyUp(object sender, RoutedEventArgs e)
        {
            bool isDouble = Double.TryParse(WidthInput.Text, out double widthValue);
            if (!isDouble && !string.IsNullOrWhiteSpace(WidthInput.Text))
            {
                DisplayInvalidValueDialog();
            }
        }

        void heightKeyUp(object sender, RoutedEventArgs e)
        {
            bool isDouble = Double.TryParse(HeightInput.Text, out double heightValue);
            if (!isDouble && !string.IsNullOrWhiteSpace(HeightInput.Text))
            {
                DisplayInvalidValueDialog();
            }
        }

        private async void DisplayInvalidValueDialog()
        {
            ContentDialog invalid = new ContentDialog
            {
                Title = "Invalid character",
                Content = "Please enter a valid measurement for the width and height. All values must but numerical.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await invalid.ShowAsync();
        }

        private async void DisplayNoSelectionDialog()
        {
            ContentDialog noSelection = new ContentDialog
            {
                Title = "No Selection",
                Content = "Please select the tint color for your order.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noSelection.ShowAsync();
        }

        private async void DisplayInvalidDimensionsDialog()
        {
            ContentDialog invalidDimensions = new ContentDialog
            {
                Title = "Invalid Dimensions",
                Content = "Invalid Width or Height entered. Please enter a width between 0.5ft to 5.0ft and a height between 0.75ft to 3.0ft.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await invalidDimensions.ShowAsync();
        }

    }
}
