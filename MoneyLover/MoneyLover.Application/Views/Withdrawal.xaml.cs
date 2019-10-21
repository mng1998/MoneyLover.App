﻿using MoneyLover.Application.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoneyLover.Application.Views
{
    /// <summary>
    /// Interaction logic for Withdrawal.xaml
    /// </summary>
    public partial class Withdrawal : Window
    {
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();


        private Dictionary<int, string> term = new Dictionary<int, string>
        {
            { 99, "Không thời hạn" },
            { 1, "1 tháng" },
            { 3, "3 tháng" },
            { 6, "6 tháng" },
            { 12, "12 tháng" },
        };

        private Dictionary<int, string> payInterest = new Dictionary<int, string>
        {
            { 1, "Cuối kỳ" },
            { 2, "Đầu kỳ" },
            { 3, "Định kỳ hằng tháng" }
        };

        private Dictionary<int, string> due = new Dictionary<int, string>
        {
            { 1, "Tái tục gốc và lãi" },
            { 2, "Tái tục gốc" },
            { 3, "Tất toán sổ" }
        };
        public Withdrawal()
        {
            InitializeComponent();
            using (var db = new MoneyLoverDB())
            {
                cbPassbookId.ItemsSource = db.PassBooks.Where(x => x.Settlement == false).ToList();
                cbPassbookId.DisplayMemberPath = "PassBookID";
                cbPassbookId.SelectedValuePath = "PassBookID";
            }

            cbbTerm.ItemsSource = term;
            cbbTerm.SelectedValuePath = "Keys";
            cbbTerm.DisplayMemberPath = "Value";

            cbbPayInterest.ItemsSource = payInterest;
            cbbPayInterest.SelectedValuePath = "Keys";
            cbbPayInterest.DisplayMemberPath = "Value";

            cbbDue.ItemsSource = due;
            cbbDue.SelectedValuePath = "Keys";
            cbbDue.DisplayMemberPath = "Value";
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void cbPassbookId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x = (Models.PassBook)cbPassbookId.SelectedItem;
            if (x != null)
            {
                cbPassbookId.SelectedValue = x.PassBookID.ToString();
                txtIndefiniteTerm.Text = x.IndefiniteTerm.ToString();
                txtInterestRates.Text = x.InterestRates.ToString();
                txtDeposit.Text = x.Deposit.ToString();
                cbbTerm.SelectedIndex = x.Term;
                txtBank.Text = x.BankID.ToString();
                cbbDue.SelectedIndex = x.Due;
                cbbPayInterest.SelectedIndex = x.PayInterest;
            }
        }

        private void btnSettlement_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn tất toán sổ tiết kiệm này?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var db = new MoneyLoverDB())
                    {
                        var ct = db.PassBooks.SingleOrDefault(t => t.PassBookID.ToString() == cbPassbookId.SelectedValue.ToString());
                        ct.Settlement = true;
                        db.SaveChanges();
                    }
                    MessageBox.Show("Tất toán thành công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
           
        }
    }
}
