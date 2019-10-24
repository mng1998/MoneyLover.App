using MoneyLover.Application.DB;
using MoneyLover.Application.Models;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private MoneyLoverDB db = new MoneyLoverDB();
        private SignIn signIn;
        private Register register;
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (Register_(txtEmail.Text, psdPassword.Password))
            {
                MessageBox.Show("Đăng kí thành công!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);

                signIn = new SignIn();
                signIn.ShowDialog();
            }
            else
                MessageBox.Show("Đã tồn tại emaial này. Vui lòng nhập lại!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public bool Register_(string email, string password)
        {
           
            User checkUser = db.Users.Where(m => m.Email == email).SingleOrDefault();
            if (checkUser == null)
            {
                db.Users.Add(new User { Email = email, Password = password, Wallet = 100000000, SavingsWallet = 0 });
                db.SaveChanges();
                return true;
            }
            else
                return false;
           
        }
    }
}
