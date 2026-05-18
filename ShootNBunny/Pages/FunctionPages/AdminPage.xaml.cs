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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShootNBunny.Pages.FunctionPages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public static List<Complaint> complaints = Core.Context.Complaint.Where(c => c.Satisfied == false).ToList();
        public static List<User> frozenUsers = Core.Context.User.Where(u => u.Frozen).ToList();
        public static List<Book> frozenBooks = Core.Context.Book.Where(b => b.Frozen).ToList();
        public static List<Review> frozenReviews = Core.Context.Review.Where(r => r.Frozen).ToList();
        public static List<RoleApplication> roleApplications = Core.Context.RoleApplication.Where(app => app.User.Roles.Name != "Автор").ToList();
        public static List<User> users = Core.Context.User.ToList();
        public static List<ComplaintFreeze> complaintFreezes = Core.Context.ComplaintFreeze.ToList();
        public AdminPage()
        {

            InitializeComponent();
            Update();
        }


        private void SatisfyComplaintBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Complaint complaint = (Complaint)button.DataContext;
            switch (complaint.ComplainType)
            {
                case "Книга":
                    complaint.Book.Frozen = true;
                    complaint.Satisfied = true;
                    break;
                case "Автор":
                    List<User> users = Core.Context.User.ToList();
                    users.FirstOrDefault(u => u.ID == complaint.AuthorID).Frozen = true;
                    complaint.Satisfied = true;
                    break;
                case "Отзыв":
                    complaint.Review.Frozen = true;
                    complaint.Satisfied = true;
                    break;
            }
            Core.Context.SaveChanges();
            Update();
        }

        private void UnSatisfyComplaintBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Complaint complaint = (Complaint)button.DataContext;
            Core.Context.Complaint.Remove(complaint);
            Core.Context.SaveChanges();
            Update();
        }


        private void UnfreezeBoolBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Book book = (Book)button.DataContext;
            book.Frozen = false;
            Core.Context.SaveChanges();
            Update();

        }

        private void UnfreezeUserBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.DataContext;
            user.Frozen = false;
            Core.Context.SaveChanges();
            Update();
        }

        private void UnfreezeReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Review review = (Review)button.DataContext;
            review.Frozen = false;
            Core.Context.SaveChanges();
            Update();
        }

        private void Update()
        {
            complaints = Core.Context.Complaint.Where(c => c.Satisfied == false).ToList();
            frozenUsers = Core.Context.User.Where(u => u.Frozen).ToList();
            frozenBooks = Core.Context.Book.Where(b => b.Frozen).ToList();
            frozenReviews = Core.Context.Review.Where(r => r.Frozen).ToList();
            roleApplications = Core.Context.RoleApplication.Where(app => app.User.Roles.Name != "Автор").ToList();
            users = Core.Context.User.ToList();

            ComplaintLB.ItemsSource = complaints.ToList();
            FrozenBooksLB.ItemsSource = frozenBooks.ToList();
            FrozenUsersLB.ItemsSource = frozenUsers.ToList();
            FrozenReviewsLB.ItemsSource = frozenReviews.ToList();
            ApplicationLB.ItemsSource = roleApplications.ToList();
            UsersLB.ItemsSource = users.ToList();
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            RoleApplication application = (RoleApplication)button.DataContext;
            application.User.RoleID = Core.Context.Roles.FirstOrDefault(r => r.Name == "Автор").ID;
            Core.Context.RoleApplication.Remove(application);
            Core.Context.SaveChanges();
            Update();
        }

        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            RoleApplication application = (RoleApplication)button.DataContext;
            Core.Context.RoleApplication.Remove(application);
            Core.Context.SaveChanges();
            Update();
        }


        private void ChangePsswrd_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.DataContext;
            NavigationService.Navigate(new ChangePswrdPage(user));
        }

        private void ChangeRole_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.DataContext;
            NavigationService.Navigate(new ChangeRolePage(user));
        }

        private void AcceptUnfreezeBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ComplaintFreeze application = (ComplaintFreeze)button.DataContext;
            application.User.Frozen = false;
            Core.Context.ComplaintFreeze.Remove(application);
            Core.Context.SaveChanges();
            Update();
        }

        private void RejectUnfreezeBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ComplaintFreeze application = (ComplaintFreeze)button.DataContext;
            Core.Context.ComplaintFreeze.Remove(application);
            Core.Context.SaveChanges();
            Update();
        }
    }
}
