﻿using System.Windows;
using System.Windows.Input;
using vmPing.Classes;

namespace vmPing.Views
{
    /// <summary>
    /// Interaction logic for EmailAlertWindow.xaml
    /// </summary>
    public partial class EmailAlertWindow : Window
    {
        ApplicationOptions _applicationOptions;

        public EmailAlertWindow(ApplicationOptions appOptions)
        {
            InitializeComponent();

            _applicationOptions = appOptions;

            txtEmailServer.Text = appOptions.EmailServer;
            txtEmailRecipient.Text = appOptions.EmailRecipient;
            txtEmailFromAddress.Text = appOptions.EmailFromAddress;

            // Set initial focus to text box.
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmailServer.Text.Length == 0)
            {
                MessageBox.Show(
                    "An email server address is required.",
                    "vmPing Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                txtEmailServer.Focus();
                return;
            }
            else if (txtEmailRecipient.Text.Length == 0)
            {
                MessageBox.Show(
                    "A recipient email address is required.",
                    "vmPing Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                txtEmailRecipient.Focus();
                return;
            }
            else if (txtEmailFromAddress.Text.Length == 0)
            {
                MessageBox.Show(
                    "A message FROM address is required.",
                    "vmPing Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                txtEmailFromAddress.Focus();
                return;
            }

            _applicationOptions.EmailAlert = true;
            _applicationOptions.EmailServer = txtEmailServer.Text;
            _applicationOptions.EmailRecipient = txtEmailRecipient.Text;
            _applicationOptions.EmailFromAddress = txtEmailFromAddress.Text;

            DialogResult = true;
        }


        private void txtEmailRecipient_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtEmailFromAddress.Text.Length == 0 && txtEmailRecipient.Text.IndexOf('@') >= 0)
                txtEmailFromAddress.Text = "vmPing" + txtEmailRecipient.Text.Substring(txtEmailRecipient.Text.IndexOf('@'));
        }
    }
}
