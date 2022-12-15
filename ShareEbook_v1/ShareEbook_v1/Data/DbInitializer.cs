using Microsoft.EntityFrameworkCore.Internal;
using ShareEbook_v1.Models;
using System;

namespace ShareEbook_v1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Posts.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{Name = "Nguyễn Quang Huy", Email="Huynguyenquang@gmail.com", PhoneNumber = "0329267878", Gender = Gender.Male, Birthday = DateTime.Now},
                new User{Name = "Lô Văn Đại", Email="daideptrai@gmail.com", PhoneNumber = "0329267877", Gender = Gender.Male, Birthday = DateTime.Now},
                new User{Name = "Phạm Thị Thùy Dương", Email="thuyduong@gmail.com", PhoneNumber = "0329267873", Gender = Gender.Female, Birthday = DateTime.Now},
                new User{Name = "Trần Thị Loan", Email="LoanTran@gmail.com", PhoneNumber = "0329269878", Gender = Gender.Female, Birthday = DateTime.Now},
                new User{Name = "Ngô Hồng Thái", Email="HonThai@gmail.com", PhoneNumber = "0339267878", Gender = Gender.Male, Birthday = DateTime.Now},
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var documents = new Document[]
            {
                new Document {Name = "ASP.Net Document", Category = "Lập trình", Author = "Microsoft", Description = "Good Document"},
                new Document {Name = "Naruto", Category = "Anime", Author = "Someone", Description = "Great Anime"},
                new Document {Name = "Chainsaw man", Category = "Manga", Author = "Someone", Description = "Great Manga"},
                new Document {Name = "Chỉ là ...", Category = "Tiểu thuyết", Author = "Reddy", Description = "Sad..."},
                new Document {Name = "Hành trình", Category = "Sách", Author = "Hidor", Description = "Good Document"},
            };

            foreach(Document d in documents)
            {
                context.Documents.Add(d);
            }
            context.SaveChanges();

            

            var accounts = new Account[]
            {
                new Account{Username = "hidro", Password = "123", Type = TypeAccount.Admin, UserId = 1},
                new Account{Username = "daideptrai", Password = "123", Type = TypeAccount.User, UserId = 2},
                new Account{Username = "thuyduong", Password = "123", Type = TypeAccount.User, UserId = 3},
                new Account{Username = "loantran", Password = "123", Type = TypeAccount.User, UserId = 4} ,
                new Account{Username = "hongthai", Password = "123", Type = TypeAccount.User, UserId = 5},
            };

            foreach (Account a in accounts)
            {
                context.Accounts.Add(a);
            }
            context.SaveChanges();

            var notifis = new Notifi[]
            {
                new Notifi{Content = "Chúc mừng bạn đã đăng tài liệu thành công", Type = 1, UserId = 1},
                new Notifi{Content = "Chúc mừng bạn đã đăng tài liệu thành công", Type = 1, UserId = 2},
                new Notifi{Content = "Chúc mừng bạn đã đăng tài liệu thành công", Type = 1, UserId = 3},
                new Notifi{Content = "Chúc mừng bạn đã đăng tài liệu thành công", Type = 1, UserId = 4},
                new Notifi{Content = "Chúc mừng bạn đã đăng tài liệu thành công", Type = 1, UserId = 5},
            };

            foreach(Notifi n in notifis)
            {
                context.Notifis.Add(n);
            }
            context.SaveChanges();

            var posts = new Post[]
            {
                new Post{DateSubmitted = DateTime.Now,  Pending = true, UserId = 1, DocumentId = 1},
                new Post{DateSubmitted = DateTime.Now.AddDays(1),  Pending = false, UserId = 2, DocumentId = 2},
                new Post{DateSubmitted = DateTime.Now.AddMonths(-1),  Pending = false, UserId = 3, DocumentId = 3},
                new Post{DateSubmitted = DateTime.Now.AddDays(-2),  Pending = true, UserId = 4, DocumentId = 4},
                new Post{DateSubmitted = DateTime.Now.AddMonths(-2),  Pending = true, UserId = 5, DocumentId = 5},
            };

            foreach(Post p in posts)
            {
                context.Posts.Add(p);
            }
            context.SaveChanges();
        }
    }
}
