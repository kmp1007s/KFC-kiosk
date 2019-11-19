﻿using Kfc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KfcKiosk
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            seatCtrl.SeatEvent += MainView_Loaded;
            analysisCtrl.AnalysisEvent += MainView_Loaded;
        }

        private async void setTimeout(Action action, int timeMilisec)
        {
            await Task.Delay(timeMilisec);
            action();
        }

        private void MainView_Loaded(object sender, EventArgs args)
        {
            ToggleMainView();
            analysisCtrl.refreshViewData();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            App.seatData.Load();
            App.floorData.Load();
            App.foodData.Load();
            ConnectionServer();

            setTimeout(() =>
            {
                onLoadFinish();
            }, 2000);
        }

        private void ConnectionServer()
        {
            App.tc.ConnectTCPServer("10.80.163.138", 80);
            App.tc.TCPLogin("@2215");
        }

        private void onLoadFinish()
        {
            loadingView.Visibility = Visibility.Collapsed;
            seatCtrl.Visibility = Visibility.Visible;
        }

        private void ToggleMainView()
        {
            if (seatCtrl.Visibility == Visibility.Visible)
            {
                seatCtrl.Visibility = Visibility.Collapsed;
                analysisCtrl.Visibility = Visibility.Visible;
            }
            else
            {
                seatCtrl.Visibility = Visibility.Visible;
                analysisCtrl.Visibility = Visibility.Collapsed;
            }
        }
    }
}
