using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System;
using System.IO;
using System.Linq;

namespace ProductCatalog
{
    public partial class MainWindow : Window
    {
        private class Product
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public bool Available { get; set; }
            public string Category { get; set; }
            public string ImagePath { get; set; }
        }
        
        private readonly Product[] produkty = new Product[]
        {
            new Product { Name = "Kawa", Price = 3, Available = true, Category = "NapojeGorące", ImagePath = "kawa.png" },
            new Product { Name = "TelewizorSamsung", Price = 3000, Available = true, Category = "Elektronika", ImagePath = "monitor.png" },
            new Product { Name = "Kebab", Price = 15, Available = true, Category = "Miłość", ImagePath = "kebab.png" }
        };

        private Product currentProduct; 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object? sender, RoutedEventArgs e)
        {
            string searchName = InputTextBox.Text.ToLower();
            currentProduct = produkty.FirstOrDefault(p => p.Name.ToLower().Contains(searchName));

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (currentProduct != null)
            {
                ShowTextBlock.Text = $"Nazwa: {currentProduct.Name}\nCena: {currentProduct.Price} PLN\nDostępność: " +
                                     $"{(currentProduct.Available ? "Dostępny" : "Niedostępny")}\nKategoria: {currentProduct.Category}";

                
                string imagePath = Path.Combine(AppContext.BaseDirectory, currentProduct.ImagePath);
                if (File.Exists(imagePath))
                {
                    ProductImage.Source = new Bitmap(imagePath);
                }
                else
                {
                    ProductImage.Source = null;
                }

                ChangeDepositButton.IsVisible = true; 
            }
            else
            {
                ShowTextBlock.Text = "Produkt nie znaleziony";
                ProductImage.Source = null;
                ChangeDepositButton.IsVisible = false;
            }
        }

        private void ChangeDeposit_Click(object? sender, RoutedEventArgs e)
        {
            if (currentProduct != null)
            {
                currentProduct.Available = !currentProduct.Available;
                UpdateDisplay();
            }
        }
    }
}
