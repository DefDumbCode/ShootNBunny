using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootNBunny
{
    public partial class Complaint
    {
        public string ComplainType
        {
            get
            {
                if (BookID != null) return "Книга";
                if (AuthorID != null) return "Автор";
                if (ReviewID != null) return "Отзыв";
                return "Отзыв";
            }
        }

        public string ComplaintObjName
        {
            get
            {
                switch (ComplainType)
                {
                    case "Книга":
                        return Book.Name;
                    case "Автор":
                        List<User> users = Core.Context.User.ToList();
                        return users.FirstOrDefault(u => u.ID == AuthorID).Name;
                    case "Отзыв":
                        return Review.Text;
                    default:
                        return "";
                }
            }
        }
    }
}
