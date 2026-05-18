using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootNBunny
{
    public partial class UnfreezeApplication
    {
        public string FrozenType
        {
            get
            {
                if (AuthorID != null) return "Автор";
                if (BookID != null) return "Книга";
                if (ReviewID != null) return "Отзыв";
                return "Автор";
            }
        }

        public string FrozenPreview
        {
            get
            {
                switch (FrozenType)
                {
                    case "Автор":
                        List<User> users = Core.Context.User.ToList();
                        return $"Автор: {users.First(u => u.ID == AuthorID)}";
                    case "Книга":
                        return $"Книга: {Book.Name}";
                    case "Отзыв":
                        return $"Отзыв: {Review.Text}";
                    default:
                        return "";
                }
            }
        }
    }
}
