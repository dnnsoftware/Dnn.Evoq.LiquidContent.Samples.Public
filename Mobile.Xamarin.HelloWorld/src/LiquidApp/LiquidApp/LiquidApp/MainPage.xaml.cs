using LiquidApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LiquidApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var dnnService = new DnnService();
            MyLabel.Text = "Content Library : " + dnnService.GetVersion().Version;
        }
    }
}
