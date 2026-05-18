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
    /// Логика взаимодействия для RedactionPage.xaml
    /// </summary>
    public partial class RedactionPage : Page
    {
        public Book book_ {  get; set; }
        public RedactionPage(Book book)
        {
            InitializeComponent();
            this.book_ = book;
            BookTextTB.Text = book.Text;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                var conf = MessageBox.Show("Изменения будут сохранены", "Подтвердите действие", MessageBoxButton.OKCancel);
                if (conf == MessageBoxResult.OK)
                {
                    if (!String.IsNullOrEmpty(book_.Text))
                    {
                        book_.Text = BookTextTB.Text;
                        Core.Context.SaveChanges();
                        Core.Update();
                        NavigationService.Navigate(new AuthorPage());
                    }
                    else
                    {
                        MessageBox.Show("Книга должна иметь содержание");
                    }
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                if (book_.Text != BookTextTB.Text)
                {
                    var conf = MessageBox.Show("При возвращении назад внесенные изменения будут утеряны", "Подтвердите действие", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (conf == MessageBoxResult.OK)
                    {
                        NavigationService.GoBack();
                    }
                }
                NavigationService.GoBack();

            }
        }
    }
}
