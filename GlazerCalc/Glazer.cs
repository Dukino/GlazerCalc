using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GlazerCalc
{
    class Glazer
    {
        private double width      { get; set; }
        private double height     { get; set; }
        private double woodLength { get; set; }
        private double glassArea  { get; set; }
        private String tintColor  { get; set; }
        private int    quantity   { get; set; }

        public Glazer(double width, double height, double woodLength, double glassArea,
            String tintColor, int quantity)
        {
            this.width      = width;
            this.height     = height;
            this.woodLength = woodLength;
            this.glassArea  = glassArea;
            this.tintColor  = tintColor;
            this.quantity   = quantity;
        }     
        
        public void DisplayResults(TextBlock header, TextBlock textBlock)
        {
            header.Text = "Review your Calculations";
            textBlock.Text = "Width: " + width +
                "\nHeight: " + height +
                "\nWood Length: " + woodLength +
                "\nGlass Area: " + glassArea +
                "\nTint Color: " + tintColor +
                "\nQuantity: " + quantity; 
        }
    }
}
