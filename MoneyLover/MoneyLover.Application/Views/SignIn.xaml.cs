using MoneyLover.Application.DB;
using MoneyLover.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        private MoneyLoverDB db = new MoneyLoverDB();
        private SignIn signIn;
        public SignIn()
        {
            InitializeComponent();
        }

       
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (Login(txtEmail.Text, psdPassword.Password))
            {
                this.Close();
                PassbookList passBookList = new PassbookList();
                passBookList.Show();
            }
            else
                signIn.ShowDialog();
        }

        public bool Login(string email, string password)
        {
            User checkUser = db.Users.Where(m => m.Email == email).FirstOrDefault();
            if (checkUser != null && checkUser.Password == password)
            {
                StoreUser(checkUser);
                return true;
            }
            else return false;
        }
        private void StoreUser(User user)
        {
            System.Windows.Application.Current.Resources["current_user_id"] = user.UserID;
            System.Windows.Application.Current.Resources["user_signed_in"] = true;
        }
    }
}
