using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShootNBunny.Pages.FunctionPages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public User user_ {  get; set; }
        public ProfilePage(User user)
        {
            InitializeComponent();
            this.user_ = user;
            DataContext = user_;
            ReviewsLB.ItemsSource = Core.Context.Review.Where(r => r.UserID == user_.ID).ToList();

            if(!user_.Frozen)
            {
                FreezeComplaintBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                List<string> complaint_reasons = Core.Context.Complaint.Where(c => (c.AuthorID == user.ID 
                || c.Review.UserID == user.ID 
                || c.Book.User.ID == user.ID)
                && c.Satisfied).Select(c => c.Reason.Name).ToList();
                foreach(string reason in complaint_reasons)
                {
                    FreezeReasonTB.Text += reason;
                    FreezeReasonTB.Text += "\n";
                }
            }

            if (user.Roles.Name != "Читатель")
            {
                AuthorBtn.Visibility = Visibility.Hidden;
            }

        }

        private void AuthorBtn_Click(object sender, RoutedEventArgs e)
        {
            if(user_.Frozen)
            {
                MessageBox.Show("Замороженные пользователи не могут писать книги и отзывы", "Внимание");
            }
            else
            {
                NavigationService.Navigate(new ApplicationPage());
            }
        }

        private void FreezeComplaintBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FreezeComplainPage());
        }
    }
}
