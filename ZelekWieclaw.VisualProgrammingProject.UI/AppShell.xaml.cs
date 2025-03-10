﻿using ZelekWieclaw.VisualProgrammingProject.Views;

namespace ZelekWieclaw.VisualProgrammingProject.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("AllBeerProducersPage", typeof(AllBeerProducersPage));
            Routing.RegisterRoute("BeerProducerPage", typeof(BeerProducerPage));
            Routing.RegisterRoute("BeerProductsPage", typeof(BeerProductsPage));
            Routing.RegisterRoute("BeerProductPage", typeof(BeerProductPage));
        }
    }
}