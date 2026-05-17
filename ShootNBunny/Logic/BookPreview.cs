using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShootNBunny
{
    public partial class Book
    {
        public string AuthorName
        {
            get
            {
                string name = User.Name;
                string preview = name.Substring(0, Math.Min(20, name.Length)) + "...";
                if(name.Length <= preview.Length)
                {
                    return name;
                }
                else
                {
                    return preview;
                }
            }
        }

        public string BookName
        {
            get
            {
                string preview = Name.Substring(0, Math.Min(20, Name.Length)) + "...";
                if (Name.Length <= preview.Length)
                {
                    return Name;
                }
                else
                {
                    return preview;
                }
            }
        }

        public string ReviewAmnt
        {
            get
            {
                List<Review> reviews = Core.Context.Review.ToList();
                int reviewAmnt = reviews.Where(r => r.BookID == ID).Count();
                return "Отзывов: " + reviewAmnt.ToString();
            }
        }

        public string BooksGenres
        {
            get
            {
                List<string> booksGenre = BookGenre.Select(bg => bg.Genre.Name).ToList();
                string genres = "";
                foreach (string genre in booksGenre)
                {
                    genres += genre + ", ";
                }
                return genres.Substring(0, genres.Length - 2);
            }
        }
    }
}
